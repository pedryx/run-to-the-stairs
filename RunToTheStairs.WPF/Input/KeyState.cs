namespace RunToTheStairs.WPF.Input
{
    class KeyState
    {
        private bool last_;
        private bool current_;

        public bool Pressed => !last_ && current_;

        public bool Released => last_ && !current_;

        public bool IsUp => !current_;

        public bool IsDown => current_;

        public void Update(bool isDown)
        {
            last_ = current_;
            current_ = isDown;
        }

    }
}
