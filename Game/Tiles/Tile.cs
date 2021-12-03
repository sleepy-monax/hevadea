using Hevadea.Framework.Graphic;
using Hevadea.Tiles.Renderers;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Tiles
{
    public class Tile
    {
        private TileRender _render;
        private List<TileComponent> _tags;

        public string Name { get; }
        public bool BlockLineOfSight { get; set; } = false;
        public Color MinimapColor { get; }

        public TileRender Render
        {
            get => _render;
            set
            {
                _render = value;
                _render.Tile = this;
            }
        }

        public Tile(string name, Color? minimapColor = null)
        {
            Name = name;
            _tags = new List<TileComponent>();
            MinimapColor = minimapColor ?? Color.Gray;
        }

        public Tile(string name, TileRender renderer, Color? minimapColor = null) : this(name, minimapColor)
        {
            Render = renderer;
        }

        public void Update(Coordinates coords, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            foreach (var t in _tags)
                if (t is IUpdatableTileComponent u)
                    u.Update(this, coords, data, level, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Coordinates coords, Dictionary<string, object> data, Level level,
            GameTime gameTime)
        {
            //spriteBatch.FillRectangle(coords.ToRectangle(), new Color(148, 120, 92));
            _render?.Draw(spriteBatch, coords, level);

            foreach (var t in _tags)
                if (t is IDrawableTileComponent d)
                    d.Draw(this, spriteBatch, coords, data, level, gameTime);
        }

        public bool HasTag<T>()
        {
            foreach (var t in _tags)
                if (t is T)
                    return true;

            return false;
        }

        public T Tag<T>() where T : TileComponent
        {
            foreach (var t in _tags)
                if (t is T variable)
                    return variable;

            return null;
        }

        public void AddTag(TileComponent tag)
        {
            tag.AttachedTile = this;
            _tags.Add(tag);
        }

        public void AddTag(params TileComponent[] tags)
        {
            foreach (var t in tags) AddTag(t);
        }
    }
}