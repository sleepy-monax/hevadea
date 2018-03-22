using Hevadea.Framework.Graphic;
using Hevadea.Game.Storage;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Hevadea.Framework.Utils;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public sealed class Health : EntityComponent, IEntityComponentDrawableOverlay, IEntityComponentUpdatable, IEntityComponentSaveLoad
    {
        private float _maxValue, _value, _knckbckX, _knckbckY, _coolDown;
        
        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;
        public bool TakeKnockback { get; set; } = true;
        
        public bool NaturalRegeneration { get; set; } = false;
        public double NaturalregenerationSpeed { get; set; } = 1.0;
        
        public float Value => _value;
        public float MaxValue => _maxValue;
        public float ValuePercent => _value / _maxValue;

        public delegate void GetHurtByEntityHandle(Entity entity, float damages);
        public delegate void GetHurtByTileHandler(Tile tile, float damages, int tX, int tY);
        
        public event EventHandler Killed;
        public event GetHurtByTileHandler HurtedByTile;
        public event GetHurtByEntityHandle HurtedByEntity;

        public Health(float maxHealth)
        {
            _value = maxHealth;
            _maxValue = maxHealth;
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ShowHealthBar && Math.Abs(_value - _maxValue) > 0.05)
            {
                var barY = Owner.Y + 8;
                var barX = Owner.X - 16;
                
                var rect = new Rectangle((int) barX, (int) barY, (int) (30 * ValuePercent), 6);

                spriteBatch.FillRectangle(new Rectangle((int) barX + 1, (int) barY + 1, rect.Width, rect.Height),
                    Color.Black * 0.45f);

                var red =   (int)Math.Sqrt(255 * 255 * (1 - ValuePercent));
                var green = (int)Math.Sqrt(255 * 255 * (ValuePercent));
                spriteBatch.FillRectangle(rect, new Color(red, green, 0));
            }
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(Heal), _value);
            if (NaturalRegeneration)
                store.Set("natural_regeneration", _coolDown);
        }

        public void OnGameLoad(EntityStorage store)
        {
            _value = store.GetFloat(nameof(Heal), _value);
            if (NaturalRegeneration)
                _coolDown = store.GetFloat("natural_regeneration");
        }

        public void Update(GameTime gameTime)
        {
            _coolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (NaturalRegeneration && _coolDown > 5)
            {
                _value = (float)Math.Min(_maxValue, _value + NaturalregenerationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (_knckbckX != 0f || _knckbckY != 0f)
            {
                Owner.GetComponent<Move>()?.Do(_knckbckX, _knckbckY, Owner.Facing);
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
            _value = Math.Max(0, _value - damages);

            if (TakeKnockback && Owner.HasComponent<Move>())
            {
                _knckbckX += knockbackX;
                _knckbckY += knockbackY;
            }

            if (Math.Abs(_value) < 0.1f) Die();
        }

        // Entity get hurt by a tile (ex: lava)
        public void Hurt(Tile tile, float damages, int tX, int tY)
        {
            if (Invicible) return;

            HurtedByTile?.Invoke(tile, damages, tX, tY);
            _value = Math.Max(0, _value - damages);

            if (Math.Abs(_value) < 0.1f) Die();
        }


        // The mob is heal by a mod (healing itself)
        public void Heal(Entity entity, float damages, Direction attackDirection)
        {
            _value = Math.Min(_maxValue, _value + damages);
        }

        // The entity in heal b
        public void Heal(Tile tile, float damages, int tileX, int tileY)
        {
            _value = Math.Min(_maxValue, _value + damages);
        }

        public void Die()
        {
            Killed?.Invoke(this, null);
            Owner.GetComponent<Dropable>()?.Drop();
            Owner.Remove();
        }
    }
}