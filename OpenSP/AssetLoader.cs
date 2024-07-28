namespace OpenSP
{
    public static class AssetLoader
    {
        private static System.Collections.Generic.List<System.Reflection.Assembly> _loadedAssemblies = GetLoadedAssemblies();
        private static System.Collections.Generic.List<System.Reflection.Assembly> GetLoadedAssemblies()
        {
            return new System.Collections.Generic.List<System.Reflection.Assembly>(new System.Reflection.Assembly[1] { typeof(AssetLoader).Assembly });
        }
        public static void LoadAssembly(System.Reflection.Assembly assembly)
        {
            foreach (System.Reflection.Assembly loadedAssembly in _loadedAssemblies)
            {
                if (assembly == loadedAssembly)
                {
                    return;
                }
            }
            _loadedAssemblies.Add(assembly);
        }
        public static bool UnloadAssembly(System.Reflection.Assembly assembly)
        {
            return _loadedAssemblies.Remove(assembly);
        }
        public static System.Drawing.Bitmap LoadAsset(string assetFileName)
        {
            string assetFileNameToLower = assetFileName.ToLower();
            if (assetFileNameToLower.Contains("."))
            {
                foreach (System.Reflection.Assembly loadedAssembly in _loadedAssemblies)
                {
                    foreach (string manifestResourceName in loadedAssembly.GetManifestResourceNames())
                    {
                        if (manifestResourceName.ToLower().EndsWith(assetFileNameToLower))
                        {
                            return new System.Drawing.Bitmap(loadedAssembly.GetManifestResourceStream(manifestResourceName));
                        }
                    }
                }
                throw new System.Exception($"Unable to find asset with name \"{assetFileName}\". Try loading another assembly.");
            }
            else
            {
                foreach (System.Reflection.Assembly loadedAssembly in _loadedAssemblies)
                {
                    foreach (string manifestResourceName in loadedAssembly.GetManifestResourceNames())
                    {
                        string manifestResourceNameToLower = manifestResourceName.ToLower();
                        int dotIndex = manifestResourceNameToLower.LastIndexOf(".");
                        if(dotIndex is -1)
                        {
                            if (manifestResourceNameToLower.EndsWith(assetFileNameToLower))
                            {
                                return new System.Drawing.Bitmap(loadedAssembly.GetManifestResourceStream(manifestResourceName));
                            }
                        }
                        else
                        {
                            var a = manifestResourceNameToLower.Substring(0, dotIndex);
                            if (a.EndsWith(assetFileNameToLower))
                            {
                                return new System.Drawing.Bitmap(loadedAssembly.GetManifestResourceStream(manifestResourceName));
                            }
                        }
                    }
                }
                throw new System.Exception($"Unable to find asset with name \"{assetFileName}\". Try loading another assembly.");
            }
        }
    }
}