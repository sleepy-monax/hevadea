using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Microsoft.Xna.Framework;

namespace Hevadea.Game
{
    public class Camera : Framework.Graphic.Camera
    {
        public Entity FocusEntity;
        
        public Camera(Entity focusEntity)
        {
            FocusEntity = focusEntity;
            Zoom = 6f;
        }

        public void JumpToFocusEntity()
        {
            X = FocusEntity.X;
            Y = FocusEntity.Y;
        }

        public override void Animate(GameTime gameTime)
        {
            X = FocusEntity.X;
            Y = FocusEntity.Y;
        }
    }
}