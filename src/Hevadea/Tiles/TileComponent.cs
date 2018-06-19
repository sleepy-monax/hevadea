using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Tiles
{
    public interface IDrawableTileComponent
    {
        void Draw(Tile tile, SpriteBatch spriteBatch, Coordinates position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public interface IUpdatableTileComponent
    {
        void Update(Tile tile, Coordinates position, Dictionary<string, object> data, Level level, GameTime gameTime);
    }

    public class TileComponent
    {
        public Tile AttachedTile { get; set; }
    }
}