using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Tiles
{
    public class Tile
    {
        public int Id { get; private set; }
        private List<TileTag> _tags;
        private TileRenderer _renderer;

        public Tile(TileRenderer renderer)
        {
            Id = TILES.ById.Count;
            TILES.ById.Add(this);
            _renderer = renderer;
            _tags = new List<TileTag>();
        }

        public void Update(TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            foreach (var t in _tags)
            {
                if (t is IUpdatableTag u) u.Update(this, position, data, level, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            _renderer?.Draw(spriteBatch, position.ToOnScreenPosition().ToVector2(), new TileConection(this, position, level));
            foreach (var t in _tags)
            {
                if (t is IDrawableTag d) d.Draw(this, spriteBatch, position, data, level, gameTime);
            }
        }

        #region Tags

        public bool HasTag<T>()
        {
            foreach (var t in _tags)
            {
                if (t is T) return true;
            }

            return false;
        }

        public T Tag<T>() where T : TileTag
        {
            foreach (var t in _tags)
            {
                if (t is T) return (T)t;
            }

            return null;
        }

        public void AddTag(TileTag tag) {tag.AttachedTile = this; _tags.Add(tag); }
        public void AddTag(params TileTag[] tags) { foreach (var t in tags) AddTag(t); }

        #endregion
    }
}