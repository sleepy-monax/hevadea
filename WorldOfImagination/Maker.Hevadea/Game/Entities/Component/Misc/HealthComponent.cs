using System;
using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class HealthComponent : EntityComponent, IDrawableComponent, IUpdatableComponent
    {
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
            OnDie?.Invoke(this, null);
            Owner.Remove();
        }

        public void Update(GameTime gameTime)
        {
            //TODO Heal regeneration
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // TODO Heal bar

            if (ShowHealthBar)
            {

            }
        }
    }
}