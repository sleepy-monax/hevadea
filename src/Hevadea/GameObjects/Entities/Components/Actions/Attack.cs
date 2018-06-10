using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Hevadea.GameObjects.Items.Tags;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Components;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.GameObjects.Entities.Components.Actions
{
    public class Attack : EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawable
    {
        public bool IsAttacking { get; private set; }

        public float BaseDamages { get; set; } = 1f;
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;
        public double AttackCooldown { get; set; } = 1;

        public int HitBoxSize { get; set; } = 26;

        private Direction _lastDirection = Direction.North;
        private double _speedFactor = 1;
        private double _timer;

        private Sprite _swingUP;
        private Sprite _swingDown;
        private Sprite _swingLeft;
        private Sprite _swingRight;

        public Attack()
        {
            _swingUP = new Sprite(Ressources.TileIcons, 5);
            _swingDown = new Sprite(Ressources.TileIcons, 3);
            _swingLeft = new Sprite(Ressources.TileIcons, 6);
            _swingRight = new Sprite(Ressources.TileIcons, 4);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsAttacking) return;
            var invTimer = 1f - _timer;
            var HitBox = Owner.GetFacingArea(26);

            switch (_lastDirection)
            {
                case Direction.North:
                    _swingUP.Draw(spriteBatch, HitBox, Color.White);
                    break;

                case Direction.East:
                    _swingRight.Draw(spriteBatch, HitBox, Color.White);
                    break;

                case Direction.South:
                    _swingDown.Draw(spriteBatch, HitBox, Color.White);
                    break;

                case Direction.West:
                    _swingLeft.Draw(spriteBatch, HitBox, Color.White);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            _timer = Math.Max(0.0, _timer - gameTime.ElapsedGameTime.TotalSeconds * 5);

            if (!IsAttacking) _speedFactor = AttackCooldown;

            if (IsAttacking && _timer < 0.1)
            {
                _speedFactor = _speedFactor * 0.9;
                IsAttacking = false;
            }
        }

        public float GetBaseDamages()
        {
            var damages = BaseDamages;

            var energy = Owner.GetComponent<Energy>();

            if (energy != null) damages = damages * (energy.Value / energy.MaxValue);

            return damages;
        }

        public float GetDamages(Item weapon, Entity target)
        {
            return GetBaseDamages() * (weapon?.Tag<DamageTag>()?.GetDamages(target) ?? 1f);
        }

        public float GetDamages(Item weapon, Tile tile)
        {
            return GetBaseDamages() * (weapon?.Tag<DamageTag>()?.GetDamages(tile) ?? 1f);
        }

        public bool AttackEntity(Item weapon, Entity target)
        {
            var breakable = target.GetComponent<Breakable>();
            var health = target.GetComponent<Health>();

            if (breakable != null)
                breakable.Break();
            else if (health != null)
                health.Hurt(Owner, GetDamages(weapon, target), Owner.Facing);
            else return false;

            return true;
        }

        public bool AttackTile(Item weapon, Coordinates tilePosition)
        {
            var tile = Owner.Level.GetTile(tilePosition);

            var damages = tile.Tag<DamageTile>();
            var breakable = tile.Tag<BreakableTile>();

            if (breakable != null)
                breakable.Break(tilePosition, Owner.Level);
            else if (damages != null)
                damages.Hurt(GetDamages(weapon, tile), tilePosition, Owner.Level);
            else return false;

            return true;
        }

        public void Do(Item weapon)
        {
            //TODO: rework this function.

            if (IsAttacking) return;
            if (Owner.GetComponent<Pickup>()?.HasPickedUpEntity() ?? false) return;

            var baseDamages = GetBaseDamages();
            if (!Owner.GetComponent<Energy>()?.Reduce(1f) ?? false) return;
            var facingTile = Owner.GetFacingTile();

            if (CanAttackEntities)
            {
                var facingEntities = Owner.GetFacingEntities(HitBoxSize);

                foreach (var e in facingEntities)
                    if (e != Owner && AttackEntity(weapon, e))
                    {
                        IsAttacking = true;
                        break;
                    }
            }

            if (CanAttackTile && !IsAttacking && AttackTile(weapon, Owner.GetFacingTile()))
                IsAttacking = true;

            if (IsAttacking && Owner == Owner.GameState.LocalPlayer.Entity)
                Owner.GameState.Camera.Thrauma += 0.15f;

            _lastDirection = Owner.Facing;
            _timer = _speedFactor;
        }
    }
}