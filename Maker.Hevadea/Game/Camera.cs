using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Hevadea.Game
{
    public class Camera
    {
        public float Zoom = 3f;
        public Entity FocusEntity = null;
        public float X = 0f;
        public float Y = 0f;

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
            if (FocusEntity == null) { return Matrix.Identity; }

            X = (float)Math.Floor(FocusEntity.X + FocusEntity.Width  / 2f + (FocusEntity.Width  / 2f));
            Y = (float)Math.Floor(FocusEntity.Y + FocusEntity.Height / 2f + (FocusEntity.Height / 2f));

            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation((float)Math.Floor(-(X * Zoom - Engine.Graphic.GetWidth()  / 2f)),
                                                                       (float)Math.Floor(-(Y * Zoom - Engine.Graphic.GetHeight() / 2f)), 0f);
        }
    }
}