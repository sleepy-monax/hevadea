using System;
using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class HealthComponent : EntityComponent, IDrawableComponent, IUpdatableComponent
    {
        public float HealthPercent => Health / (float)MaxHealth;
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;

        public delegate void OnDieHandler(object sender, EventArgs e);
        public event OnDieHandler OnDie;

        public HealthComponent(int maxHealth)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public virtual void Hurt(Entity entity, int damages, Direction attackDirection)
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
        public virtual void Hurt(Tile tile, int damages, int tileX, int tileY)
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
        public virtual void Heal(Entity entity, int damages, Direction attackDirection)
        {
            Health = Math.Min(MaxHealth, Health + damages);
        }

        // The entity in heal b
        public virtual void Heal(Tile tile, int damages, int tileX, int tileY)
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
            // TODO Heal bar

            if (ShowHealthBar && (Health != MaxHealth))
            {
                var barY = Owner.Y + Owner.Height + 8;
                var barX = Owner.X + Owner.Width / 2 - 16;
                
                spriteBatch.FillRectangle(new Rectangle((int)barX, (int)barY, 32, 8), Color.Black * 0.25f);
                spriteBatch.FillRectangle(new Rectangle((int)barX, (int)barY, (int)(32 * HealthPercent), 8), Color.Red * (1f - HealthPercent));
                spriteBatch.FillRectangle(new Rectangle((int)barX, (int)barY, (int)(32 * HealthPercent), 8), Color.Green * HealthPercent);
            }
        }
    }
}