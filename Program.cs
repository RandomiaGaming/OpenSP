namespace YAGMCBSoundPanel
{
    public static class Program
    {
        [System.STAThread()]
        public static void Main()
        {
            System.Reflection.Assembly assembly = typeof(Program).Assembly;
            string assemblyLocation = assembly.Location;
            string assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            string audioClipDirectory = assemblyDirectory + "\\YAGMCB Soundtrack\\YAGMCB Performance Soundtrack";
            string[] audioClipFilePaths = System.IO.Directory.GetFiles(audioClipDirectory);
            AudioClip[] audioClips = new AudioClip[audioClipFilePaths.Length];
            for (int i = 0; i < audioClips.Length; i++)
            {
                audioClips[i] = new AudioClip(audioClipFilePaths[i]);
            }
            SoundPanel soundPanel = new SoundPanel(audioClips);
            soundPanel.Run();
        }
    }
}
