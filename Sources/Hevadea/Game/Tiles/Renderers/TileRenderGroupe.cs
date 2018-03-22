using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Tiles.Renderers
{
    public class TileRenderGroupe : TileRender
    {
        public List<TileRender> Renderers { get; set; } = new List<TileRender>();

        public override void Draw(SpriteBatch spriteBatch, TilePosition position, Level level)
        {
            foreach (var r in Renderers)
            {
                r.Tile = Tile;
                r.Draw(spriteBatch, position, level);
            }
        }
    }
}
