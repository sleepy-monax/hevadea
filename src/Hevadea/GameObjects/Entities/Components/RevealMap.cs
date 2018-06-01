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
            Owner.Level.Minimap.Reveal(Rise.Rnd.Next(-Range, Range) + p.X, Rise.Rnd.Next(-Range, Range) + p.Y);
        }
    }
}