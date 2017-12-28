using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Rise.Ressource;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.Game.Items
{
    public class Item
    {
        public int MaxStack = 99;
        public Sprite Sprite;
        public virtual bool CanBeStackWith(Item other)
        {
            return GetType() == other.GetType();
        }

        public void Attack(Mob user, Entity taget)
        {
            
        }
        
        public void Attack(Mob user, TilePosition taget)
        {
            
        }
        
        public void InteracteOn(Mob user, Entity entity)
        {
            
        }
        
        public void InteracteOn(Mob user, TilePosition tile)
        {
            
        }
    }
}
