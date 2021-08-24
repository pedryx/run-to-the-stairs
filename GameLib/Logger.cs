using System;
using System.IO;
using System.Runtime.InteropServices;


namespace GameLib
{
    /// <summary>
    /// Represent a logger of messages with timestamp.
    /// </summary>
    public static class Logger
    {
        private const int consoleHide = 0;
        private const int consoleShow = 5;

        [DllImport(@"kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport(@"kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport(@"user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Output of the logger.
        /// </summary>
        public static TextWriter Out { get; set; } = Console.Out;

        /// <summary>
        /// Write message.Time stamp will be attached to messag. Format of
        /// time stamp will be "HH:mm:ss".
        /// </summary>
        /// <param name="message"></param>
        public static void Write(string message)
        {
            Out.Write($"[{DateTime.Now:HH:mm:ss}] ");
            Out.WriteLine(message);
        }

        /// <summary>
        /// Show console window.
        /// </summary>
        public static void ShowConsole()
        {
            var handle = GetConsoleWindow();
            if (handle == IntPtr.Zero)
                AllocConsole();
            else
                ShowWindow(handle, consoleShow);
        }

        /// <summary>
        /// Hide console window.
        /// </summary>
        public static void HideConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, consoleHide);
        }
    }
}
