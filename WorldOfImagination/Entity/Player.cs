using OpenTK;
using WorldOfImagination.Framework;
using WorldOfImagination.Framework.Modules;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination.Entity
{
    public class Player : Entity, IDrawable
    {
    
        public Player()
        {
            Velocity = Vector2.Zero;
            HitBox = new Rectangle(0, 0, 16, 16);
        }

        public void Draw(Draw Draw)
        {
            
        }
    }
}