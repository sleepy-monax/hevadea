using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class TreeEntity : Entity
    {
        Sprite treeSprite;

        public TreeEntity()
        {
            Width = 4;
            Height = 4;
            Origin = new Point(2,2);
            treeSprite = new Sprite(Ressources.tile_entities, 0, new Point(16, 16));

            Components.Add(new HealthComponent(20));
            Components.Add(new InteractableComponent());
            Components.Get<HealthComponent>().OnDie += (sender, args) =>
            {
                ITEMS.WOOD_LOG.Drop(Level, X + Origin.X, Y + Origin.Y, Engine.Random.Next(1, 5));
                ITEMS.PINE_CONE.Drop(Level, X + Origin.X, Y + Origin.Y, Engine.Random.Next(0, 3));
            };
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var offx = -6;
            var offy = -10;
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 32), new Point(0, 1), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 16), new Point(0, 2), Color.White);
            treeSprite.DrawSubSprite(spriteBatch, new Vector2(X + offx, Y + offy - 0), new Point(0, 3), Color.White);
        }

        public override bool IsBlocking(Entity e)
        {
            return e is PlayerEntity || e is ZombieEntity;
        }
    }
}