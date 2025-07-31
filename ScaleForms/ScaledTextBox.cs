namespace ScaleForms
{
    public class ScaledTextBox : System.Windows.Forms.TextBox
    {
        #region Public Variables
        public double XMin { get { return _xMin; } set { _xMin = value; RefreshBounds(); } }
        public double YMin { get { return _yMin; } set { _yMin = value; RefreshBounds(); } }
        public double XMax { get { return _xMax; } set { _xMax = value; RefreshBounds(); } }
        public double YMax { get { return _yMax; } set { _yMax = value; RefreshBounds(); } }
        public double FontSize = 1.0;
        #endregion
        #region Protected Variables
        protected double _xMin = 0.0;
        protected double _yMin = 0.0;
        protected double _xMax = 1.0;
        protected double _yMax = 1.0;
        protected System.Windows.Forms.Control _currentParent = null;
        #endregion
        #region Public Constructors
        public ScaledTextBox()
        {
            ParentChanged += OnParentChangedEvent;
        }
        #endregion
        #region Public Methods
        public virtual void RefreshBounds()
        {
            if (Parent is null)
            {
                return;
            }
            Size = new System.Drawing.Size((int)(Parent.ClientSize.Width * (_xMax - _xMin)), (int)(Parent.ClientSize.Height * (_yMax - _yMin)));
            Location = new System.Drawing.Point((int)(Parent.ClientSize.Width * _xMin), Parent.ClientSize.Height - (int)(Parent.ClientSize.Height * _yMin) - Size.Height);
            try
            {
                Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, (float)(Height * FontSize), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            }
            catch
            {

            }
        }
        #endregion
        #region Protected Methods
        protected virtual void OnParentChangedEvent(object sender, System.EventArgs e)
        {
            if (!(_currentParent is null))
            {
                _currentParent.Resize -= OnParentResizedEvent;
            }
            _currentParent = Parent;
            _currentParent.Resize += OnParentResizedEvent;
            RefreshBounds();
        }
        protected virtual void OnParentResizedEvent(object sender, System.EventArgs e)
        {
            RefreshBounds();
        }
        #endregion
    }
}