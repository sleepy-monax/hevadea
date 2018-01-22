using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class Health : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public float ValuePercent => Value / (float)MaxValue;
        public float Value { get; private set; }
        public float MaxValue { get; private set; }
        
        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;
        public bool NaturalRegeneration { get; set; } = false;

        public delegate void OnDieHandler(object sender, EventArgs e);
        public event OnDieHandler OnDie;

        public Health(float maxHealth)
        {
            Value = maxHealth;
            MaxValue = maxHealth;
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public virtual void Hurt(Entity entity, float damages, Direction attackDirection)
        {
            if (!Invicible)
            {
                Value = Math.Max(0, Value - damages);

                if (Value == 0)
                {
                    Die();
                }
            }
        }

        // Entity get hurt by a tile (ex: lava)
        public virtual void Hurt(Tile tile, float damages, int tileX, int tileY)
        {
            if (!Invicible)
            {
                Value = Math.Max(0, Value - damages);

                if (Value == 0)
                {
                    Die();
                }
            }
        }


        // The mob is heal by a mod (healing itself)
        public virtual void Heal(Entity entity, float damages, Direction attackDirection)
        {
            Value = Math.Min(MaxValue, Value + damages);
        }

        // The entity in heal b
        public virtual void Heal(Tile tile, float damages, int tileX, int tileY)
        {
            Value = Math.Min(MaxValue, Value + damages);
        }

        public virtual void Die()
        {
            OnDie?.Invoke(this, null);
            Owner.Remove();
        }

        public void Update(GameTime gameTime)
        {
            //TODO Heal regeneration
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ShowHealthBar && (Value != MaxValue))
            {
                var barY = Owner.Y + Owner.Origin.Y + 8;
                var barX = Owner.X + Owner.Origin.X - 16;
                var rect = new Rectangle((int)barX, (int)barY, (int)(30 * ValuePercent), 6);

                spriteBatch.FillRectangle(new Rectangle((int)barX + 1, (int)barY + 1, rect.Width, rect.Height), Color.Black * 0.45f);
                spriteBatch.FillRectangle(rect, Color.Red);
                spriteBatch.FillRectangle(rect, Color.SeaGreen * ValuePercent);
            }
        }

        public void OnSave(EntityStorage store)
        {
            store.Set(nameof(Value), Value);
        }

        public void OnLoad(EntityStorage store)
        {
            Value = store.Get(nameof(Value), Value);
        }
    }
}