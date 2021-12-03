using System;
using Hevadea.Storage;
using Hevadea.Tiles;
using Hevadea.Utils;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public sealed class ComponentHealth : EntityComponent, IEntityComponentUpdatable, IEntityComponentSaveLoad
    {
        private float _knckbckX, _knckbckY, _coolDown;

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

        public ComponentHealth(float maxHealth)
        {
            Value = maxHealth;
            MaxValue = maxHealth;
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
            _coolDown += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (NaturalRegeneration && _coolDown > 5)
                Value = (float) Math.Min(MaxValue,
                    Value + NaturalregenerationSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (_knckbckX != 0f || _knckbckY != 0f)
            {
                Owner.GetComponent<ComponentMove>()?.Do(_knckbckX, _knckbckY);
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

                if (dir.Length() > 1f) dir.Normalize();

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

            if (TakeKnockback && Owner.HasComponent<ComponentMove>())
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

        public void Restore()
        {
            Value = MaxValue;
        }

        public void Die()
        {
            Owner.GetComponent<ComponentPickup>()?.LayDownEntity();
            Owner.GetComponent<ComponentInventory>()?.Content.DropOnGround(Owner.Level, Owner.X, Owner.Y);
            Owner.GetComponent<ComponentDropable>()?.Drop();
            Owner.GetComponent<ComponentDropExperience>()?.Drop();
            Owner.Remove();

            Killed?.Invoke(this, null);
        }
    }
}