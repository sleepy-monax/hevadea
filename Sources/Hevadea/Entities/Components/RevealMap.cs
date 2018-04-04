using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public class RevealMap : Component, IEntityComponentUpdatable
    {
        public int Range { get; set; } = 4;
        
        public void Update(GameTime gameTime)
        {
            var p = Owner.GetTilePosition();

            var pp = new Point(Rise.Rnd.Next(-Range, Range) + p.X, Rise.Rnd.Next(-Range, Range) + p.Y);
            Owner.Level.Minimap.Texture.SetPixel(pp.X, pp.Y, Owner.Level.GetTile(pp.X, pp.Y).MiniMapColor);
        }
    }
}