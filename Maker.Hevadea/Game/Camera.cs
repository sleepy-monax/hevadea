using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Hevadea.Game
{
    public class Camera
    {
        public float Zoom = 1f;
        public Entity FocusEntity = null;
        public float X = 0f;
        public float Y = 0f;

        public Camera(Entity focusEntity)
        {
            FocusEntity = focusEntity;
        }

        public int GetWidth()
        {
            return (int) (Engine.Graphic.GetWidth() / Zoom);
        }

        public int GetHeight()
        {
            return (int) (Engine.Graphic.GetHeight() / Zoom);
        }

        public Matrix GetTransform()
        {
            Update();
            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation((float)Math.Floor(-(X * Zoom - Engine.Graphic.GetWidth()  / 2f)),
                                                                       (float)Math.Floor(-(Y * Zoom - Engine.Graphic.GetHeight() / 2f)), 0f);
        }

        public void Update()
        {
            X = (float)Math.Floor(FocusEntity.X + FocusEntity.Width / 2f);
            Y = (float)Math.Floor(FocusEntity.Y + FocusEntity.Height / 2f);
        }
    }
}