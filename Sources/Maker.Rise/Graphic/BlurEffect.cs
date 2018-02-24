using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Rise.Graphic
{
    public class BlurEffect
    {

        public Effect Effect { get; }
        public int BlurRadius { get; set; } = 16;
        public float BlurAmount { get; set; } = 2.0f;

        public Vector2[] TextureOffsetsX;
        public Vector2[] TextureOffsetsY;
        public float[] Kernel;
        public float Sigma;

        public BlurEffect()
        {
            Effect = EngineRessources.effectBlur;
        }

        public void Setup(float textureWidth, float textureHeight)
        {
            ComputeKernel();
            ComputeOffsets(textureWidth, textureHeight);
        }

        public void Use(bool Vertical)
        {
            Effect.CurrentTechnique = Effect.Techniques["SpriteDrawing"];
            Effect.Parameters["weights"].SetValue(Kernel);
            Effect.Parameters["offsets"].SetValue(Vertical ? TextureOffsetsX : TextureOffsetsY);
        }

        private void ComputeKernel()
        {
            Kernel = null;
            Kernel = new float[BlurRadius * 2 + 1];
            Sigma = BlurRadius / BlurAmount;

            float twoSigmaSquare = 2.0f * Sigma * Sigma;
            float sigmaRoot = (float)Math.Sqrt(twoSigmaSquare * Math.PI);
            float total = 0.0f;
            float distance = 0.0f;
            int index = 0;

            for (int i = -BlurRadius; i <= BlurRadius; ++i)
            {
                distance = i * i;
                index = i + BlurRadius;
                Kernel[index] = (float)Math.Exp(-distance / twoSigmaSquare) / sigmaRoot;
                total += Kernel[index];
            }

            for (int i = 0; i < Kernel.Length; ++i)
                Kernel[i] /= total;
        }

        private void ComputeOffsets(float textureWidth, float textureHeight)
        {
            TextureOffsetsX = null;
            TextureOffsetsX = new Vector2[BlurRadius * 2 + 1];

            TextureOffsetsY = null;
            TextureOffsetsY = new Vector2[BlurRadius * 2 + 1];

            int index = 0;
            float xOffset = 1.0f / textureWidth;
            float yOffset = 1.0f / textureHeight;

            for (int i = -BlurRadius; i <= BlurRadius; ++i)
            {
                index = i + BlurRadius;
                TextureOffsetsX[index] = new Vector2(i * xOffset, 0.0f);
                TextureOffsetsY[index] = new Vector2(0.0f, i * yOffset);
            }
        }
    }
}
