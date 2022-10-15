using System.Diagnostics;
using System.IO;
using System;
using System.Media;
using NAudio;
using NAudio.Wave;
using System.Threading;

namespace YAGMCBSoundPanel
{
    public static class Program
    {
        public static string SoundQueDirectory = "C:\\Users\\RandomiaGaming\\Desktop\\YAGMCB Soundtrack\\YAGMCB Performance Soundtrack";
        public static SoundPlayer[] SoundQues;
        public static int PlayingSoundIndex = 0;
        public static void Main()
        {
            string[] soundQueFilePaths = Directory.GetFiles(SoundQueDirectory);
            SoundQues = new SoundPlayer[soundQueFilePaths.Length];
            for (int i = 0; i < soundQueFilePaths.Length; i++)
            {
                SoundQues[i] = new SoundPlayer(soundQueFilePaths[i]);
            }
            PlaySound(0);
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.KeyChar is 'd')
                {
                    SkipFowards();
                }
                else if (consoleKeyInfo.KeyChar is 'a')
                {
                    SkipBackwards();
                }
                else if (consoleKeyInfo.KeyChar is 's')
                {
                    Stop();
                }
                else if (consoleKeyInfo.KeyChar is 'r')
                {
                    Replay();
                }
            }
        }
        public static void Replay()
        {
            PlaySound(PlayingSoundIndex);
        }
        public static void SkipFowards()
        {
            if (PlayingSoundIndex == SoundQues.Length - 1)
            {
                PlaySound(PlayingSoundIndex);
            }
            else
            {
                PlaySound(PlayingSoundIndex + 1);
            }
        }
        public static void SkipBackwards()
        {
            if (PlayingSoundIndex is 0)
            {
                Replay();
            }
            else
            {
                PlaySound(PlayingSoundIndex - 1);
            }
        }
        public static void PlaySound(int index)
        {
            if (index < 0 || index >= SoundQues.Length)
            {
                throw new Exception("index must be greater than or equal to 0 and less than sound count.");
            }
            if (PlayingSoundIndex != -1)
            {
                SoundQues[PlayingSoundIndex].Playing = false;
                SoundQues[PlayingSoundIndex].StreamPosition = 0;
            }
            PlayingSoundIndex = index;
            SoundQues[PlayingSoundIndex].audioFileReader.Position = 0;
            SoundQues[PlayingSoundIndex].waveOutputDevice.Play();
        }
        public static void Stop()
        {
            SoundQues[PlayingSoundIndex].Playing = false;
            SoundQues[PlayingSoundIndex].StreamPosition = 0;
        }
    }
}
