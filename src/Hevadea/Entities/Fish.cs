using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Ai;
using Hevadea.Entities.Components.Ai.Behaviors;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hevadea.Registry;

namespace Hevadea.Entities
{
    public class Fish : Entity
    {
        private Sprite _sprite;

        public Fish()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(11, 0));

            AddComponent(new Agent(new BehaviorAnimal() { NaturalEnvironment = { TILES.WATER }, MoveSpeedWandering = 0.5f }));
            AddComponent(new Breakable());
            AddComponent(new Colider(new Rectangle(-4, -4, 8, 8)));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.RAW_FISH, 1f, 1, 1) } });
            AddComponent(new Move());
            AddComponent(new Pushable());
            AddComponent(new Swim { IsSwimingPainfull = false });
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Vector2(X - 8f, Y - 8f), Color.White);
        }
    }
}