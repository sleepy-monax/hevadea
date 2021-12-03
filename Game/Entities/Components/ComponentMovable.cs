using System;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public class ComponentMovable : EntityComponent
    {
        private float _sx = 0;
        private float _sy = 0;

        public Vector2 Speed => new Vector2(_sx, _sy);
        public bool IsMoving => Math.Abs(_sx) > 0.001f || Math.Abs(_sy) > 0.001f;

        public void Move(float sx, float sy)
        {
            _sx += sx;
            _sy += sy;
        }

        public void MoveTo(Coordinates coords, float speed)
        {
            var destination = coords.GetCenter();
            MoveTo(destination.X, destination.Y, speed);
        }

        public void MoveTo(float x, float y, float speed)
        {
            var dir = new Vector2(x - Owner.X, y - Owner.Y);

            if (dir.Length() > 1f)
            {
                dir.Normalize();
                dir = dir * speed;
            }

            _sx += dir.X;
            _sy += dir.Y;
        }
    }
}