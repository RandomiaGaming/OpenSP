namespace ScaleForms
{
    public delegate void ButtonClickedEvent();
    public class ScaledButton : ScaledPictureBox
    {
        #region Public Variables
        public ButtonClickedEvent ButtonClickedEvent = null;
        #endregion
        #region Public Constructors
        public ScaledButton()
        {
            MouseDown += OnMouseDownEvent;
        }
        #endregion
        #region Public Methods
        public new virtual void Click()
        {
            if (!(ButtonClickedEvent is null))
            {
                ButtonClickedEvent.Invoke();
            }
        }
        #endregion
        #region Protected Methods
        protected void OnMouseDownEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Click();
        }
        #endregion
    }
}