using System.Windows.Forms;

namespace YAGMCBSoundPanel
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            System.Console.WriteLine("Initializing Sound Panel...");
            System.Reflection.Assembly assembly = typeof(Program).Assembly;
            string assemblyLocation = assembly.Location;
            string assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            string audioClipDirectory = assemblyDirectory + "\\YAGMCB Soundtrack\\YAGMCB Performance Soundtrack";
            string[] audioClipFilePaths = System.IO.Directory.GetFiles(audioClipDirectory);
            AudioClip[] audioClips = new AudioClip[audioClipFilePaths.Length - 80];
            for (int i = 0; i < audioClips.Length; i++)
            {
                audioClips[i] = new AudioClip(audioClipFilePaths[i]);
            }
            SoundPanel soundPanel = new SoundPanel(audioClips);
            System.Console.WriteLine("Initialized Sound Panel.");
            soundPanel.Run();
            return 0;
        }
    }
}
