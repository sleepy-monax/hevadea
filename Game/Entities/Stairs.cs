using Hevadea.Entities.Components;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Stairs : Entity
    {
        public int Destination { get; set; } = 1;
        public bool GoUp { get; set; } = false;

        private _Sprite _spriteUp;
        private _Sprite _spriteDown;

        public Stairs()
        {
            SortingOffset = -16;
            var interaction = new ComponentInteractive();
            interaction.Interacted +=
                (sender, arg) =>
                {
                    Level.RemoveEntity(arg.Entity);
                    World.GetLevel(Destination).AddEntityAt(arg.Entity, Coordinates);
                };

            AddComponent(interaction);

            _spriteUp = Resources.Sprites["entity/stair_up"];
            _spriteDown = Resources.Sprites["entity/stair_down"];
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawSprite(GoUp ? _spriteUp : _spriteDown, Position - new Vector2(8), Color.White);
        }

        public override void OnSave(EntityStorage store)
        {
            store.Value(nameof(GoUp), GoUp);
            store.Value(nameof(Destination), Destination);
        }

        public override void OnLoad(EntityStorage store)
        {
            GoUp = store.ValueOf(nameof(GoUp), GoUp);
            Destination = store.ValueOf(nameof(Destination), Destination);
        }
    }
}