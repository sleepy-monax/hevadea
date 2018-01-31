using Maker.Hevadea.Enums;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class Attack : EntityComponent, IUpdatableComponent, IDrawableComponent
    {
        public bool IsAttacking = false;
        public Direction AttackDirection  { get; private set; } = Direction.Up;
        public double AttackCooldownTimer { get; private set; } = 0;
        public double BaseAttackCooldown { get; set; } = 1;
        public double AttackCouldown { get; private set; } = 1;

        public float BaseDamages { get; set; }
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;

        public Attack(float baseDamages)
        {
            BaseDamages = baseDamages;
        }

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
            if (IsAttacking) return;
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

            if (CanAttackTile && !IsAttacking)
            {
            
                var tile = Owner.Level.GetTile(facingTile);
                tile.Hurt(Owner, damages * (weapon?.GetAttackBonus(tile) ?? 1f), facingTile, Owner.Facing);
            }

            IsAttacking = true;
            AttackDirection = Owner.Facing;
            AttackCooldownTimer = AttackCouldown;
        }

        public void Update(GameTime gameTime)
        {
            AttackCooldownTimer = Math.Max(0.0, AttackCooldownTimer - gameTime.ElapsedGameTime.TotalSeconds * 5);

            if (!IsAttacking)
            {
                AttackCouldown = BaseAttackCooldown;
            }

            if (IsAttacking && AttackCooldownTimer < 0.1)
            {
                AttackCouldown = AttackCouldown * 0.9;
                IsAttacking = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (IsAttacking)
            {
                var invTimer =  1f - AttackCooldownTimer;
                
                switch (AttackDirection)
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
