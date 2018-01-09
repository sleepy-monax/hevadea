using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Render
{
    public class NpcRenderComponent : EntityComponent, IDrawableComponent
    {
        public Sprite Sprite { get; set; }

        public NpcRenderComponent(Sprite sprite)
        {
            Sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var animationFrame = (int)(gameTime.TotalGameTime.TotalSeconds * 4 % 4);
            var isWalking = false;

            if (Owner.HasComponent<MoveComponent>())
            {
                var moveComponent = Owner.GetComponent<MoveComponent>();
                isWalking = moveComponent.IsMoving;
            }

            if (isWalking)
            {
                if (animationFrame == 1)
                {
                    animationFrame = 2;
                }
                else if (animationFrame == 2)
                {
                    animationFrame = 1;
                }
                else if (animationFrame == 3)
                {
                    animationFrame = 2;
                }

                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18), new Point(animationFrame, (int)Owner.Facing),
                    Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 4, Owner.Y - 18), new Point(2, (int)Owner.Facing), Color.White);
            }

            isWalking = false;
        }
    }


}
