namespace OpenSP
{
    public static class Program
    {
        [System.STAThread()]
        public static void Main()
        {
            System.Diagnostics.Stopwatch stopwatch =  new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            System.Reflection.Assembly assembly = typeof(Program).Assembly;
            string assemblyLocation = assembly.Location;
            string assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            string audioClipDirectory = "D:\\Media Archive\\Media Archive Root\\Important Memories\\Matilda\\Guide Vocals Lossless";
            string[] audioClipFilePaths = System.IO.Directory.GetFiles(audioClipDirectory);
            AudioClip[] audioClips = new AudioClip[audioClipFilePaths.Length];
            for (int i = 0; i < audioClips.Length; i++)
            {
                audioClips[i] = new AudioClip(audioClipFilePaths[i]);
            }
            SoundPanel soundPanel = new SoundPanel(audioClips);
            stopwatch.Stop();
            System.IO.File.WriteAllText("C:\\Users\\RandomiaGaming\\Desktop\\Output.txt", stopwatch.ElapsedTicks.ToString());
            soundPanel.Run();
        }
    }
}