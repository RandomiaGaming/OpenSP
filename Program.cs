using System;

namespace YAGMCBSoundPanel
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            System.Reflection.Assembly assembly = typeof(Program).Assembly;
            string assemblyLocation = assembly.Location;
            string assemblyDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            string soundQuesDirectory = assemblyDirectory + "\\YAGMCB Soundtrack\\YAGMCB Performance Soundtrack";
            SoundPanel soundPanel = new SoundPanel(soundQuesDirectory);
            soundPanel.Run();
            return 0;
        }
    }
}
