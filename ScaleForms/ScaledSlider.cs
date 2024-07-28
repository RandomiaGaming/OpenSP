namespace ScaleForms
{
    public delegate void SliderClickedEvent(double value);
    public enum SliderDirection : byte { LeftToRight = 0, RightToLeft = 1, TopToBottom = 2, BottomToTop = 3 };
    public class ScaledSlider : ScaledPictureBox
    {
        #region Public Variables
        public System.Drawing.Color SliderForegroundColor
        {
            get
            {
                return _sliderForegroundColor;
            }
            set
            {
                _sliderForegroundColor = value;
                _sliderForegroundBrush = new System.Drawing.SolidBrush(_sliderForegroundColor);
            }
        }
        public System.Drawing.Color SliderBackgroundColor
        {
            get
            {
                return _sliderBackgroundColor;
            }
            set
            {
                _sliderBackgroundColor = value;
                _sliderBackgroundBrush = new System.Drawing.SolidBrush(_sliderBackgroundColor);
            }
        }
        public SliderDirection SliderDirection = SliderDirection.LeftToRight;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < 0)
                {
                    throw new System.Exception("Value must be greater than or equal to 0.");
                }
                else if (value > 1)
                {
                    throw new System.Exception("Value must be less than or equal to 1.");
                }
                _value = value;
            }
        }
        public SliderClickedEvent SliderClickedEvent = null;
        #endregion
        #region Internal Variables
        internal double _value;
        internal System.Drawing.Color _sliderForegroundColor = default(System.Drawing.Color);
        internal System.Drawing.Color _sliderBackgroundColor = default(System.Drawing.Color);
        internal System.Drawing.Brush _sliderForegroundBrush = new System.Drawing.SolidBrush(default(System.Drawing.Color));
        internal System.Drawing.Brush _sliderBackgroundBrush = new System.Drawing.SolidBrush(default(System.Drawing.Color));
        #endregion
        #region Public Constructors
        public ScaledSlider()
        {
            MouseDown += OnMouseDownEvent;
        }
        #endregion
        #region Public Methods
        public new virtual void Click(int mousePositionX, int mousePositionY)
        {
            if (!(SliderClickedEvent is null))
            {
                double value;
                if (SliderDirection is SliderDirection.LeftToRight)
                {
                    value = mousePositionX / (double)Width;
                }
                else if (SliderDirection is SliderDirection.RightToLeft)
                {
                    value = 1.0 - (mousePositionX / (double)Width);
                }
                else if (SliderDirection is SliderDirection.TopToBottom)
                {
                    value = mousePositionY / (double)Height;
                }
                else
                {
                    value = 1.0 - (mousePositionY / (double)Height);
                }
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                SliderClickedEvent.Invoke(value);
            }
        }
        #endregion
        #region Protected Methods
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            pevent.Graphics.FillRectangle(_sliderBackgroundBrush, new System.Drawing.Rectangle(0, 0, Width, Height));
            if (SliderDirection is SliderDirection.LeftToRight)
            {
                pevent.Graphics.FillRectangle(_sliderForegroundBrush, new System.Drawing.Rectangle(0, 0, (int)(Width * _value), Height));
            }
            else if (SliderDirection is SliderDirection.RightToLeft)
            {
                pevent.Graphics.FillRectangle(_sliderForegroundBrush, new System.Drawing.Rectangle(Width - (int)(Width * _value), 0, (int)(Width * _value), Height));
            }
            else if (SliderDirection is SliderDirection.TopToBottom)
            {
                pevent.Graphics.FillRectangle(_sliderForegroundBrush, new System.Drawing.Rectangle(0, 0, Width, (int)(Height * _value)));
            }
            else
            {
                pevent.Graphics.FillRectangle(_sliderForegroundBrush, new System.Drawing.Rectangle(0, Height - (int)(Height * _value), Width, (int)(Height * _value)));
            }
            base.OnPaint(pevent);
        }
        protected void OnMouseDownEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Click(e.Location.X, e.Location.Y);
        }
        #endregion

    }
}