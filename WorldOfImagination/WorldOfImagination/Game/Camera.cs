using Maker.Rise;
using Maker.Rise.GameComponent;
using Microsoft.Xna.Framework;
using System;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game
{
    public class Camera
    {

        float Zoom = 3.0f;

        RiseGame Game;
        public Entity FocusEntity = null;

        public Camera(RiseGame game)
        {
            Game = game;
        }

        public int GetWidth()
        {
            return (int)(Game.Graphics.GetWidth() / Zoom);
        }

        public int GetHeight()
        {
            return (int)(Game.Graphics.GetHeight() / Zoom);
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
                    (float)Math.Floor(-(cameraX * Zoom - Game.Graphics.GetWidth() / 2 )),
                    (float)Math.Floor(-(cameraY * Zoom - Game.Graphics.GetHeight() / 2)), 0f);
            }
        }

    }
}
