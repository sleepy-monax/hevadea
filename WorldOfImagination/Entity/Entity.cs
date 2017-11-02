using OpenTK;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination.Entity
{
    public class Entity
    {
        public Rectangle HitBox { get; set; }
        public Vector2 Velocity {get; set;}
    }
}