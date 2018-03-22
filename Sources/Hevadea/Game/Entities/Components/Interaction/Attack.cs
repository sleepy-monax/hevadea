using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Items;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.UI;
using Hevadea.Framework.Utils;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Attack : EntityComponent, IEntityComponentUpdatable, IEntityComponentDrawable
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
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsAttacking) return;
            var invTimer = 1f - _timer;

            switch (_lastDirection)
            {
                case Direction.Up:
                    _swingUP.Draw(spriteBatch, HitBox, Color.White);
                    break;
                case Direction.Right:
                    _swingRight.Draw(spriteBatch, HitBox, Color.White);
                    break;
                case Direction.Down:
                    _swingDown.Draw(spriteBatch, HitBox, Color.White);
                    break;
                case Direction.Left:
                    _swingLeft.Draw(spriteBatch, HitBox, Color.White);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            HitBox = new Rectangle(Owner.Position.ToPoint() - new Rectangle(new Point(0), HitBoxSize).GetAnchorPoint(DirectionToAnchore[Owner.Facing]), HitBoxSize);
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

            var energy = Owner.Get<Energy>();

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
            if (!Owner.Get<Energy>()?.Reduce(1f) ?? false) return;

            var damages = GetBaseDamages();
            var facingTile = Owner.GetFacingTile();

            if (CanAttackEntities)
            {
                var facingEntities = Owner.Level.GetEntitiesOnArea(HitBox);
                facingEntities.Sort((a, b) =>
                    {
                        return Mathf.Distance(a.X, a.Y, Owner.X, Owner.Y)
                            .CompareTo(Mathf.Distance(b.X, b.Y, Owner.X, Owner.Y));
                    });
                
                foreach (var e in facingEntities)
                    if (e != Owner)
                    {
                        if (e.Has<Breakable>())
                        {
                            e.Get<Breakable>()?.Break(weapon);
                            IsAttacking = true;
                        }

                        var eHealth = e.Get<Health>();
                        if (!eHealth?.Invicible ?? false)
                        {
                            eHealth.Hurt(Owner, damages * (weapon?.GetAttackBonus(e) ?? 1f), Owner.Facing);
                            IsAttacking = true;
                            break;
                        }
                    }
            }

            if (CanAttackTile && !IsAttacking)
            {
                var tile = Owner.Level.GetTile(facingTile);
                if (tile.HasTag<Tags.Damage>())
                {
                    tile.Tag<Tags.Damage>().Hurt(damages * (weapon?.GetAttackBonus(tile) ?? 1f), facingTile, Owner.Level);   
                }

                if (tile.HasTag<Tags.Breakable>())
                {
                    tile.Tag<Tags.Breakable>().Break(facingTile, Owner.Level);
                }

                IsAttacking = true;
            }

            _lastDirection = Owner.Facing;
            _timer = _speedFactor;
        }
    }
}