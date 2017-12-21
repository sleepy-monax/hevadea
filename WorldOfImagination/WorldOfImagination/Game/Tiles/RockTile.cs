using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.Tiles
{
    public class RockTile : Tile
    {
        public RockTile(byte id) : base(id)
        {
        }

        public override bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.Draw(Ressources.tile_tiles, 4, pos.ToPoint().ToVector2(), Color.White);
        }

    }
}
