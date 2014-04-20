using System;

namespace KMMobile
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (App app = new App())
            {
                app.Run();
            }
        }
    }
}
