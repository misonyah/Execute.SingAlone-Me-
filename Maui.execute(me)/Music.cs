using System;
using System.IO;

namespace World.Execute;

class Music
{
    public static string WorldExecuteMeSongFile()
    {
        string fileName = "ghost.mp3";
        string dir = AppContext.BaseDirectory; // executable folder
        while (!string.IsNullOrEmpty(dir))
        {
            string filePath = Path.Combine(dir, fileName);
            if (File.Exists(filePath))
                return filePath;

            dir = Directory.GetParent(dir)?.FullName;
        }

        throw new FileNotFoundException("Convert https://www.youtube.com/watch?v=ESx_hy1n7HA to mp3");
    }
}