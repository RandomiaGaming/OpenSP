namespace OpenSP
{
    public delegate void SoundPlayerEvent();
    public sealed class AudioPlayer
    {
        #region Public Variables
        public bool Paused
        {
            get
            {
                return !(_nAudioWaveOutputDevice.PlaybackState is NAudio.Wave.PlaybackState.Playing);
            }
            set
            {
                if (value)
                {
                    Pause();
                }
                else
                {
                    Play();
                }
            }
        }
        public bool Muted
        {
            get
            {
                return _muted;
            }
            set
            {
                if (value)
                {
                    Mute();
                }
                else
                {
                    Unmute();
                }
            }
        }
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                SetVolume(value);
            }
        }
        public AudioClip LoadedAudioClip
        {
            get
            {
                return _loadedAudioClip;
            }
            set
            {
                LoadAudioClip(value);
            }
        }
        public long StreamPosition
        {
            get
            {
                return GetStreamPosition();
            }
            set
            {
                Seek(value);
            }
        }
        public System.TimeSpan Position
        {
            get
            {
                return GetPosition();
            }
            set
            {
                Seek(value);
            }
        }
        #endregion
        #region Internal Variables
        internal bool _muted = false;
        internal double _volume = 1.0;
        internal AudioClip _loadedAudioClip = null;
        internal NAudio.Wave.WaveOut _nAudioWaveOutputDevice = null;
        #endregion
        #region Public Constructors
        public AudioPlayer()
        {
            _nAudioWaveOutputDevice = new NAudio.Wave.WaveOut();
            _nAudioWaveOutputDevice.DeviceNumber = 0;
        }
        #endregion
        #region Public Methods
        public void Play()
        {
            if (_loadedAudioClip is null)
            {
                throw new System.Exception("Cannot play when LoadedAudioClip is null.");
            }
            else
            {
                _nAudioWaveOutputDevice.Play();
            }
        }
        public void Pause()
        {
            _nAudioWaveOutputDevice.Pause();
        }
        public void Unmute()
        {
            _muted = false;
            _nAudioWaveOutputDevice.Volume = (float)_volume;
        }
        public void Mute()
        {
            _muted = true;
            _nAudioWaveOutputDevice.Volume = 0.0f;
        }
        public void SetVolume(double volume)
        {
            _volume = volume;
            if (!_muted)
            {
                _nAudioWaveOutputDevice.Volume = (float)_volume;
            }
        }
        public void LoadAudioClip(AudioClip audioClip)
        {
            UnloadAudioClip();
            audioClip.Bind(this);
            _nAudioWaveOutputDevice.Init(audioClip._nAudioWaveStream);
            _loadedAudioClip = audioClip;
        }
        public void Seek(long streamPosition)
        {
            if (_loadedAudioClip is null)
            {
                throw new System.Exception("Cannot play when LoadedAudioClip is null.");
            }
            else
            {
                if (streamPosition < 0 || streamPosition >= _loadedAudioClip._nAudioWaveStream.Length)
                {
                    throw new System.Exception("StreamPosition must be between 0 and LoadedAudioClip.Length.");
                }
                _loadedAudioClip._nAudioWaveStream.Position = streamPosition;
            }
        }
        public long GetStreamPosition()
        {
            if (_loadedAudioClip is null)
            {
                return 0;
            }
            else
            {
                return _loadedAudioClip._nAudioWaveStream.Position;
            }
        }
        public System.TimeSpan GetPosition()
        {
            if (_loadedAudioClip is null)
            {
                return new System.TimeSpan(0);
            }
            else
            {
                return _loadedAudioClip._nAudioWaveStream.CurrentTime;
            }
        }
        public void Seek(System.TimeSpan position)
        {
            if (_loadedAudioClip is null)
            {
                throw new System.Exception("Cannot play when LoadedAudioClip is null.");
            }
            else
            {
                if (position.Ticks < 0 || position >= _loadedAudioClip._nAudioWaveStream.TotalTime)
                {
                    throw new System.Exception("StreamPosition must be between 0 and LoadedAudioClip.Duration.");
                }
                _loadedAudioClip._nAudioWaveStream.CurrentTime = position;
            }
        }
        public void Stop()
        {
            _nAudioWaveOutputDevice.Stop();
            if (!(_loadedAudioClip is null))
            {
                Seek(0);
            }
        }
        public void Replay()
        {
            if (!(_loadedAudioClip is null))
            {
                Play(_loadedAudioClip);
            }
        }
        public void UnloadAudioClip()
        {
            _nAudioWaveOutputDevice.Stop();
            if (!(_loadedAudioClip is null))
            {
                _loadedAudioClip.Unbind();
            }
            _loadedAudioClip = null;
        }
        public void ToggleMute()
        {
            if (_muted)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
        }
        public void TogglePaused()
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
        public void Play(AudioClip audioClip)
        {
            if (audioClip is null)
            {
                throw new System.Exception("Cannot play null.");
            }
            LoadAudioClip(audioClip);
            Play();
        }
        #endregion
    }
}