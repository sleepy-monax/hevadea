using System;

namespace TexPacker
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.ReadLine();
            string path = "/home/nicolas/Bureau/assets/";
            var spriteAtlas = new SpriteAtlas(1024, 1024);
            spriteAtlas.InsertSprites(path);
            spriteAtlas.SaveImage().Save("out.png");
            Console.ReadLine();
        }
    }
}
