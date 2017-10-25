using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using WorldOfImagination.Framework;

namespace WorldOfImagination
{
    class Program
    {
        static void Main(string[] args)
        {
            new Host(new WorldOfImagination()).Run();
        }
    }
}
