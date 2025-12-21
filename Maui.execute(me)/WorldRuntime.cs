using NAudio.Wave;

namespace World.Execute;

public static class WorldRuntime
{
    public static async Task RunAsync()
    {
        WorldInterface.WriteLine("Initializing…");

        await ReadFileAsync("textFiles/user");

        await world.animateTextAsync("Song: World.Execute(Me)", "", "white", 10, false);
        await world.animateTextAsync("Producer: Mili", "", "white", 10, false);

        await Task.Delay(2000);
        WorldInterface.Clear();

        var musicFile = Music.WorldExecuteMeSongFile();

        var songTask = PlayMusicAsync(musicFile);
        var worldTask = world.worldExecuteMeAsync();

        await Task.WhenAll(songTask, worldTask);

        WorldInterface.Run();
    }

    private static async Task ReadFileAsync(string fileName)
    {
        try
        {
            // Assumes the file is added as a MAUI Asset in your project
            using var stream = await FileSystem.OpenAppPackageFileAsync($"{fileName}.txt");
            using var reader = new StreamReader(stream);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                WorldInterface.WriteLine(line);
                await Task.Delay(5);
            }
        }
        catch (FileNotFoundException)
        {
            WorldInterface.WriteLine("The file could not be found.");
        }
        catch (Exception e)
        {
            WorldInterface.WriteLine($"Error reading file: {e.Message}");
        }
    }


    private static Task PlayMusicAsync(string musicFile)
    {
        return Task.Run(() =>
        {
            using var audioRender = new AudioFileReader(musicFile);
            using var outputDevice = new WaveOutEvent();

            outputDevice.Init(audioRender);
            outputDevice.Play();

            while (outputDevice.PlaybackState == PlaybackState.Playing)
                Thread.Sleep(1);
        });
    }
}
