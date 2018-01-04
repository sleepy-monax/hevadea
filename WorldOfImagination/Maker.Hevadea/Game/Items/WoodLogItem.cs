using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Items
{
    public class WoodLogItem : StackableItem
    {

        public WoodLogItem()
        {
            Sprite = new Sprite(Ressources.tile_items, new Point(6,0));
        }

    }
}
