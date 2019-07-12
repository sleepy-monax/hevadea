using Hevadea.Framework.Extension;
using Hevadea.Systems.InventorySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.Renderer
{
    public class MobRenderer : RendererComponent
    {
        float _animationTime;

        public static readonly int[] FRAMES = { 0, 2, 1, 2 };
        public Texture2D Texture { get; }

        public MobRenderer(Texture2D texture)
        {
            Texture = texture;
        }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            _animationTime += 8f * gameTime.GetDeltaTime();

            var frame = FRAMES[(int)(_animationTime % 4)];

            var framePosition = entity.IsMoving() ? new Point(frame * 16, (int)Owner.Facing * 32) : new Point(2 * 16, (int)Owner.Facing * 32);
            var frameSize     = entity.IsSwimming() ? new Point(16, 20) : new Point(16, 32);

            var sourceRectangle = new Rectangle(framePosition, frameSize);
            var sourceRectangleShadow = new Rectangle(framePosition, new Point(16, 32));

            if (entity.IsSwimming())
            {
                spriteBatch.Draw(
                    Texture,
                    position + new Vector2(-8, -24 + (entity.IsSwimming() ? 4 : -2f)),
                    sourceRectangleShadow,
                    Color.Black * 0.5f);
            }

            spriteBatch.Draw(
                Texture,
                position + new Vector2(-8, -24 + (entity.IsSwimming() ? 4 : -2f)),
                sourceRectangle,
                Color.White);

            var holdedItem = entity.HoldedItem();

            holdedItem?.GetSprite().Draw(
                spriteBatch,
                new Rectangle(entity.Position.ToPoint(), new Point(16)),
                Color.White);
        }
    }
}
