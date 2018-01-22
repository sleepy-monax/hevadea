using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Maker.Hevadea.Enum;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class Attack : EntityComponent, IUpdatableComponent, IDrawableComponent
    {
        public bool IsAttacking = false;
        public Direction AttackDirection  { get; private set; } = Direction.Up;
        public double AttackCooldownTimer { get; private set; } = 0;
        public double BaseAttackCooldown { get; set; } = 1;
        public double AttackCouldown { get; private set; } = 1;

        public int BaseDamages { get; set; }
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;

        public Attack(int baseDamages)
        {
            BaseDamages = baseDamages;
        }

        public void Do(Item weapon)
        {
            if (!IsAttacking)
            {
                float multiplier = 1f;
                var energy = Owner.Components.Get<Energy>();

                if (energy != null)
                {
                    multiplier = energy.Value / energy.MaxValue;
                    if (!energy.Reduce(1f)) return;
                }

                var tilePosition = Owner.GetTilePosition();
                var dir = Owner.Facing.ToPoint();

                tilePosition = new TilePosition(tilePosition.X + dir.X, tilePosition.Y + dir.Y);

                var entities = Owner.Level.GetEntitiesOnArea(new Rectangle((int)(Owner.X + Owner.Height * dir.X),
                                                                           (int)(Owner.Y + Owner.Width * dir.Y),
                                                                           Owner.Height, Owner.Width));
                if (CanAttackEntities && entities.Count > 0)
                {
                    foreach (var e in entities)
                    {
                        if (!e.Components.Has<Health>() || e.Components.Get<Health>().Invicible) continue;
                        weapon.Attack(Owner, e, BaseDamages * multiplier);
                        IsAttacking = true;
                        break;
                    }
                }

                if (CanAttackTile && !IsAttacking)
                {
                    weapon.Attack(Owner, tilePosition, BaseDamages);
                    IsAttacking = true;
                }

                AttackDirection = Owner.Facing;
                AttackCooldownTimer = AttackCouldown;
            }
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
