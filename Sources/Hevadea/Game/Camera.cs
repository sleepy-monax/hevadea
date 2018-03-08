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
            Zoom = 4f;
        }

        public override void Animate(GameTime gameTime)
        {
            X -= (X - Mathf.Floor(FocusEntity.X)) * 0.1f;
            Y -= (Y - Mathf.Floor(FocusEntity.Y)) * 0.1f;
        }
    }
}