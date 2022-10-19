namespace YAGMCBSoundPanel
{
    public sealed class SoundPanel
    {
        #region Internal Variables
        internal int _currentAudioClipIndex = 0;
        internal AudioClip[] _audioClips = null;
        internal AudioPlayer _audioPlayer = null;

        internal System.Drawing.Bitmap _autoPlayDisabledImage = null;
        internal System.Drawing.Bitmap _autoPlayEnabledImage = null;
        internal System.Drawing.Bitmap _mutedImage = null;
        internal System.Drawing.Bitmap _pauseImage = null;
        internal System.Drawing.Bitmap _playImage = null;
        internal System.Drawing.Bitmap _replayImage = null;
        internal System.Drawing.Bitmap _skipNextImage = null;
        internal System.Drawing.Bitmap _skipPreviousImage = null;
        internal System.Drawing.Bitmap _stopImage = null;
        internal System.Drawing.Bitmap _unmutedImage = null;

        internal System.Windows.Forms.Timer _updateTimer = null;

        internal System.Windows.Forms.Form _form = null;

        internal ScaledButton _skipPreviousButton = null;
        internal ScaledButton _pauseButton = null;
        internal ScaledButton _skipNextButton = null;
        internal ScaledButton _replayButton = null;
        internal ScaledButton _stopButton = null;
        internal ScaledButton _muteButton = null;
        internal ScaledButton _autoPlayButton = null;

        internal ScaledSlider _progressSlider = null;

        internal ScaledSlider _volumeSlider = null;

        internal ScaledLabel _currentTrackInfoLabel = null;

        internal bool _autoPlayEnabled = false;
        #endregion
        #region Public Constructors
        public SoundPanel(AudioClip[] audioClips)
        {
            if (audioClips is null)
            {
                throw new System.Exception("audioClips cannot be null.");
            }
            else if (audioClips.Length is 0)
            {
                throw new System.Exception("audioClips cannot be empty.");
            }

            _audioClips = audioClips;

            _audioPlayer = new AudioPlayer();
            _audioPlayer.Volume = 0.5;
            _audioPlayer.LoadAudioClip(audioClips[0]);

            _autoPlayDisabledImage = new System.Drawing.Bitmap("Icons\\AutoPlayDisabled.bmp");
            _autoPlayEnabledImage = new System.Drawing.Bitmap("Icons\\AutoPlayEnabled.bmp");
            _mutedImage = new System.Drawing.Bitmap("Icons\\Muted.bmp");
            _pauseImage = new System.Drawing.Bitmap("Icons\\Pause.bmp");
            _playImage = new System.Drawing.Bitmap("Icons\\Play.bmp");
            _replayImage = new System.Drawing.Bitmap("Icons\\Replay.bmp");
            _skipNextImage = new System.Drawing.Bitmap("Icons\\SkipNext.bmp");
            _skipPreviousImage = new System.Drawing.Bitmap("Icons\\SkipPrevious.bmp");
            _stopImage = new System.Drawing.Bitmap("Icons\\Stop.bmp");
            _unmutedImage = new System.Drawing.Bitmap("Icons\\Unmuted.bmp");

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 25;
            _updateTimer.Tick += OnUpdateTimerTick;

            _form = new System.Windows.Forms.Form();
            _form.TransparencyKey = System.Drawing.Color.Transparent;

            _skipPreviousButton = new ScaledButton();
            _skipPreviousButton.XMin = 0.0 / 15.0;
            _skipPreviousButton.YMin = 0.0;
            _skipPreviousButton.XMax = 2.0 / 15.0;
            _skipPreviousButton.YMax = 0.15;
            _skipPreviousButton.Image = _skipPreviousImage;
            _skipPreviousButton.ButtonClickedEvent = OnSkipPreviousButtonClicked;
            _form.Controls.Add(_skipPreviousButton);

            _pauseButton = new ScaledButton();
            _pauseButton.XMin = 2.0 / 15.0;
            _pauseButton.YMin = 0.0;
            _pauseButton.XMax = 4.0 / 15.0;
            _pauseButton.YMax = 0.15;
            _pauseButton.Image = _playImage;
            _pauseButton.ButtonClickedEvent = OnPauseButtonClicked;
            _form.Controls.Add(_pauseButton);

            _skipNextButton = new ScaledButton();
            _skipNextButton.XMin = 4.0 / 15.0;
            _skipNextButton.YMin = 0.0;
            _skipNextButton.XMax = 6.0 / 15.0;
            _skipNextButton.YMax = 0.15;
            _skipNextButton.Image = _skipNextImage;
            _skipNextButton.ButtonClickedEvent = OnSkipNextButtonClicked;
            _form.Controls.Add(_skipNextButton);

            _replayButton = new ScaledButton();
            _replayButton.XMin = 6.0 / 15.0;
            _replayButton.YMin = 0.0;
            _replayButton.XMax = 8.0 / 15.0;
            _replayButton.YMax = 0.15;
            _replayButton.Image = _replayImage;
            _replayButton.ButtonClickedEvent = OnReplayButtonClicked;
            _form.Controls.Add(_replayButton);

            _stopButton = new ScaledButton();
            _stopButton.XMin = 8.0 / 15.0;
            _stopButton.YMin = 0.0;
            _stopButton.XMax = 10.0 / 15.0;
            _stopButton.YMax = 0.15;
            _stopButton.Image = _stopImage;
            _stopButton.ButtonClickedEvent = OnStopButtonClicked;
            _form.Controls.Add(_stopButton);

            _muteButton = new ScaledButton();
            _muteButton.XMin = 10.0 / 15.0;
            _muteButton.YMin = 0.0;
            _muteButton.XMax = 12.0 / 15.0;
            _muteButton.YMax = 0.15;
            _muteButton.Image = _unmutedImage;
            _muteButton.ButtonClickedEvent = OnMuteButtonClicked;
            _form.Controls.Add(_muteButton);

            _autoPlayButton = new ScaledButton();
            _autoPlayButton.XMin = 12.0 / 15.0;
            _autoPlayButton.YMin = 0.0;
            _autoPlayButton.XMax = 14.0 / 15.0;
            _autoPlayButton.YMax = 0.15;
            _autoPlayButton.Image = _autoPlayDisabledImage;
            _autoPlayButton.ButtonClickedEvent = OnAutoPlayButtonClicked;
            _form.Controls.Add(_autoPlayButton);

            _volumeSlider = new ScaledSlider();
            _volumeSlider.XMin = 14.0 / 15.0;
            _volumeSlider.YMin = 0.0;
            _volumeSlider.XMax = 15.0 / 15.0;
            _volumeSlider.YMax = 0.15;
            _volumeSlider.SliderBackgroundColor = System.Drawing.Color.DarkGray;
            _volumeSlider.SliderForegroundColor = System.Drawing.Color.CornflowerBlue;
            _volumeSlider.SliderDirection = SliderDirection.BottomToTop;
            _volumeSlider.SliderClickedEvent = OnVolumeSliderClicked;
            _form.Controls.Add(_volumeSlider);

            _progressSlider = new ScaledSlider();
            _progressSlider.XMin = 0;
            _progressSlider.YMin = 0.15;
            _progressSlider.XMax = 1;
            _progressSlider.YMax = 0.1875;
            _progressSlider.SliderBackgroundColor = System.Drawing.Color.DarkGray;
            _progressSlider.SliderForegroundColor = System.Drawing.Color.CornflowerBlue;
            _progressSlider.SliderDirection = SliderDirection.LeftToRight;
            _progressSlider.SliderClickedEvent = OnProgressSliderClicked;
            _form.Controls.Add(_progressSlider);

            _currentTrackInfoLabel = new ScaledLabel();
            _currentTrackInfoLabel.XMin = 0;
            _currentTrackInfoLabel.YMin = 0.1875;
            _currentTrackInfoLabel.XMax = 1;
            _currentTrackInfoLabel.YMax = 0.225;
            _currentTrackInfoLabel.BackColor = System.Drawing.Color.Transparent;
            _currentTrackInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _currentTrackInfoLabel.Text = _audioClips[_currentAudioClipIndex].SourceFileNameWithoutExtension;
            _currentTrackInfoLabel.AutoSize = false;
            _currentTrackInfoLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, _currentTrackInfoLabel.Height, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            _form.Controls.Add(_currentTrackInfoLabel);

            Update();
        }
        #endregion
        #region Public Methods
        public void Run()
        {
            _updateTimer.Start();
            System.Windows.Forms.Application.Run(_form);
        }
        #endregion
        #region Internal Methods
        internal void OnUpdateTimerTick(object sender, System.EventArgs e)
        {
            Update();
        }
        internal void Update()
        {
            _currentTrackInfoLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, _currentTrackInfoLabel.Height, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            _currentTrackInfoLabel.Text = _audioClips[_currentAudioClipIndex].SourceFileNameWithoutExtension;

            _progressSlider._value = _audioPlayer.StreamPosition / (double)_audioClips[_currentAudioClipIndex].StreamLength;
            _progressSlider.Invalidate();

            _volumeSlider._value = _audioPlayer.Volume;
            _volumeSlider.Invalidate();

            if (_audioPlayer.StreamPosition == _audioClips[_currentAudioClipIndex].StreamLength)
            {
                if (_autoPlayEnabled)
                {
                    _currentAudioClipIndex++;
                    _audioPlayer.Play(_audioClips[_currentAudioClipIndex]);
                }
                else
                {
                    _audioPlayer.Stop();
                }
            }
            if (_audioPlayer.Paused)
            {
                _pauseButton.Image = _playImage;
            }
            else
            {
                _pauseButton.Image = _pauseImage;
            }
            if (_audioPlayer._muted)
            {
                _muteButton.Image = _mutedImage;
            }
            else
            {
                _muteButton.Image = _unmutedImage;
            }
            if (_autoPlayEnabled)
            {
                _autoPlayButton.Image = _autoPlayEnabledImage;
            }
            else
            {
                _autoPlayButton.Image = _autoPlayDisabledImage;
            }
        }
        internal void OnSkipPreviousButtonClicked()
        {
            if (_currentAudioClipIndex > 0)
            {
                _currentAudioClipIndex--;
                _audioPlayer.Play(_audioClips[_currentAudioClipIndex]);
            }
            else
            {
                _audioPlayer.Stop();
            }
            Update();
        }
        internal void OnPauseButtonClicked()
        {
            _audioPlayer.TogglePaused();
            Update();
        }
        internal void OnSkipNextButtonClicked()
        {
            if (_currentAudioClipIndex < _audioClips.Length - 1)
            {
                _currentAudioClipIndex++;
                _audioPlayer.Play(_audioClips[_currentAudioClipIndex]);
            }
            else
            {
                _audioPlayer.Stop();
            }
            Update();
        }
        internal void OnReplayButtonClicked()
        {
            _audioPlayer.Replay();
            Update();
        }
        internal void OnStopButtonClicked()
        {
            _audioPlayer.Stop();
            Update();
        }
        internal void OnMuteButtonClicked()
        {
            _audioPlayer.ToggleMute();
            Update();
        }
        internal void OnAutoPlayButtonClicked()
        {
            _autoPlayEnabled = !_autoPlayEnabled;
            Update();
        }
        internal void OnProgressSliderClicked(double value)
        {
            _audioPlayer.Seek((long)(_audioClips[_currentAudioClipIndex].StreamLength * value));
            Update();
        }
        internal void OnVolumeSliderClicked(double value)
        {
            _audioPlayer.Volume = value;
            Update();
        }
        #endregion
    }
}