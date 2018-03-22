using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Tiles.Renderers
{
    public abstract class TileRender
    {
        public Tile Tile { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch, TilePosition position, Level level);
    }
}
