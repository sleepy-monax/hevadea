using Microsoft.Xna.Framework;
using System;
using WorldOfImagination.GameComponent;

namespace WorldOfImagination.Game
{
    public class Camera
    {

        float Zoom = 2.0f;

        WorldOfImaginationGame Game;
        public Entity FocusEntity = null;

        public Camera(WorldOfImaginationGame game)
        {
            Game = game;
        }

        public Matrix GetTransform()
        {
            if (FocusEntity == null)
            {
                return Matrix.Identity;
            }
            else
            {
                var cameraX = (float)Math.Floor(FocusEntity.X + (FocusEntity.Width / 2));
                var cameraY = (float)Math.Floor(FocusEntity.Y + (FocusEntity.Height / 2));
                return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                    (float)Math.Floor(-(cameraX * Zoom - Game.Graphics.GetWidth() / 2 )),
                    (float)Math.Floor(-(cameraY * Zoom - Game.Graphics.GetHeight() / 2)), 0f);
            }
        }

    }
}
