using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.Framework.Graphic
{
    public class Camera
    {
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Zoom { get; set; } = 1f;
        
        public Matrix GetTransform()
        {
            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                       Mathf.Floor(-(X * Zoom - Rise.Graphic.GetWidth() / 2f)),
                       Mathf.Floor(-(Y * Zoom - Rise.Graphic.GetHeight() / 2f)), 0f);
        }

        public Vector2 ToWorldSpace(Vector2 screen)
        {
             return new Vector2((screen.X / Zoom) + (X - Rise.Graphic.GetWidth() / Zoom),
                                (screen.Y / Zoom) + (Y - Rise.Graphic.GetHeight() / Zoom));
        }

        public Vector2 ToScreenSpace(Vector2 world)
        {
            return new Vector2();
        }

        public void Update(GameTime gameTime)
        {
            Animate(gameTime);
        }
        
        public virtual void Animate(GameTime gameTime){}
    }
}