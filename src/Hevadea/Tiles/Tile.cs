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
        public string Name { get; }
        public bool BlockLineOfSight { get; set; } = false;

        private List<TileComponent> Tags { get; }
        public TileRender Render { get => _render; set { _render = value; _render.Tile = this; } }
        private TileRender _render = null;
        public Color MiniMapColor { get; }

        public Tile(string name, Color? minimapColor = null)
        {
            Name = name;
            Tags = new List<TileComponent>();
            MiniMapColor = minimapColor ?? Color.Gray;
        }

        public Tile(string name, TileRender renderer, Color? minimapColor = null) : this(name, minimapColor)
        {
            Render = renderer;
        }

        public void Update(Coordinates position, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            foreach (var t in Tags)
            {
                if (t is IUpdatableTileComponent u) u.Update(this, position, data, level, gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Coordinates position, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            spriteBatch.FillRectangle(position.ToRectangle(), new Color(148, 120, 92));
            _render?.Draw(spriteBatch, position, level);

            foreach (var t in Tags)
            {
                if (t is IDrawableTileComponent d) d.Draw(this, spriteBatch, position, data, level, gameTime);
            }
        }

        public bool HasTag<T>()
        {
            foreach (var t in Tags)
            {
                if (t is T) return true;
            }

            return false;
        }

        public T Tag<T>() where T : TileComponent
        {
            foreach (var t in Tags)
            {
                if (t is T variable) return variable;
            }

            return null;
        }

        public void AddTag(TileComponent tag)
        {
            tag.AttachedTile = this; Tags.Add(tag);
        }

        public void AddTag(params TileComponent[] tags)
        {
            foreach (var t in tags) AddTag(t);
        }
    }
}