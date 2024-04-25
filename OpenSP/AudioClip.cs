namespace OpenSP
{
    public sealed class AudioClip : System.IDisposable
    {
        #region Public Variables
        public readonly string SourceFilePath = null;
        public readonly string SourceDirectoryPath = null;
        public readonly string SourceFileName = null;
        public readonly string SourceFileNameWithoutExtension = null;
        public int ChannelCount { get { return _nAudioWaveStream.WaveFormat.Channels; } }
        public int BitsPerSample { get { return _nAudioWaveStream.WaveFormat.BitsPerSample; } }
        public int SampleRate { get { return _nAudioWaveStream.WaveFormat.SampleRate; } }
        public long StreamLength { get { return _nAudioWaveStream.Length; } }
        public System.TimeSpan Length { get { return _nAudioWaveStream.TotalTime; } }
        public bool Disposed { get; private set; }
        public bool Bound { get { return !(BoundAudioPlayer is null); } }
        public AudioPlayer BoundAudioPlayer { get; private set; }
        #endregion
        #region Internal Variables
        internal object _bindingLockObject = new object();
        internal byte[] _rawData = null;
        internal System.IO.MemoryStream _rawDataStream = null;
        internal NAudio.Wave.RawSourceWaveStream _nAudioWaveStream = null;
        #endregion
        #region Public Constructors
        public AudioClip(string sourceFilePath)
        {
            if (sourceFilePath is null)
            {
                throw new System.Exception("filePath cannot be null.");
            }
            if (!System.IO.File.Exists(sourceFilePath))
            {
                throw new System.Exception("filePath does not exist.");
            }
            if (sourceFilePath.EndsWith(".osp"))
            {
                SourceFilePath = sourceFilePath;
                SourceDirectoryPath = System.IO.Path.GetDirectoryName(sourceFilePath);
                SourceFileName = System.IO.Path.GetFileName(sourceFilePath);
                SourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath);
                try
                {
                    System.IO.FileStream fileStream = System.IO.File.Open(SourceFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    _rawDataStream = new System.IO.MemoryStream((int)fileStream.Length);
                    fileStream.CopyTo(_rawDataStream);
                    fileStream.Dispose();
                    _nAudioWaveStream = new NAudio.Wave.RawSourceWaveStream(_rawDataStream, new NAudio.Wave.WaveFormat(44100, 32, 2));
                }
                catch
                {
                    throw new System.Exception("Could not load audio clip from file. Possibly file is corrupted or access was denied.");
                }
            }
            else
            {
                SourceFilePath = sourceFilePath;
                SourceDirectoryPath = System.IO.Path.GetDirectoryName(sourceFilePath);
                SourceFileName = System.IO.Path.GetFileName(sourceFilePath);
                SourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFilePath);
                try
                {
                    NAudio.Wave.AudioFileReader audioFileReader = new NAudio.Wave.AudioFileReader(SourceFilePath);
                    _rawData = new byte[audioFileReader.Length];
                    audioFileReader.Read(_rawData, 0, _rawData.Length);
                    audioFileReader.Dispose();
                    _rawDataStream = new System.IO.MemoryStream(_rawData, 0, _rawData.Length, false, false);
                    _nAudioWaveStream = new NAudio.Wave.RawSourceWaveStream(_rawDataStream, audioFileReader.WaveFormat);
                }
                catch
                {
                    throw new System.Exception("Could not load audio clip from file. Possibly file is corrupted or access was denied.");
                }
            }
        }
        #endregion
        #region Public Methods
        public void Dispose()
        {
            if (Bound)
            {
                throw new System.Exception("Cannot dispose AudioClip while it is bound to an AudioPlayer.");
            }
            if (Disposed)
            {
                throw new System.Exception("AudioClip has already been disposed.");
            }
            _nAudioWaveStream.Dispose();
            _rawDataStream.Dispose();
            _rawData = null;
            Disposed = true;
        }
        #endregion
        #region Internal Methods
        internal void Bind(AudioPlayer bindingTarget)
        {
            if(bindingTarget is null)
            {
                throw new System.Exception("bindingTarget cannot be null.");
            }
            lock (_bindingLockObject)
            {
                if(BoundAudioPlayer == bindingTarget)
                {
                    return;
                }
                if (Bound)
                {
                    throw new System.Exception("Cannot bind to AudioClip because it is already bound to another AudioPlayer.");
                }
                BoundAudioPlayer = bindingTarget;
            }
        }
        internal void Unbind()
        {
            lock (_bindingLockObject)
            {
                BoundAudioPlayer = null;
                _nAudioWaveStream.Position = 0;
            }
        }
        #endregion
    }
}