using System;

namespace WorldOfImagination
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
            using (var game = new WorldOfImaginationGame())
            {
                game.Run();
            }
        }
    }
}
