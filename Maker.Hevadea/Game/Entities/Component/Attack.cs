using System;
using Maker.Hevadea.Enums;
using Maker.Hevadea.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Attack : EntityComponent, IUpdatableComponent, IDrawableComponent
    {
        public float BaseDamages { get; set; } = 1f;
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;
        public double AttackCooldown { get; set; } = 1;

        private Direction _lastDirection = Direction.Up;
        private bool _isAttacking = false;
        private double _speedFactor = 1;
        private double _timer = 0;

        public float GetBaseDamages()
        {
            var damages = BaseDamages;

            var energy = Owner.Components.Get<Energy>();

            if (energy != null)
            {
                damages = damages * (energy.Value / energy.MaxValue);
            }

            return damages;
        }

        public void Do(Item weapon)
        {
            if (_isAttacking) return;
            if (!Owner.Components.Get<Energy>()?.Reduce(1f) ?? false) return;

            var damages = GetBaseDamages();
            var facingTile = Owner.GetFacingTile();

            if (CanAttackEntities)
            {
                var facingEntities = Owner.Level.GetEntityOnTile(facingTile);

                foreach (var e in facingEntities)
                {

                    if (e != Owner)
                    {
                        e.Components.Get<Breakable>()?.Break();

                        var eHealth = e.Components.Get<Health>();
                        if ((!eHealth?.Invicible ?? false))
                        {
                            eHealth.Hurt(Owner, damages * (weapon?.GetAttackBonus(e) ?? 1f), Owner.Facing);

                            break;
                        }
                    }

                }
            }

            if (CanAttackTile && !_isAttacking)
            {
            
                var tile = Owner.Level.GetTile(facingTile);
                tile.Hurt(Owner, damages * (weapon?.GetAttackBonus(tile) ?? 1f), facingTile, Owner.Facing);
            }

            _isAttacking = true;
            _lastDirection = Owner.Facing;
            _timer = _speedFactor;
        }

        public bool IsAttacking()
        {
            return _isAttacking;
        }
        
        public void Update(GameTime gameTime)
        {
            _timer = Math.Max(0.0, _timer - gameTime.ElapsedGameTime.TotalSeconds * 5);

            if (!_isAttacking)
            {
                _speedFactor = AttackCooldown;
            }

            if (_isAttacking && _timer < 0.1)
            {
                _speedFactor = _speedFactor * 0.9;
                _isAttacking = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_isAttacking)
            {
                var invTimer =  1f - _timer;
                
                switch (_lastDirection)
                {
                    case Direction.Up:
                        spriteBatch.Draw(Ressources.img_swing, new Rectangle((int)(Owner.X), (int)(Owner.Y - Owner.Height), (int)(Owner.Width * invTimer), Owner.Height), Color.White);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(Ressources.img_swing, new Rectangle((int)(Owner.X + Owner.Width), (int)(Owner.Y), Owner.Width, (int)(Owner.Height * invTimer)), Color.White);
                        break;
                    case Direction.Down:
                        spriteBatch.Draw(Ressources.img_swing, new Rectangle((int)(Owner.X), (int)(Owner.Y + Owner.Height), (int)(Owner.Width * invTimer), Owner.Height), Color.White);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(Ressources.img_swing, new Rectangle((int)(Owner.X - Owner.Width), (int)(Owner.Y), Owner.Width, (int)(Owner.Height * invTimer)), Color.White);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            //spriteBatch.FillRectangle(new Rectangle((int)Owner.X, (int)Owner.Y, (int)(64 * AttackCooldownTimer), 16), Color.Red);
        }
    }
}
