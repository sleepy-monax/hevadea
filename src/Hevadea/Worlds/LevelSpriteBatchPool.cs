using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Worlds
{
    public class LevelSpriteBatchPool
    {
        public SpriteBatch TileSpriteBatch { get; }
        public SpriteBatch EntitiesSpriteBatch { get; }

        public SpriteBatch OverlaySpriteBatch { get; }
        public SpriteBatch LightsSpriteBatch { get; }
        public SpriteBatch ShadowsSpriteBatch { get; }

        public SpriteBatch GenericSpriteBatch { get; }

        public LevelSpriteBatchPool()
        {
            TileSpriteBatch = Rise.Graphic.CreateSpriteBatch();

            EntitiesSpriteBatch = Rise.Graphic.CreateSpriteBatch();

            OverlaySpriteBatch = Rise.Graphic.CreateSpriteBatch();
            LightsSpriteBatch = Rise.Graphic.CreateSpriteBatch();
            ShadowsSpriteBatch = Rise.Graphic.CreateSpriteBatch();

            GenericSpriteBatch = Rise.Graphic.CreateSpriteBatch();
        }

        public void Begin(Camera camera)
        {
            Matrix transform = camera.GetTransform();

            TileSpriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
            EntitiesSpriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
            OverlaySpriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);

            LightsSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, transformMatrix: transform);
            ShadowsSpriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
        }
    }
}