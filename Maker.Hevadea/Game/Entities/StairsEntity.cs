﻿using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Storage;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities
{
    public class StairsEntity : Entity
    {
        public int Destination { get; set; } = 1;
        public bool GoUp { get; set; } = false;
        
        private Sprite _spriteUp;
        private Sprite _spriteDown;
        
        public StairsEntity(bool goUp, int destination) : this()
        {
            GoUp = goUp;
            Destination = destination;
        }

        public StairsEntity()
        {
            Width = Height = 16;
            Origin = new Point(0, 0);

            var interaction = new Interactable();
            Components.Add(interaction);
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
                _spriteUp.Draw(spriteBatch, new Rectangle((int)X, (int) Y, 16, 16), Color.White);
            }
            else
            {
                _spriteDown.Draw(spriteBatch, new Rectangle((int) X, (int) Y, 16, 16), Color.White);
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