using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Entities.Component.Interaction
{
    public class AttackComponent : EntityComponent, IUpdatableComponent, IDrawableComponent
    {
        public bool IsAttacking = false;
        public double AttackCooldownTimer { get; private set; } = 0;
        private double BaseAttackCooldown { get; set; } = 0.35f;
        public double AttackCouldown { get; private set; } = 0.35;

        public int BaseDamages { get; set; }
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;

        public AttackComponent(int baseDamages)
        {
            BaseDamages = baseDamages;
        }

        public void Attack(Item weapon)
        {
            if (!IsAttacking)
            {
                float multiplier = 1f;
                var energy = Owner.GetComponent<EnergyComponent>();

                if (energy != null)
                {
                    multiplier = energy.Energy / energy.MaxEnergy;
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
                        if (!e.HasComponent<HealthComponent>() || e.GetComponent<HealthComponent>().Invicible) continue;
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

                AttackCooldownTimer = AttackCouldown;

            }
        }

        public void Update(GameTime gameTime)
        {
            AttackCooldownTimer = Math.Max(0.0, AttackCooldownTimer - gameTime.ElapsedGameTime.TotalSeconds);

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
            spriteBatch.FillRectangle(new Rectangle((int)Owner.X, (int)Owner.Y, (int)(64 * AttackCooldownTimer), 16), Color.Red);
        }
    }
}
