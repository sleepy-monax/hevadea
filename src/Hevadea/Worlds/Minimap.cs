using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
		Texture2D _texture;


        public List<MinimapWaypoint> Waypoints { get; set; } = new List<MinimapWaypoint>();
        

        public Minimap(Level level)
        {
			if (Rise.NoGraphic) 
			{
				_bitmap 
			}
			else
			{
				_texture = new Texture2D(Rise.MonoGame.GraphicsDevice, level.Width, level.Height);
				_texture.Clear(Color.TransparentBlack);            
            }


            _level = level;
        }
    }
}