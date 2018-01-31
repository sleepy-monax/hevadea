using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Items
{
    public class PlacableItem<T> : Item where T : Entity, new() 
    {
        public PlacableItem(string name, Sprite sprite) : base(name, sprite)
        {
        }

        public override void InteracteOn(Entity user, TilePosition pos)
        {
            if (user.Level.GetEntityOnTile(pos).Count == 0)
            {
                var inventory = user.Components.Get<Inventory>();

                if (inventory != null) inventory.Content.Remove(this, 1);

                user.Level.SpawnEntity(new T(), pos.X, pos.Y);

                if (user is PlayerEntity p)
                {
                    if (p.Components.Get<Inventory>().Content.Count(p.HoldingItem) == 0)
                    {
                        p.HoldingItem = null;
                    }
                }

            }
            else
            {
                base.InteracteOn(user, pos);
            }
        }
    }
}
