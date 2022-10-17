using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGMCBSoundPanel
{
    public sealed class AudioClip
    {
        /*public int a => NAudioRawSourceWaveStream.BlockAlign;
        public NAudio.Wave.RawSourceWaveStream NAudioRawSourceWaveStream;
        public AudioClip(string filePath)
        {
            NAudioRawSourceWaveStream.
            NAudio.Wave.AudioFileReader audioFileReader = new NAudio.Wave.AudioFileReader(filePath);
            byte[] audioData = new byte[audioFileReader.Length];
            audioFileReader.Read(audioData, 0, audioData.Length);
            audioFileReader.Dispose();
            System.IO.MemoryStream internalAudioDataStream = new System.IO.MemoryStream(audioData);
            NAudioRawSourceWaveStream = new NAudio.Wave.RawSourceWaveStream(internalAudioDataStream, audioFileReader.WaveFormat);
        }
        public override string ToString()
        {
            return $"{FullName} - {NAudioRawSourceWaveStream.TotalTime.ToString("m\\:ss")}";
        }*/
    }
}
