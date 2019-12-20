using System;

namespace MRoyale.Windows
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var gameBase = new GameBase())
                gameBase.Run();
        }
    }
}
