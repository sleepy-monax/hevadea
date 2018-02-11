using System;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Creatures
{
    public class ZombieEntity : Entity
    {
        private int _counter = 16;
        private Direction _direction = Direction.Down;
        private Random _rnd = new Random();

        public ZombieEntity()
        {
            Width = 8;
            Height = 8;

            Origin = new Point(4, 4);

            Components.Add(new Move());
            Components.Add(new Health(10));
            Components.Add(new Attack());
            Components.Add(new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _counter--;

            if (_counter == 0)
            {
                _direction = (Direction) _rnd.Next(0, 4);
                _counter = _rnd.Next(8, 48);
            }

            var v = _direction.ToPoint();
            Components.Get<Move>().Do(v.X * 0.25f, v.Y * 0.25f, _direction);
        }

        public override bool IsBlocking(Entity entity)
        {
            return true;
        }
    }
}