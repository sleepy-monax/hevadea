using System;
using System.Runtime.InteropServices.ComTypes;
using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class ZombieEntity : Entity
    {
        public ZombieEntity()
        {
            Width = 8;
            Height = 8;
            
            AddComponent(new MoveComponent());
            AddComponent(new HealthComponent(20));
            AddComponent(new AttackComponent(1));
            AddComponent(new NpcRenderComponent(new Sprite(Ressources.tile_creatures, 2, new Point(16, 32))));
        }

        Random rnd = new Random();
        private int counter = 16;
        private Direction direction = Direction.Down;
        
        public override void OnUpdate(GameTime gameTime)
        {
            counter--;

            if (counter == 0)
            {
                direction = (Direction)rnd.Next(0, 4);
                counter = rnd.Next(8, 48);
            }
            
            var v = direction.ToPoint();
            GetComponent<MoveComponent>().Move(v.X * 0.5f, v.Y * 0.5f, direction);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}