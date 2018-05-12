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
        private Level _level;

        public List<MinimapWaypoint> Waypoints { get; set; } = new List<MinimapWaypoint>();
        public Texture2D Texture { get; set; }

        public Minimap(Level level)
        {
            Texture = new Texture2D(Rise.MonoGame.GraphicsDevice, level.Width, level.Height);
            Texture.Clear(Color.TransparentBlack);

            _level = level;
        }
    }
}