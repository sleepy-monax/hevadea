using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Entities
{
    public class ZombieEntity : Mob
    {

        public ZombieEntity()
        {
            Width = 8;
            Height = 8;
            Sprite = new Sprite(Ressources.tile_creatures, 1, new Point(16, 16));

            Health = MaxHealth = 20;
            IsInvincible = false;
        }

    }
}
