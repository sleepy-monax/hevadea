using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.Renderer
{
    public class MobRenderer : RendererComponent
    {
        public static readonly int[] FRAMES = { 0, 2, 1, 2 };

        public Texture2D Texture { get; }
        float _animationTime = 0f;

        public MobRenderer(Texture2D texture)
        {
            Texture = texture;
        }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            _animationTime += 8f * gameTime.GetDeltaTime();

            var frame = FRAMES[(int)(_animationTime % 4)];

            var framePosition = entity.IsMoving() ? new Point(frame * 16, (int)Owner.Facing * 32) : new Point(2 * 16, (int)Owner.Facing * 32);
            var frameSize     = entity.IsSwiming() ? new Point(16, 20) : new Point(16, 32);

            var sourceRectangle = new Rectangle(framePosition, frameSize);

            spriteBatch.Draw(Texture, position + new Vector2(-8, -24 + (entity.IsSwiming() ? 4 : -2f)), sourceRectangle, Color.White);
        }
    }
}
