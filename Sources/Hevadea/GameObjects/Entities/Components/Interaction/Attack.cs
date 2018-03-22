using System;
using System.Collections.Generic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Utils;
using Hevadea.Game;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components.Interaction
{
    public class Attack : Component
    {
        public bool IsAttacking { get; private set; }
     
        public float BaseDamages { get; set; } = 1f;
        public bool CanAttackTile { get; set; } = true;
        public bool CanAttackEntities { get; set; } = true;
        public double AttackCooldown { get; set; } = 1;

        public Rectangle HitBox { get; private set; }
        public Point HitBoxSize { get; set; } = new Point(26, 26);
        
        private Direction _lastDirection = Direction.Up;
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
        
        public override void Draw(SpriteBatch sb, GameTime gt)
        {
            if (!IsAttacking) return;

            switch (_lastDirection)
            {
                case Direction.Up:
                    _swingUP.Draw(sb, HitBox, Color.White);
                    break;
                case Direction.Right:
                    _swingRight.Draw(sb, HitBox, Color.White);
                    break;
                case Direction.Down:
                    _swingDown.Draw(sb, HitBox, Color.White);
                    break;
                case Direction.Left:
                    _swingLeft.Draw(sb, HitBox, Color.White);
                    break;
            }
        }

        public override void Update(GameTime gt)
        {
            HitBox = new Rectangle(Entity.Position.ToPoint() - new Rectangle(new Point(0), HitBoxSize).GetAnchorPoint(DirectionToAnchore[Entity.Facing]), HitBoxSize);
            _timer = Math.Max(0.0, _timer - gt.ElapsedGameTime.TotalSeconds * 5);

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

            var energy = Entity.GetComponent<Energy>();

            if (energy != null) damages = damages * (energy.Value / energy.MaxValue);

            return damages;
        }

        private static Dictionary<Direction, Anchor> DirectionToAnchore = new Dictionary<Direction, Anchor>()
        {
            {Direction.Up, Anchor.Bottom},
            {Direction.Down, Anchor.Top},
            {Direction.Left, Anchor.Right},
            {Direction.Right, Anchor.Left},
        };
        
        public void Do(Item weapon)
        {
            if (IsAttacking) return;
            if (!Entity.GetComponent<Energy>()?.Reduce(1f) ?? false) return;

            var damages = GetBaseDamages();
            var facingTile = Entity.GetFacingTile();

            if (CanAttackEntities)
            {
                var facingEntities = Entity.Level.GetEntitiesOnArea(HitBox);
                facingEntities.Sort((a, b) =>
                    {
                        return Mathf.Distance(a.X, a.Y, Entity.X, Entity.Y)
                            .CompareTo(Mathf.Distance(b.X, b.Y, Entity.X, Entity.Y));
                    });
                
                foreach (var e in facingEntities)
                    if (e != Entity)
                    {
                        if (e.GetComponent<Breakable>(out var breakable))
                        {
                            breakable.Break(weapon);
                            IsAttacking = true;
                        }

                        if (e.GetComponent<Health>(out var health) && !health.Invicible)
                        {
                            health.Hurt(Entity, damages * (weapon?.GetAttackBonus(e) ?? 1f));
                            IsAttacking = true;
                            break;
                        }
                    }
            }

            if (CanAttackTile && !IsAttacking)
            {
                var tile = Entity.Level.GetTile(facingTile);
                if (tile.HasTag<Tags.Damage>())
                {
                    tile.Tag<Tags.Damage>().Hurt(damages * (weapon?.GetAttackBonus(tile) ?? 1f), facingTile, Entity.Level);   
                }

                if (tile.HasTag<Tags.Breakable>())
                {
                    tile.Tag<Tags.Breakable>().Break(facingTile, Entity.Level);
                }

                IsAttacking = true;
            }

            _lastDirection = Entity.Facing;
            _timer = _speedFactor;
        }
    }
}