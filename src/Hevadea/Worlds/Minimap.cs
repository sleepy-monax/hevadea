using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Hevadea.Worlds
{
    public class MinimapWaypoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Icon { get; set; }
    }
    
    public class Minimap
    {
        Level _level;

        public Texture2D Texture { get; private set; }

        public List<MinimapWaypoint> Waypoints { get; set; } = new List<MinimapWaypoint>();
        

        public Minimap(Level level)
        {
            if (Rise.NoGraphic) 
            {

            }
            else
            {
                Texture = new Texture2D(Rise.MonoGame.GraphicsDevice, level.Width, level.Height);
                Texture.Clear(Microsoft.Xna.Framework.Color.TransparentBlack);            
            }


            _level = level;
        }

        public void Reveal(int tx, int ty)
        {
            Texture.SetPixel(tx, ty, _level.GetTile(tx, ty).MiniMapColor);
        }

        public void LoadFromFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            Texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
            fs.Close();
        }

        public void SaveToFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.OpenOrCreate);
            Texture.SaveAsPng(fs, _level.Width, _level.Height);
            fs.Close();
        }
    }
}