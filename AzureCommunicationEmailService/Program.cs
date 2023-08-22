namespace AzureCommunicationEmailService
{
    internal static class Program
    {
        static frmMain frmMain;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            frmMain = new frmMain();
            Application.Run(frmMain);
            frmMain.Dispose();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"{ex.Message} {Environment.NewLine} {ex.StackTrace}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            frmMain.WriteException(ex);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            MessageBox.Show($"{ex.Message} {Environment.NewLine} {ex.StackTrace}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            frmMain.WriteException(ex);
        }
    }
}