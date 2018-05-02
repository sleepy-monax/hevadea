using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components
{
    public class RevealMap : EntityComponent, IEntityComponentUpdatable
    {
        public int Range { get; set; }
        
		public RevealMap(int range = 4)
		{
			Range = range;
		}

        public void Update(GameTime gameTime)
        {
            var p = Owner.GetTilePosition();

            var pp = new Point(Rise.Rnd.Next(-Range, Range) + p.X, Rise.Rnd.Next(-Range, Range) + p.Y);
            Owner.Level.Minimap.Texture.SetPixel(pp.X, pp.Y, Owner.Level.GetTile(pp.X, pp.Y).MiniMapColor);
        }
    }
}