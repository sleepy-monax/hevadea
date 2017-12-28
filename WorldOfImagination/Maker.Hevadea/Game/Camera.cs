using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Maker.Rise.Components;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Hevadea.Game
{
    public class Camera
    {

        public float Zoom = 0.5f;
        public bool debugMode = false;
        public Entity FocusEntity = null;
        public float X = 0f;
        public float Y = 0f;

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
            X = (float)Math.Floor(FocusEntity.Position.X + FocusEntity.Width / 2 + (FocusEntity.Width / 2f));
            Y = (float)Math.Floor(FocusEntity.Position.Y + FocusEntity.Height / 2 + (FocusEntity.Height / 2f));

            if (debugMode) return Matrix.Identity;

            if (FocusEntity == null)
            {
                return Matrix.Identity;
            }
            else
            {
                return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                    (float)Math.Floor(-(X * Zoom - Engine.Graphic.GetWidth() / 2 )),
                    (float)Math.Floor(-(Y * Zoom - Engine.Graphic.GetHeight() / 2)), 0f);
            }
        }

    }
}
