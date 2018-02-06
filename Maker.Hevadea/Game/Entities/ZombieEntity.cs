using Maker.Hevadea.Enums;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using System;
using Maker.Hevadea.Game.Entities.Component;

namespace Maker.Hevadea.Game.Entities
{
    public class ZombieEntity : Entity
    {
        public ZombieEntity()
        {
            Width = 8;
            Height = 8;
            
            Origin = new Point(4,4);

            Components.Add(new Move());
            Components.Add(new Health(10));
            Components.Add(new Attack());
            Components.Add(new NpcRender(new Sprite(Ressources.tile_creatures, 2, new Point(16, 32))));
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
            Components.Get<Move>().Do(v.X * 0.25f, v.Y * 0.25f, direction);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}