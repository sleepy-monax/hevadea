using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using System;
using Hevadea.Framework;

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

        public float Get_zoom()
        {
            return Zoom;
        }

        public void Set_zoom(float new_zoom)
        {
            Zoom = new_zoom;
        }

        public override void Animate(GameTime gameTime)
        {
            X -= (X - Mathf.Floor(FocusEntity.X + FocusEntity.Width / 2f)) * 0.1f;
            Y -= (Y - Mathf.Floor(FocusEntity.Y + FocusEntity.Height / 2f)) * 0.1f;
        }
    }
}