using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;

namespace Hevadea.Game
{
    public class Camera
    {
        public Entity FocusEntity;
        public float X;
        public float Y;
        public float Zoom = 4f;
        public float Trauma = 0f;
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

        public Matrix GetTransform(GameTime gameTime)
        {
            Update(gameTime);
            return Matrix.CreateScale(_zoom) * Matrix.CreateTranslation(-(X * _zoom - Engine.Graphic.GetWidth() / 2f), -(Y * _zoom - Engine.Graphic.GetHeight() / 2f), 0f);
        }

        public void Update(GameTime gameTime)
        {
            X -= (X - (FocusEntity.X + FocusEntity.Width / 2f)) * 0.05f;
            Y -= (Y - (FocusEntity.Y + FocusEntity.Height / 2f)) * 0.05f;
            _zoom -= (_zoom - Zoom) * 0.05f;
            
            Trauma = Mathf.Clamp((float)gameTime.ElapsedGameTime.TotalSeconds, 0f, 1f);
        }
    }
}