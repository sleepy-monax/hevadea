using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Blueprints
{
    public class EntityFish : Entity
    {        
        private readonly Sprite _sprite;
        
        public EntityFish()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(11, 0));

            AddComponent(new Move());
            AddComponent(new Breakable());
            AddComponent(new Swim { IsSwimingPainfull = false});
            AddComponent(new Colider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.RAW_FISH, 1f, 1, 1) } });
            AddComponent(new Pushable { CanBePushBy = { ENTITIES.PLAYER } });
            AddComponent(new Agent { Behavior = new BehaviorAnimal() { NaturalEnvironment = { TILES.WATER }, MoveSpeedWandering = 0.5f } });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}
