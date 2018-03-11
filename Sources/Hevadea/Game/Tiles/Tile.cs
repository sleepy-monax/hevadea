using Hevadea.Framework.Graphic;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles.Renderers;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Game.Tiles
{
    public class Tile
    {
        public int Id { get; }
        public List<TileTag> Tags => _tags;
        public TileRender Render { get => _render; set { _render = value; _render.Tile = this; } }
        private readonly List<TileTag> _tags;
        private TileRender _render = null;

        public Tile()
        {
            Id = TILES.ById.Count;
            TILES.ById.Add(this);
            _tags = new List<TileTag>();
        }

        public Tile(TileRender renderer)
        {
            Id = TILES.ById.Count;
            TILES.ById.Add(this);
            Render = renderer;
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
            spriteBatch.FillRectangle(position.ToRectangle(), new Color(148, 120, 92));
            _render?.Draw(spriteBatch, position, level);
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
                if (t is T variable) return variable;
            }

            return null;
        }

        public void AddTag(TileTag tag) { tag.AttachedTile = this; _tags.Add(tag); }
        public void AddTag(params TileTag[] tags) { foreach (var t in tags) AddTag(t); }
        #endregion
    }
}