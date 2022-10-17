using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YAGMCBSoundPanel
{
    public sealed class SoundPanel
    {
        public string SoundQueDirectory;
        public SoundQue[] SoundQues;
        public int CurrentSoundQueIndex = 0;
        public WaveOut WaveOutputDevice;
        public SoundPanel(string soundQueDirectory)
        {
            Console.WriteLine("Initializing Sound Panel...");
            SoundQueDirectory = soundQueDirectory;
            string[] soundQueFilePaths = Directory.GetFiles(SoundQueDirectory);
            SoundQues = new SoundQue[soundQueFilePaths.Length - 80];
            for (int i = 0; i < SoundQues.Length; i++)
            {
                SoundQues[i] = new SoundQue(soundQueFilePaths[i]);
            }
            WaveOutputDevice = new WaveOut();
            WaveOutputDevice.Init(SoundQues[0].RawSourceWaveStream);
            Console.WriteLine("Initialized Sound Panel.");

            Form player = new Form();

            Button PlayPauseButton = new Button();

            Application.Run(player);
        }
        public void Run()
        {
            while (true)
            {
                string command = Console.ReadLine();

            }
        }
        public void Stop()
        {
            WaveOutputDevice.Stop();
            SoundQues[CurrentSoundQueIndex].RawSourceWaveStream.Position = 0;
            Console.WriteLine(SoundQues[CurrentSoundQueIndex].ToString());
        }
        public void PlayPause()
        {

        }
        public void SkipNext()
        {

        }
        public void SkipPrevious()
        {

        }
        public void Replay()
        {

        }
        public void Play(int soundQueIndex)
        {

        }
        public void ToggleAutoPlay()
        {

        }
        public void ToggleMute()
        {

        }
        public void Seek(long position)
        {

        }
    }
}