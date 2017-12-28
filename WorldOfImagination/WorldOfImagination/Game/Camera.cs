using Maker.Rise;
using Maker.Rise.Components;
using Microsoft.Xna.Framework;
using System;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game
{
    public class Camera
    {

        float Zoom = 4f;

        public Entity FocusEntity = null;


        public int GetWidth()
        {
            return (int)(Engine.Graphic.GetWidth() / Zoom);
        }

        public int GetHeight()
        {
            return (int)(Engine.Graphic.GetHeight() / Zoom);
        }

        public Matrix GetTransform()
        {
            if (FocusEntity == null)
            {
                return Matrix.Identity;
            }
            else
            {
                var cameraX = (float)Math.Floor(FocusEntity.Position.X + (FocusEntity.Width / 2f));
                var cameraY = (float)Math.Floor(FocusEntity.Position.Y + (FocusEntity.Height / 2f));
                return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                    (float)Math.Floor(-(cameraX * Zoom - Engine.Graphic.GetWidth() / 2 )),
                    (float)Math.Floor(-(cameraY * Zoom - Engine.Graphic.GetHeight() / 2)), 0f);
            }
        }

    }
}
