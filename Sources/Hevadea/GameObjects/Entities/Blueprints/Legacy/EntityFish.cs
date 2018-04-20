using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Ai;
using Hevadea.GameObjects.Entities.Components.Ai.Behaviors;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Blueprints.Legacy
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
            AddComponent(new Pushable { CanBePushBy = { EntityFactory.PLAYER } });
            AddComponent(new Agent { Behavior = new BehaviorAnimal() { NaturalEnvironment = { TILES.WATER }, MoveSpeedWandering = 0.5f } });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}
