using Hevadea.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Worlds
{
    public class LevelSpriteBatchPool
    {
        public SpriteBatch Tiles { get; }
        public SpriteBatch Entities { get; }
        public SpriteBatch Overlay { get; }
        public SpriteBatch Lights { get; }
        public SpriteBatch Shadows { get; }
        public SpriteBatch Generic { get; }

        public LevelSpriteBatchPool()
        {
            Tiles = Rise.Graphic.CreateSpriteBatch();
            Entities = Rise.Graphic.CreateSpriteBatch();
            Overlay = Rise.Graphic.CreateSpriteBatch();
            Lights = Rise.Graphic.CreateSpriteBatch();
            Shadows = Rise.Graphic.CreateSpriteBatch();
            Generic = Rise.Graphic.CreateSpriteBatch();
        }

        public void Begin(Camera camera)
        {
            var transform = camera.GetTransform();

            Tiles.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
            Entities.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
            Overlay.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
            Lights.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp,
                transformMatrix: transform);
            Shadows.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: transform);
        }
    }
}