using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using System;
using Maker.Utils;

namespace Maker.Hevadea.Game
{
    public class Camera
    {
        public Entity FocusEntity;
        public float X;
        public float Y;
        public float Zoom = 4f;
        private float _zoom = 1f;

        public Camera(Entity focusEntity)
        {
            FocusEntity = focusEntity;
        }

        public int GetWidth()
        {
            return (int) (Engine.Graphic.GetWidth() / _zoom);
        }

        public int GetHeight()
        {
            return (int) (Engine.Graphic.GetHeight() / _zoom);
        }

        public Matrix GetTransform()
        {
            Update();
            return Matrix.CreateScale(_zoom) * Matrix.CreateTranslation(-(X * _zoom - Engine.Graphic.GetWidth() / 2f), -(Y * _zoom - Engine.Graphic.GetHeight() / 2f), 0f);
        }

        public void Update()
        {
            X -= (X - (FocusEntity.X + FocusEntity.Width / 2f)) * 0.05f;
            Y -= (Y - (FocusEntity.Y + FocusEntity.Height / 2f)) * 0.05f;
            _zoom -= (_zoom - Zoom) * 0.05f;
        }
    }
}