using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class RendererCreature : Renderer
    {
        private float _animationTime;

        public static readonly int[] FRAMES = {0, 2, 1, 2};
        public Texture2D BaseTexture { get; }

        public RendererCreature(Texture2D baseTexture)
        {
            BaseTexture = baseTexture;
        }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            _animationTime += 8f * gameTime.GetDeltaTime();

            var frame = FRAMES[(int) (_animationTime % 4)];

            var framePosition = entity.IsMoving()
                ? new Point(frame * 16, (int) Owner.Facing * 32)
                : new Point(2 * 16, (int) Owner.Facing * 32);
            var frameSize = entity.IsSwimming() ? new Point(16, 20) : new Point(16, 32);

            var sourceRectangle = new Rectangle(framePosition, frameSize);
            var sourceRectangleShadow = new Rectangle(framePosition, new Point(16, 32));

            if (entity.IsSwimming())
                spriteBatch.Draw(
                    BaseTexture,
                    position + new Vector2(-8, -24 + (entity.IsSwimming() ? 4 : -2f)),
                    sourceRectangleShadow,
                    Color.Black * 0.5f);

            spriteBatch.Draw(
                BaseTexture,
                position + new Vector2(-8, -24 + (entity.IsSwimming() ? 4 : -2f)),
                sourceRectangle,
                Color.White);
        }
    }
}