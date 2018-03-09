using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components.Render
{
    public class NpcRender : EntityComponent, IEntityComponentDrawable, IEntityComponentUpdatable
    {
        private bool _isWalking;
        private bool _isSwiming;

        public Sprite Sprite { get; set; }

        public NpcRender(Sprite sprite)
        {
            Sprite = sprite;
            Priority = 16;
        }

        public void Update(GameTime gameTime)
        {
            _isWalking = Owner.Get<Move>()?.IsMoving ?? false;
            _isSwiming = Owner.Get<Swim>()?.IsSwiming ?? false;
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            var frame = new[] {0, 2, 1, 2}[(int) (gameTime.TotalGameTime.TotalSeconds * 8 % 4)];
            var selectedFrame = _isWalking ? new Point(frame, (int) Owner.Facing) : new Point(2, (int) Owner.Facing);

            Sprite.DrawSubSprite(spriteBatch, new Vector2(Owner.X - 8, Owner.Y - 24), selectedFrame, Color.White);

            if (_isSwiming)
            {                
                Ressources.SprUnderWater
                .DrawSubSprite(spriteBatch, new Vector2(Owner.X - 8, Owner.Y - 24), selectedFrame, Color.White);
            }
        }
    }
}