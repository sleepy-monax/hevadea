using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.Rise.Utils;

namespace WorldOfImagination.Game.Tiles
{
    public class GrassTile : Tile
    {
        public GrassTile(byte id) : base(id)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.Draw(Ressources.tile_tiles,1, pos.ToPoint().ToVector2(), Color.White);
        }
    }
}
