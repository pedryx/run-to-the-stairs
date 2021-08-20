using System;


namespace GameLib.Managers.IO
{
    public class SaveNotSupportedException : Exception
    {
        public SaveNotSupportedException()
            : base("This IO Manager dont support save.")
        {
        }
    }
}
