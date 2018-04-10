using System.Collections.Generic;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Tiles
{
    public interface IDrawableTileComponent
    {
        void Draw(Tile tile, SpriteBatch spriteBatch, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public interface IUpdatableTileComponent
    {
        void Update(Tile tile, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public class TileComponent
    {
        public Tile AttachedTile { get; set; }
    }
}
