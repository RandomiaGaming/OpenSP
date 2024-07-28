using ScaleForms;
namespace OpenSP
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
        internal System.Drawing.Bitmap _resetVolumeImage = null;
        internal System.Drawing.Bitmap _unmutedImage = null;

        internal System.Windows.Forms.Timer _updateTimer = null;

        internal System.Windows.Forms.Form _form = null;

        internal ScaledButton _skipPreviousButton = null;
        internal ScaledButton _pauseButton = null;
        internal ScaledButton _skipNextButton = null;
        internal ScaledButton _replayButton = null;
        internal ScaledButton _resetVolumeButton = null;
        internal ScaledButton _muteButton = null;
        internal ScaledButton _autoPlayButton = null;

        internal ScaledSlider _progressSlider = null;
        internal ScaledSlider _volumeSlider = null;

        internal ScaledLabel _currentTrackInfoLabel = null;
        internal ScaledLabel _keybindInstructionsLabel = null;

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

            _autoPlayDisabledImage = AssetLoader.LoadAsset("AutoPlayDisabled");
            _autoPlayEnabledImage = AssetLoader.LoadAsset("AutoPlayEnabled");
            _mutedImage = AssetLoader.LoadAsset("Muted");
            _pauseImage = AssetLoader.LoadAsset("Pause");
            _playImage = AssetLoader.LoadAsset("Play");
            _replayImage = AssetLoader.LoadAsset("Replay");
            _skipNextImage = AssetLoader.LoadAsset("SkipNext");
            _skipPreviousImage = AssetLoader.LoadAsset("SkipPrevious");
            _resetVolumeImage = AssetLoader.LoadAsset("ResetVolume");
            _unmutedImage = AssetLoader.LoadAsset("Unmuted");

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 100;
            _updateTimer.Tick += OnUpdateTimerTick;

            _form = new System.Windows.Forms.Form();
            _form.KeyDown += OnKeyDownEvent;

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

            _muteButton = new ScaledButton();
            _muteButton.XMin = 8.0 / 15.0;
            _muteButton.YMin = 0.0;
            _muteButton.XMax = 10.0 / 15.0;
            _muteButton.YMax = 0.15;
            _muteButton.Image = _unmutedImage;
            _muteButton.ButtonClickedEvent = OnMuteButtonClicked;
            _form.Controls.Add(_muteButton);

            _autoPlayButton = new ScaledButton();
            _autoPlayButton.XMin = 10.0 / 15.0;
            _autoPlayButton.YMin = 0.0;
            _autoPlayButton.XMax = 12.0 / 15.0;
            _autoPlayButton.YMax = 0.15;
            _autoPlayButton.Image = _autoPlayDisabledImage;
            _autoPlayButton.ButtonClickedEvent = OnAutoPlayButtonClicked;
            _form.Controls.Add(_autoPlayButton);

            _resetVolumeButton = new ScaledButton();
            _resetVolumeButton.XMin = 12.0 / 15.0;
            _resetVolumeButton.YMin = 0.0;
            _resetVolumeButton.XMax = 14.0 / 15.0;
            _resetVolumeButton.YMax = 0.15;
            _resetVolumeButton.Image = _resetVolumeImage;
            _resetVolumeButton.ButtonClickedEvent = OnResetVolumeButtonClicked;
            _form.Controls.Add(_resetVolumeButton);

            _volumeSlider = new ScaledSlider();
            _volumeSlider.XMin = 14.0 / 15.0;
            _volumeSlider.YMin = 0.01;
            _volumeSlider.XMax = 15.0 / 15.0;
            _volumeSlider.YMax = 0.14;
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
            _currentTrackInfoLabel.YMax = 0.2625;
            _currentTrackInfoLabel.FontSize = 0.75;
            _currentTrackInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _currentTrackInfoLabel.Text = _audioClips[_currentAudioClipIndex].SourceFileNameWithoutExtension;
            _currentTrackInfoLabel.AutoSize = false;
            _currentTrackInfoLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, _currentTrackInfoLabel.Height, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            _form.Controls.Add(_currentTrackInfoLabel);

            _keybindInstructionsLabel = new ScaledLabel();
            _keybindInstructionsLabel.XMin = 0;
            _keybindInstructionsLabel.YMin = 0.2625;
            _keybindInstructionsLabel.XMax = 1;
            _keybindInstructionsLabel.YMax = 1;
            _keybindInstructionsLabel.FontSize = 0.05;
            _keybindInstructionsLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            _keybindInstructionsLabel.Text = "Pause/Play: Space\nReplay: R\nNext Audio Clip: D\nPrevious Audio Clip: A\nSkip Forward: Shift + D\nSkip Back: Shift + A\nVolume Up: W\nVolume Down: S\nVolume Max: Shift + W\nVolume Min: Shift + S\nReplay: R\nReset Volume: Shift R\nAutoPlay On/Off: Shift + Space\nMute/Unmute: M";
            _form.Controls.Add(_keybindInstructionsLabel);

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
            _currentTrackInfoLabel.Text = _audioClips[_currentAudioClipIndex].SourceFileNameWithoutExtension;

            _progressSlider._value = _audioPlayer.StreamPosition / (double)_audioClips[_currentAudioClipIndex].StreamLength;
            _progressSlider.Invalidate();

            _volumeSlider._value = _audioPlayer._volume;
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
        internal void OnResetVolumeButtonClicked()
        {
            _audioPlayer.Volume = 0.5;
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
        internal void OnKeyDownEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode is System.Windows.Forms.Keys.Space)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    OnAutoPlayButtonClicked();
                }
                else
                {
                    OnPauseButtonClicked();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.D || e.KeyCode is System.Windows.Forms.Keys.Right)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    System.TimeSpan newPosition = _audioPlayer.Position + new System.TimeSpan(0, 0, 10);
                    if (newPosition.Ticks >= _audioClips[_currentAudioClipIndex].Length.Ticks)
                    {
                        _audioPlayer.Seek(_audioClips[_currentAudioClipIndex].Length - new System.TimeSpan(1));
                    }
                    else
                    {
                        _audioPlayer.Seek(newPosition);
                    }
                    Update();
                }
                else
                {
                    OnSkipNextButtonClicked();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.A || e.KeyCode is System.Windows.Forms.Keys.Left)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    System.TimeSpan newPosition = _audioPlayer.Position - new System.TimeSpan(0, 0, 10);
                    if (newPosition.Ticks < 0)
                    {
                        _audioPlayer.Seek(0);
                    }
                    else
                    {
                        _audioPlayer.Seek(newPosition);
                    }
                    Update();
                }
                else
                {
                    OnSkipPreviousButtonClicked();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.W || e.KeyCode is System.Windows.Forms.Keys.Up)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    _audioPlayer.Volume = 1.0;
                    Update();
                }
                else
                {
                    double newVolume = _audioPlayer._volume + 0.1;
                    if (newVolume > 1.0)
                    {
                        newVolume = 1.0;
                    }
                    _audioPlayer.Volume = newVolume;
                    Update();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.S || e.KeyCode is System.Windows.Forms.Keys.Down)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    _audioPlayer.Volume = 0.0;
                    Update();
                }
                else
                {
                    double newVolume = _audioPlayer._volume - 0.1;
                    if (newVolume < 0.0)
                    {
                        newVolume = 0.0;
                    }
                    _audioPlayer.Volume = newVolume;
                    Update();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.R)
            {
                if ((e.Shift || e.Control || e.Alt))
                {
                    _audioPlayer.Volume = 0.5;
                    Update();
                }
                else
                {
                    OnReplayButtonClicked();
                }
            }
            else if (e.KeyCode is System.Windows.Forms.Keys.M)
            {
                OnMuteButtonClicked();
            }
        }
        #endregion
    }
}