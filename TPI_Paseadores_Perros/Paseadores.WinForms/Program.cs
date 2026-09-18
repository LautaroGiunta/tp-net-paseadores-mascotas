namespace Paseadores.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            bool ciclo = true;
            while (ciclo)
            {
                FormLogin login = new FormLogin();
                if (login.ShowDialog() == DialogResult.OK && login.UsuarioLogueado != null)
                {
                    FormMain main = new FormMain(login.UsuarioLogueado);
                    Application.Run(main);

                    // El token era de la sesión que se cerró
                    ApiClient.CerrarSesion();

                    if (main.QuiereCerrarSesion)
                    {
                        ciclo = true;
                    }
                    else
                    {
                        ciclo =false;
                    }
                }
                else
                {
                    Application.Exit();
                    ciclo = false;
                }
            }
        }
    }
}
