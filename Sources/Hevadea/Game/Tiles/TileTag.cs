using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Tiles
{
    public interface IDrawableTag
    {
        void Draw(Tile tile, SpriteBatch spriteBatch, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public interface IUpdatableTag
    {
        void Update(Tile tile, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public class TileTag
    {
        public Tile AttachedTile { get; set; }
    }
}
