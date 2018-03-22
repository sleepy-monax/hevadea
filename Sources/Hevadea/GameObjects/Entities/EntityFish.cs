using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Registry;
using Hevadea.GameObjects.Entities.Components.Ai;
using Hevadea.GameObjects.Entities.Components.Ai.Behaviors;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Interaction;
using Hevadea.GameObjects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class EntityFish : Entity
    {        
        private readonly Sprite _sprite;
        
        public EntityFish()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(11, 0));

            Attach(new Colider(new Rectangle(-4, -4, 8, 8)));
            Attach(new Move());
            Attach(new Swim { IsSwimingPainfull = false});
            Attach(new Breakable());
            Attach(new Dropable { Items = { new Drop(ITEMS.RAW_FISH, 1f, 1, 1) } });
            Attach(new Pushable { CanBePushBy = { ENTITIES.PLAYER } });
            Attach(new Agent { Behavior = new BehaviorAnimal() { NaturalEnvironment = { TILES.WATER }, MoveSpeed = 0.5f } });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}
