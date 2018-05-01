using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Framework.Graphic
{
    public class Camera
    {
        double _thrauma;

        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Zoom { get; set; } = 1f;

        public double Thrauma { get => _thrauma; set => _thrauma = Mathf.Clamp01(value); }
        public double ScreenShake => Thrauma * Thrauma;
        public int MaxShakeOffset { get; set; } = 16;
        public double MaxShakeAngle { get; set; } = Math.PI / 12;

        public int GetWidth()
        {
            return (int)(Rise.Graphic.GetWidth() / Zoom);
        }

        public int GetHeight()
        {
            return (int)(Rise.Graphic.GetHeight() / Zoom);
        }

        float value = 0f;

        public Matrix GetTransform()
        {
            float shakeAngle =   (float)(MaxShakeAngle  * ScreenShake * (Rise.Rnd.NextDouble() * 2 - 1f));
            float shakeOffsetX = (float)(MaxShakeOffset * ScreenShake * (Rise.Rnd.NextDouble() * 2 - 1f));
            float shakeOffsetY = (float)(MaxShakeOffset * ScreenShake * (Rise.Rnd.NextDouble() * 2 - 1f));

            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                       Mathf.Floor(-((X + shakeOffsetX) * Zoom)),
                       Mathf.Floor(-((Y + shakeOffsetY) * Zoom)), 0f) * Matrix.CreateRotationZ(shakeAngle) * Matrix.CreateTranslation(Rise.Graphic.GetWidth() / 2f, Rise.Graphic.GetHeight() / 2f, 0) ;
        }

        public Vector2 ToWorldSpace(Vector2 screen)
        {
            return new Vector2((screen.X / Zoom) + (X - Rise.Graphic.GetWidth() / Zoom) + (Rise.Graphic.GetWidth() / Zoom) / 2,
                               (screen.Y / Zoom) + (Y - Rise.Graphic.GetHeight() / Zoom) + (Rise.Graphic.GetHeight() / Zoom) / 2);
        }

        public Vector2 ToScreenSpace(Vector2 world)
        {
            return new Vector2();
        }

        public void Update(GameTime gameTime)
        {
            if (Rise.Input.KeyPress(Microsoft.Xna.Framework.Input.Keys.T))
            {
                Thrauma = 1f;
            }
            Thrauma -= gameTime.ElapsedGameTime.TotalSeconds;
            Animate(gameTime);
            value += 0.01f;
        }

        public virtual void Animate(GameTime gameTime) { }
    }
}