using System.Runtime.CompilerServices;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Providers;
using SoundFlow.Structs;


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

        var song = await createSong();
        var songTask = song.executeSong();
        var worldTask = world.worldExecuteMeAsync();

        await Task.WhenAll(songTask, worldTask);

        song.Dispose();

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

    public class Song : IDisposable
    {
        public MiniAudioEngine? engine;
        public AudioPlaybackDevice? outputDevice;
        public StreamDataProvider? provider;
        public SoundPlayer? player;

        public void Dispose()
        {
            if (player != null)
            {
                player.Dispose();
                player = null;
            }
            if (provider != null)
            {
                provider.Dispose();
                provider = null;
            }
            if (outputDevice != null)
            {
                outputDevice.Dispose();
                outputDevice = null;
            }
            if (engine != null)
            {
                engine.Dispose();
                engine = null;
            }
        }

        public async Task executeSong()
        {
            if (player == null)
                return;

            var tcs = new TaskCompletionSource();
            player?.PlaybackEnded += (s, e) =>
            {
                tcs.TrySetResult();
            };
            player?.Play();

            await tcs.Task;
        }
    }

    public static async Task<Song> createSong()
    {
        var song = new Song();

        song.engine = new MiniAudioEngine();

        song.outputDevice = song.engine.InitializePlaybackDevice(song.engine.PlaybackDevices.FirstOrDefault(x => x.IsDefault), AudioFormat.Cd);
        song.outputDevice.Start();

        var fileStream = await FileSystem.OpenAppPackageFileAsync("ghost.mp3");

        song.provider = new StreamDataProvider(song.engine, fileStream, new SoundFlow.Metadata.Models.ReadOptions
        {
            DurationAccuracy = SoundFlow.Metadata.Models.DurationAccuracy.AccurateScan
        });

        song.player = new SoundPlayer(song.engine, song.outputDevice.Format, song.provider);
        song.outputDevice.MasterMixer.AddComponent(song.player);

        return song;
    }
}
