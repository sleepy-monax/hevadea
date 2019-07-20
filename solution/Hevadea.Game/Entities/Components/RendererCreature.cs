using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class RendererCreature : Renderer
    {
        private static readonly int[] FRAMES = { 0, 2, 1, 2 };
        private static readonly Vector2[] PICKEDUP_ENTITY_OFFSETS = { new Vector2(0, 1), new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 0) };

        private _Sprite _defaultSprite;
        private _Sprite _liftingSprite;
        private _Sprite _waterEffect;

        public RendererCreature(_Sprite defaultSprite, _Sprite liftingSprite = null)
        {
            _defaultSprite = defaultSprite.WithGrid(3, 4);

            if (liftingSprite != null)
            {
                _liftingSprite = liftingSprite.WithGrid(3, 4);
            }
            else
            {
                _liftingSprite = _defaultSprite;
            }

            _waterEffect = Resources.Sprites["water"].WithGrid(4, 1);
        }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            var entitySprite = _defaultSprite;
            var pickedUpEntityPosition = position;

            if (entity.IsHolding())
            {
                entitySprite = _liftingSprite;
                pickedUpEntityPosition -= new Vector2(0, 18);
            }

            if (entity.IsMoving())
            {
                var moveFrame = FRAMES[(int)((8f * gameTime.GetTotalTime()) % 4)];
                entitySprite = entitySprite.SubSprite(moveFrame, (int)Owner.Facing);
                pickedUpEntityPosition += PICKEDUP_ENTITY_OFFSETS[(int)((8f * gameTime.GetTotalTime()) % 4)];
            }
            else
            {
                entitySprite = entitySprite.SubSprite(2, (int)Owner.Facing);
            }

            if (entity.IsSwimming())
            {
                var waterDepth = 0.6f;

                var waterSprite = _waterEffect.SubSprite((int)(8 * gameTime.GetTotalTime()) % 5, 0);
                var upperHalf = entitySprite.UpperHalf(waterDepth);
                var bottomHalf = entitySprite.BottomHalf(waterDepth);

                spriteBatch.DrawSprite(bottomHalf, position + new Vector2(-entitySprite.Width / 2, 0), Color.DimGray * 0.5f);
                spriteBatch.DrawSprite(waterSprite, position - new Vector2(waterSprite.Width / 2, waterSprite.Height / 2), Color.White);
                spriteBatch.DrawSprite(upperHalf, position + new Vector2(-entitySprite.Width / 2, -upperHalf.Height), Color.White);

                pickedUpEntityPosition += new Vector2(0, upperHalf.Height - bottomHalf.Height);

            }
            else
            {
                spriteBatch.DrawSprite(entitySprite, position + new Vector2(-entitySprite.Width / 2, -entitySprite.Height) + new Vector2(0, 6), Color.White);
            }

            var pickedUpEntity = entity.GetHoldedEntity();

            if (pickedUpEntity != null && pickedUpEntity.HasComponent<Renderer>())
            {
                var renderer = pickedUpEntity.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.Render(spriteBatch, pickedUpEntity, pickedUpEntityPosition, gameTime);
                }
            }
        }
    }
}