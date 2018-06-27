using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Stairs : Entity
    {
        public int Destination { get; set; } = 1;
        public bool GoUp { get; set; } = false;

        private Sprite _spriteUp;
        private Sprite _spriteDown;

        public Stairs(bool goUp, int destination) : this()
        {
            GoUp = goUp;
            Destination = destination;
        }

        public Stairs()
        {
            SortingOffset = -16;
            var interaction = new Interactable();
            AddComponent(interaction);
            interaction.Interacted +=
                (sender, arg) =>
                {
                    Level.RemoveEntity(arg.Entity);
                    World.GetLevel(Destination).AddEntityAt(arg.Entity, Coordinates);
                };

            _spriteUp = new Sprite(Ressources.TileEntities, new Point(8, 0));
            _spriteDown = new Sprite(Ressources.TileEntities, new Point(8, 1));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (GoUp)
            {
                _spriteUp.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
            }
            else
            {
                _spriteDown.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
            }
        }

        public override void OnSave(EntityStorage store)
        {
            store.Value(nameof(Destination), Destination);
            store.Value(nameof(GoUp), GoUp);
        }

        public override void OnLoad(EntityStorage store)
        {
            Destination = store.ValueOf(nameof(Destination), Destination);
            GoUp = store.ValueOf(nameof(GoUp), GoUp);
        }
    }
}