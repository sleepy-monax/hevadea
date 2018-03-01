using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public static class SpriteBatchExtension
    {
        public delegate void DrawTask(SpriteBatch spriteBatch, GameTime gameTime);

        public static void Begin(this SpriteBatch spriteBatch, SpriteBatchBeginState spriteBatchBeginState)
        {
            spriteBatch.Begin(spriteBatchBeginState.SortMode, spriteBatchBeginState.BlendState, spriteBatchBeginState.SamplerState, spriteBatchBeginState.DepthStencilState, spriteBatchBeginState.RasterizerState, spriteBatchBeginState.Effect, spriteBatchBeginState.TransformMatrix);
        }
        
        public static void BeginDrawEnd(this SpriteBatch sb, DrawTask task, GameTime gameTime = null, SpriteBatchBeginState state = null)
        {
            if (state != null)
            {
                sb.Begin(state);
            }
            else
            {
                sb.Begin();
            }
            
            task(sb, gameTime);
            sb.End();
        }
    }
}