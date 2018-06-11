using Hevadea.Framework.Graphic;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Tiles;
using Hevadea.Storage;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.GameObjects.Entities.Components.States
{
    public sealed class Health : EntityComponent, IEntityComponentDrawableOverlay, IEntityComponentUpdatable, IEntityComponentSaveLoad
    {
        private float _knckbckX, _knckbckY, _coolDown, _heathbarTimer = 0f;

        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;
        public bool TakeKnockback { get; set; } = true;
        public bool NaturalRegeneration { get; set; } = false;

        public double NaturalregenerationSpeed { get; set; } = 1.0;

        public float Value { get; private set; }
        public float MaxValue { get; }

        public float ValuePercent => Value / MaxValue;

        public delegate void GetHurtByEntityHandle(Entity entity, float damages);

        public delegate void GetHurtByTileHandler(Tile tile, float damages, int tX, int tY);

        public event EventHandler Killed;

        public event GetHurtByTileHandler HurtedByTile;

        public event GetHurtByEntityHandle HurtedByEntity;

        public Health(float maxHealth)
        {
            Value = maxHealth;
            MaxValue = maxHealth;
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ShowHealthBar && Math.Abs(Value - MaxValue) > 0.05)
            {
                var barY = Owner.Y + 4;
                var barX = Owner.X - 15;

                var rect = new Rectangle((int)barX, (int)barY, (int)(30 * ValuePercent), 2);

                spriteBatch.FillRectangle(new Rectangle((int)barX + 1, (int)barY + 1, rect.Width, rect.Height),
                    Color.Black * 0.45f);

                var red = (int)Math.Sqrt(255 * 255 * (1 - ValuePercent));
                var green = (int)Math.Sqrt(255 * 255 * (ValuePercent));
                spriteBatch.FillRectangle(rect, new Color(red, green, 0));
            }
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Value(nameof(Heal), Value);
            if (NaturalRegeneration)
                store.Value("natural_regeneration", _coolDown);
        }

        public void OnGameLoad(EntityStorage store)
        {
            Value = store.ValueOf(nameof(Heal), Value);
            if (NaturalRegeneration)
                _coolDown = store.ValueOf("natural_regeneration", 0f);
        }

        public void Update(GameTime gameTime)
        {
            _coolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (NaturalRegeneration && _coolDown > 5)
            {
                Value = (float)Math.Min(MaxValue, Value + NaturalregenerationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (_knckbckX != 0f || _knckbckY != 0f)
            {
                Owner.GetComponent<Move>()?.Do(_knckbckX, _knckbckY);
                _knckbckX *= 0.9f;
                _knckbckY *= 0.9f;
            }
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public void Hurt(Entity entity, float damages, bool knockback = true)
        {
            if (knockback)
            {
                var dir = new Vector2(Owner.X - entity.X, Owner.Y - entity.Y);

                if (dir.Length() > 1f)
                {
                    dir.Normalize();
                }

                Hurt(entity, damages, dir.X, dir.Y);
            }
            else
            {
                Hurt(entity, damages, 0, 0);
            }
        }

        public void Hurt(Entity entity, float damages, Direction attackDirection)
        {
            var dir = attackDirection.ToPoint();

            Hurt(entity, damages, dir.X * damages, dir.Y * damages);
        }

        public void Hurt(Entity entity, float damages, float knockbackX, float knockbackY)
        {
            if (Invicible) return;
            _coolDown = 0f;
            HurtedByEntity?.Invoke(entity, damages);
            Value = Math.Max(0, Value - damages);

            if (TakeKnockback && Owner.HasComponent<Move>())
            {
                _knckbckX += knockbackX;
                _knckbckY += knockbackY;
            }

            if (Math.Abs(Value) < 0.1f) Die();
        }

        // Entity get hurt by a tile (ex: lava)
        public void Hurt(Tile tile, float damages, int tX, int tY)
        {
            if (Invicible) return;
            _coolDown = 0f;
            HurtedByTile?.Invoke(tile, damages, tX, tY);
            Value = Math.Max(0, Value - damages);

            if (Math.Abs(Value) < 0.1f) Die();
        }

        // The mob is heal by a mod (healing itself)
        public void Heal(Entity entity, float damages, Direction attackDirection)
        {
            Value = Math.Min(MaxValue, Value + damages);
        }

        // The entity in heal b
        public void Heal(Tile tile, float damages, int tileX, int tileY)
        {
            Value = Math.Min(MaxValue, Value + damages);
        }

        public void HealAll()
        {
            Value = MaxValue;
        }

        public void Die()
        {
            Owner.GetComponent<Pickup>()?.LayDownEntity();
            Owner.GetComponent<Inventory>()?.Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.GetComponent<Dropable>()?.Drop();
            Owner.Remove();

            Killed?.Invoke(this, null);
        }
    }
}