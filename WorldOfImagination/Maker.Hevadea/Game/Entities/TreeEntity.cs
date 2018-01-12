using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Registry;
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

            AddComponent(new HealthComponent(20));
            AddComponent(new InteractableComponent());
            GetComponent<HealthComponent>().OnDie += (sender, args) =>
            {
                for (int i = 0; i < Game.Random.Next(1, 4); i++)
                {
                    var dropWood = new ItemEntity(ITEMS.WOOD_LOG, Game.Random.Next(-50,50) / 10f, Game.Random.Next(-50, 50) / 10f);
                    Level.AddEntity(dropWood);
                    dropWood.SetPosition(X, Y);
                }
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