using System;


namespace RunToTheStairs.WPF.Input
{
    class GameButton
    {
        private readonly KeyState state_ = new();

        public bool Active { get; set; }

        public EventHandler OnPress;

        public EventHandler OnRelease;

        public void Update(bool isDown)
        {
            state_.Update(isDown);

            if (state_.Pressed)
            {
                Active = !Active;
                OnPress?.Invoke(this, new EventArgs());
            }
            else if (state_.Released)
            {
                OnRelease?.Invoke(this, new EventArgs());
            }
        }
    }
}
