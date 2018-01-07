using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Items
{
    public class WoodLogItem : StackableItem
    {
        public WoodLogItem()
        {
            Sprite = new Sprite(Ressources.tile_items, new Point(6, 0));
        }
    }
}