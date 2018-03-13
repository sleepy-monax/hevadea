using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityStairs : Entity
    {
        public int Destination { get; set; } = 1;
        public bool GoUp { get; set; } = false;
        
        private Sprite _spriteUp;
        private Sprite _spriteDown;
        
        public EntityStairs(bool goUp, int destination) : this()
        {
            GoUp = goUp;
            Destination = destination;
        }

        public EntityStairs()
        {
            SortingOffset = -16;
            var interaction = new Interactable();
            Attach(interaction);
            interaction.OnInteracte +=
                (sender, arg) =>
                {
                    Level.RemoveEntity(arg.Entity);
                    World.GetLevel(Destination).AddEntity(arg.Entity);
                };
            
            _spriteUp = new Sprite(Ressources.TileEntities, new Point(8, 0));
            _spriteDown = new Sprite(Ressources.TileEntities, new Point(8, 1));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (GoUp)
            {
                _spriteUp.Draw(spriteBatch, new Rectangle((int)X - 8, (int) Y - 8, 16, 16), Color.White);
            }
            else
            {
                _spriteDown.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
            }
        }

        public override void OnSave(EntityStorage store)
        {
            store.Set(nameof(Destination), Destination);
            store.Set(nameof(GoUp), GoUp);
        }

        public override void OnLoad(EntityStorage store)
        {
            Destination = store.GetInt(nameof(Destination), Destination);
            GoUp = store.GetBool(nameof(GoUp), GoUp);
        }
    }
}