using System;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public sealed class Health : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public delegate void GetHurtByEntityHandle(Entity entity, float damages, Direction attackDirection);

        public delegate void GetHurtByTileHandler(Tile tile, float damages, int tX, int tY);

        public delegate void OnDieHandler(object sender, EventArgs e);

        private readonly float _maxValue;

        private float _value;

        public Health(float maxHealth)
        {
            _value = maxHealth;
            _maxValue = maxHealth;
        }

        public bool Invicible { get; set; } = false;
        public bool ShowHealthBar { get; set; } = true;
        public bool NaturalRegeneration { get; set; } = false;
        private double _valuePercent => _value / _maxValue;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ShowHealthBar && Math.Abs(_value - _maxValue) > 0.05)
            {
                var barY = Owner.Y + Owner.Origin.Y + 8;
                var barX = Owner.X + Owner.Origin.X - 16;
                var rect = new Rectangle((int) barX, (int) barY, (int) (30 * _valuePercent), 6);

                spriteBatch.FillRectangle(new Rectangle((int) barX + 1, (int) barY + 1, rect.Width, rect.Height),
                    Color.Black * 0.45f);
                spriteBatch.FillRectangle(rect, Color.Red);
                spriteBatch.FillRectangle(rect, Color.SeaGreen * (float) _valuePercent);
            }
        }

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(_value), _value);
        }

        public void OnGameLoad(EntityStorage store)
        {
            _value = store.Get(nameof(_value), _value);
        }

        public void Update(GameTime gameTime)
        {
            if (NaturalRegeneration)
            {
            }
        }

        public event OnDieHandler OnDie;
        public event GetHurtByTileHandler GetHurtByTile;
        public event GetHurtByEntityHandle GetHurtByEntity;

        public double GetValue()
        {
            return _valuePercent;
        }

        // Entity get hurt by a other entity (ex: Zombie)
        public void Hurt(Entity entity, float damages, Direction attackDirection)
        {
            if (Invicible) return;

            GetHurtByEntity?.Invoke(entity, damages, attackDirection);
            _value = Math.Max(0, _value - damages);

            if (Math.Abs(_value) < 0.1f) Die();
        }

        // Entity get hurt by a tile (ex: lava)
        public void Hurt(Tile tile, float damages, int tX, int tY)
        {
            if (Invicible) return;

            GetHurtByTile?.Invoke(tile, damages, tX, tY);
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
            OnDie?.Invoke(this, null);
            Owner.Components.Get<Dropable>()?.Drop();
            Owner.Remove();
        }
    }
}