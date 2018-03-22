using System.Collections.Generic;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Tiles.Renderers
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
