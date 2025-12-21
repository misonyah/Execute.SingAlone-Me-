using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace World.Execute;

internal static partial class world
{
    private static DateTime _startTime;

    // Timing helper to wait until specific LRC timestamp
    public static async Task Timing(int min, int sec, int hs)
    {
        if (_startTime == default) _startTime = DateTime.Now;
        double targetMs = min * 60_000 + sec * 1000 + hs * 10;
        double elapsedMs = (DateTime.Now - _startTime).TotalMilliseconds;
        double waitMs = targetMs - elapsedMs;
        if (waitMs > 0)
            await Task.Delay((int)waitMs);
    }

    public static async Task readFileAsync(string fileName)
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName + ".txt");
            using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);

            string? line;
            bool firstLine = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (!firstLine)
                    WorldInterface.WriteLine(""); // only add newline between lines
                WorldInterface.Write(line);
                firstLine = false;
                await Task.Delay(5);
            }

            // Handle file that doesn't end with a newline
            if (firstLine) // file was empty, nothing written
                return;
            WorldInterface.WriteLine(""); // final linefeed
        }
        catch (Exception e)
        {
            WorldInterface.WriteLine($"Error reading file: {e.Message}");
        }
        // return await readFileColorAsync(fileName, parseColors: false);
    }



    public static async Task readFileColorAsync(string fileName, bool parseColors = true)
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName + ".txt");
            using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);

            string? line;
            bool firstLine = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (!firstLine)
                    WorldInterface.WriteLine(""); // only add newline between lines
                firstLine = false;

                foreach (char c in line)
                {
                    if (parseColors)
                    {
                        switch (c)
                        {
                            case '+': WorldInterface.Write("\x1b[31m"); break;
                            case '$':
                            case 'X': WorldInterface.Write("\x1b[33m"); break;
                            default: WorldInterface.Write("\x1b[0m"); break;
                        }
                    }

                    WorldInterface.Write(c.ToString());

                    if (parseColors)
                        WorldInterface.Write("\x1b[0m"); // reset after each char
                }

                await Task.Delay(5);
            }

            // Ensure the last line is terminated
            if (!firstLine)
                WorldInterface.WriteLine("");
        }
        catch (Exception e)
        {
            WorldInterface.WriteLine($"Error reading file: {e.Message}");
        }
    }



    public static async Task slowTypeAsync(string text, int delayMilliseconds, bool newLine = true, int spaceNumber = 0, string color = "")
    {
        delayMilliseconds = (int)(delayMilliseconds * 0.9);     // HMMZ
        SetColor(color);
        WorldInterface.Write("[Console] ");

        int i = 0;
        while (i < text.Length)
        {
            if (text[i] == '\x1b')
            {
                // find the end of the escape sequence
                int start = i;
                i++; // skip ESC
                if (i < text.Length && text[i] == '[')
                {
                    i++;
                    while (i < text.Length && (char.IsDigit(text[i]) || text[i] == ';'))
                        i++;
                    if (i < text.Length && text[i] == 'm')
                        i++; // include final 'm'
                }
                // write the full escape sequence at once
                WorldInterface.Write(text.Substring(start, i - start));
                continue;
            }

            WorldInterface.Write(text[i].ToString());
            i++;
            await Task.Delay(delayMilliseconds);
        }

        if (newLine)
        {
            WorldInterface.WriteLine();
            for (int j = 0; j < spaceNumber; j++)
                WorldInterface.WriteLine();
        }

        WorldInterface.Write("\x1b[0m"); // reset color
    }

    // public static async Task slowType2Async(string text, string name, int delayMilliseconds, bool newLine = true, int spaceNumber = 0, string color = "")
    // {
    //     SetColor(color);
    //     WorldInterface.Write(name + " ");
    //     foreach (char c in text)
    //     {
    //         WorldInterface.Write(c.ToString());
    //         await Task.Delay(delayMilliseconds);
    //     }

    //     if (newLine)
    //     {
    //         WorldInterface.WriteLine();
    //         for (int i = 0; i < spaceNumber; i++)
    //             WorldInterface.WriteLine();
    //     }
    //     WorldInterface.Write("\x1b[0m"); // reset color
    // }

    private static void SetColor(string color)
    {
        switch (color.ToLower())
        {
            default:
            case "green": WorldInterface.Write("\x1b[32m"); break;
            case "blue": WorldInterface.Write("\x1b[34m"); break;
            case "yellow": WorldInterface.Write("\x1b[33m"); break;
            case "red": WorldInterface.Write("\x1b[31m"); break;
            case "white": WorldInterface.Write("\x1b[37m"); break;
        }
    }

    public static async Task animateTextAsync(string animateWord, string actorTalk, string color, int loops, bool sepColor)
    {
        WorldInterface.Write("\r");
        for (int a = 0; a < loops; a++)
        {
            char[] shuffled = animateWord.ToCharArray();
            Shuffle(shuffled);
            WorldInterface.Write(actorTalk + new string(shuffled) + "\r");
            // await Task.Delay(50);
            await Task.Delay(30);
        }

        SetColor(color);
        if (sepColor)
            WorldInterface.WriteLine(actorTalk + " \x1b[33m" + animateWord + "\x1b[0m");
        else
            WorldInterface.WriteLine(actorTalk + animateWord);
        WorldInterface.Write("\x1b[0m"); // reset color
    }

    // Example of async simulateWorld
    private static async Task simulateWorldAsync(int worldtype)
    {
        int totalBars = 69;
        int delay = 187;
        string[] phrases = { "Adding. 'You' and 'Me'.", "Generating the Universe", "Adding Star and Moons..", "Crafting the Narrative." };
        Random random = new();
        WorldInterface.WriteLine("████████Generating World████████");
        await readFileAsync("textFiles/generatingWorld" + worldtype);
        WorldInterface.WriteLine("Seed ID: 03108891624980232");

        for (int i = 0; i <= totalBars; i++)
        {
            string randomPhrase = phrases[random.Next(phrases.Length)];
            string progressBar = randomPhrase + " [" + new string('#', i) + new string('-', totalBars - i) + "]";
            double percent = Math.Ceiling(i * 10 / 6.9);
            WorldInterface.Write("\r" + progressBar + " " + percent + "%");
            await Task.Delay(delay);
        }
        WorldInterface.WriteLine();
    }

    private static void Shuffle(char[] array)
    {
        Random rand = new();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    public static async Task encryptWallAsync(int loopAmount, List<string> wordBlock)
    {
        int sleepAmount = (int)Math.Ceiling(100.0 / loopAmount);
        for (int i = 0; i < loopAmount; i++)
        {
            foreach (var word in wordBlock)
            {
                WorldInterface.Write(word + "\r");
                await Task.Delay(sleepAmount);
            }
            WorldInterface.WriteLine();
        }
    }

    public static async Task simulateLoadingAsync(string message, int delayAmount, int barAmount)
    {
        int totalBars = barAmount;
        for (int i = 0; i <= totalBars; i++)
        {
            string progressBar = "[" + new string('#', i) + new string('-', totalBars - i) + "]";
            WorldInterface.Write($"\r{progressBar} {i * 100 / totalBars}%");
            await Task.Delay(delayAmount);
        }
        WorldInterface.WriteLine(message);
    }
}
