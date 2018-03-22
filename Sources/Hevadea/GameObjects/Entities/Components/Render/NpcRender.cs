using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Render
{
    public class NpcRender : Component
    {
        private bool _isWalking;
        private bool _isSwiming;
        private bool _isPickingItem;
        
        public Sprite Sprite { get; set; }

        public NpcRender(Sprite sprite)
        {
            Sprite = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            _isWalking = Entity.GetComponent<Move>()?.IsMoving ?? false;
            _isSwiming = Entity.GetComponent<Swim>()?.IsSwiming ?? false;
            _isPickingItem = (Entity.GetComponent<Inventory>()?.HasPickup ?? false) || ((Entity.GetComponent<Pickup>()?.GetPickupEntity() ?? null) != null);
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            var frame = new[] {0, 2, 1, 2}[(int) (gameTime.TotalGameTime.TotalSeconds * 8 % 4)];
            var selectedFrame = _isWalking ? new Point(frame, (int) Entity.Facing) : new Point(2, (int) Entity.Facing);

            if (_isPickingItem)
            {
                Ressources.SprPickup
                .DrawSubSprite(spriteBatch, new Vector2(Entity.X - 8, Entity.Y - 24), selectedFrame, Color.White);
            }
            else
            {
                Sprite
                .DrawSubSprite(spriteBatch, new Vector2(Entity.X - 8, Entity.Y - 24), selectedFrame, Color.White);
            }

            if (_isSwiming)
            {                
                Ressources.SprUnderWater
                .DrawSubSprite(spriteBatch, new Vector2(Entity.X - 8, Entity.Y - 24), selectedFrame, Color.White);
            }
        }
    }
}