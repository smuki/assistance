namespace WinFormsApp1
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "OnlyRun");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new frmTools());
                mutex.ReleaseMutex();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}