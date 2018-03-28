using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class RevealMap : EntityComponent, IEntityComponentUpdatable
    {
        public int Range { get; set; } = 4;
        
        public void Update(GameTime gameTime)
        {
            var p = Owner.GetTilePosition();
            var pp = new Point(Rise.Rnd.Next(-Range, Range) + p.X, Rise.Rnd.Next(-Range, Range) + p.Y);
            Owner.Level.Map.SetPixel(pp.X, pp.Y, Owner.Level.GetTile(pp.X, pp.Y).MiniMapColor);
        }
    }
}