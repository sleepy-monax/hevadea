using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class HealthComponent : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public float HealthPercent => Health / (float)MaxHealth;
        public float Health { get; private set; }
        public float MaxHealth { get; private set; }
        
        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;
        public bool NaturalRegeneration { get; set; } = false;

        public delegate void OnDieHandler(object sender, EventArgs e);
        public event OnDieHandler OnDie;

        public HealthComponent(float maxHealth)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public virtual void Hurt(Entity entity, float damages, Direction attackDirection)
        {
            if (!Invicible)
            {
                Health = Math.Max(0, Health - damages);

                if (Health == 0)
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
                Health = Math.Max(0, Health - damages);

                if (Health == 0)
                {
                    Die();
                }
            }
        }


        // The mob is heal by a mod (healing itself)
        public virtual void Heal(Entity entity, float damages, Direction attackDirection)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        // The entity in heal b
        public virtual void Heal(Tile tile, float damages, int tileX, int tileY)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        public virtual void Die()
        {
            if (OnDie != null)
            {
                OnDie.Invoke(this, null);
            }
            Owner.Remove();
        }

        public void Update(GameTime gameTime)
        {
            //TODO Heal regeneration
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ShowHealthBar && (Health != MaxHealth))
            {
                var barY = Owner.Y + Owner.Height + 8;
                var barX = Owner.X + Owner.Width / 2 - 16;
                
                spriteBatch.FillRectangle(new Rectangle((int)barX, (int)barY, 32, 8), Color.Black * 0.45f);
                spriteBatch.FillRectangle(new Rectangle((int)barX + 1, (int)barY + 1, (int)(30 * HealthPercent), 6), Color.Red);
                spriteBatch.FillRectangle(new Rectangle((int)barX + 1, (int)barY + 1, (int)(30 * HealthPercent), 6), Color.Green * HealthPercent);
                spriteBatch.FillRectangle(new Rectangle((int)barX + 1, (int)barY + 1, (int)(30 * HealthPercent), 3), Color.White * 0.25f);
            }
        }

        public void OnSave(EntityStorage store)
        {
            store.Set(nameof(Health), Health);
        }

        public void OnLoad(EntityStorage store)
        {
            Health = store.Get(nameof(Health), Health);
        }
    }
}