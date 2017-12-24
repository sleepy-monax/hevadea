using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfImagination.Game.Tiles
{
    public class WaterTile : Tile
    {
        public WaterTile(byte id) : base(id)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.Draw(Ressources.tile_tiles, 4, pos.ToOnScreenPosition().ToVector2(), Color.White);
        }

    }
}
