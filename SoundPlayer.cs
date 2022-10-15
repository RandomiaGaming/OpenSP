using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGMCBSoundPanel
{
    public sealed class SoundPlayer
    {
        public bool Playing
        {
            get
            {
                return waveOutputDevice.PlaybackState == PlaybackState.Playing;
            }
            set
            {
                if (value)
                {
                    if (!(waveOutputDevice.PlaybackState is PlaybackState.Playing))
                    {
                        waveOutputDevice.Play();
                    }
                }
                else
                {
                    if (waveOutputDevice.PlaybackState is PlaybackState.Playing)
                    {
                        waveOutputDevice.Pause();
                    }
                }
            }
        }
        public TimeSpan Position
        {
            get
            {
                return audioFileReader.CurrentTime;
            }
            set
            {
                audioFileReader.CurrentTime = value;
            }
        }
        public long StreamPosition
        {
            get
            {
                return audioFileReader.Position;
            }
            set
            {
                audioFileReader.Position = value;
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return audioFileReader.TotalTime;
            }
        }
        public long StreamLength
        {
            get
            {
                return audioFileReader.Length;
            }
        }
        public float Volume
        {
            get
            {
                return waveOutputDevice.Volume;
            }
            set
            {
                waveOutputDevice.Volume = value;
            }
        }

        public WaveOut waveOutputDevice;
        public AudioFileReader audioFileReader;
        public SoundPlayer(string filePath)
        {
            waveOutputDevice = new WaveOut();
            audioFileReader = new AudioFileReader(filePath);
            waveOutputDevice.Init(audioFileReader);
            waveOutputDevice.Play();
            waveOutputDevice.Pause();
            audioFileReader.Position = 0;
        }
    }
}