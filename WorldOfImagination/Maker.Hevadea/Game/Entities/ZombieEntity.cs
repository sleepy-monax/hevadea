using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class ZombieEntity : Mob
    {
        public ZombieEntity()
        {
            Width = 8;
            Height = 8;
            
            Health = MaxHealth = 20;
            IsInvincible = false;
        }
    }
}