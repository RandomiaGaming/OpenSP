namespace YAGMCBSoundPanel
{
    public sealed class SoundQue
    {
        public string FullName;
        public int TrackNumber;
        public int SongNumber;
        public int PartNumber;
        public string Name;
        public NAudio.Wave.RawSourceWaveStream RawSourceWaveStream;
        public SoundQue(string filePath)
        {
            FullName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string[] dataEntries = FullName.Split('-');
            TrackNumber = int.Parse(dataEntries[0].Substring(6, 2));
            SongNumber = int.Parse(dataEntries[1].Substring(6, 2));
            PartNumber = int.Parse(dataEntries[2].Substring(6, 1));
            Name = dataEntries[3].Substring(1);
            NAudio.Wave.AudioFileReader audioFileReader = new NAudio.Wave.AudioFileReader(filePath);
            byte[] audioData = new byte[audioFileReader.Length];
            audioFileReader.Read(audioData, 0, audioData.Length);
            audioFileReader.Dispose();
            System.IO.MemoryStream internalAudioDataStream = new System.IO.MemoryStream(audioData);
            RawSourceWaveStream = new NAudio.Wave.RawSourceWaveStream(internalAudioDataStream, audioFileReader.WaveFormat);
        }
        public override string ToString()
        {
            return $"{FullName} - {RawSourceWaveStream.TotalTime.ToString("m\\:ss")}";
        }
    }
}