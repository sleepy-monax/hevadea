using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Ai.Actions;
using Hevadea.GameObjects.Tiles;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.GameObjects.Entities.Components.Ai.Behaviors
{
    public class BehaviorEnemy : BehaviorAnimal
    {
        public float AgroRange { get; set; } = 5f;
        public float FollowRange { get; set; } = 7f;
        public float ChanceToAgro { get; set; } = 0.5f;
        public float MoveSpeedAgro { get; set; } = 0.5f;

        public Entity Target { get; private set; } = null;
        private TilePosition _lastTagetPosition = null;
        

        private List<Entity> _targetsOnSight;

        public override void IaAborted(AgentAbortReason why)
        {
            base.Update(null);
        }

        public override void Update(GameTime gameTime)
        {
            if (Target != null &&
                Target.Level == Agent.Owner.Level &&
                Mathf.Distance(Agent.Owner.Position, Target.Position) > FollowRange * Game.Unit && !Agent.IsBusy())
            {
                Agent.Abort(AgentAbortReason.TagetLost);
                Target = null;
            }

            _targetsOnSight = Agent.Owner.Level.GetEntitiesOnArea(Agent.Owner.Position, AgroRange * Game.Unit)
                .Where((e) => e.Blueprint == EntityFactory.PLAYER && CheckLineOfSight(e.GetTilePosition())).ToList();

            if (Target != null && Target.GetTilePosition() != _lastTagetPosition && Mathf.Distance(Agent.Owner.Position, Target.Position) < FollowRange * Game.Unit)
            {
                if (CheckLineOfSight(Target.GetTilePosition()))
                {
                    Agent.Flush();
                    _lastTagetPosition = Target.GetTilePosition();
                    Agent.MoveTo(_lastTagetPosition, MoveSpeedAgro, true, (int)(FollowRange + 4));
                }
            }

            if (!Agent.IsBusy())
            {
                if (Target == null)
                {
                    _targetsOnSight.Sort((a, b) => Mathf.Distance(a.Position, Agent.Owner.Position)
                                         .CompareTo(Mathf.Distance(b.Position, Agent.Owner.Position)));

                    if (_targetsOnSight.Any())
                    {
                        Target = _targetsOnSight.First();
                        _lastTagetPosition = Target.GetTilePosition();
                        Agent.MoveTo(Target.GetTilePosition(), MoveSpeedAgro, true, (int)(FollowRange + 4));
                    }
                }

            }
            

            if (!Agent.IsBusy())
                base.Update(gameTime);
        }

        private bool CheckLineOfSight(TilePosition to)
        {
            bool result = true;
            LoopUtils.Line(Agent.Owner.GetTilePosition().ToPoint(), to.ToPoint(), (p) =>
            {
                result &= !Agent.Owner.Level.GetTile(p.X, p.Y).BlockLineOfSight;
            });
            return result;
        }

        public override void DrawDebug(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Target == null)
            {
                spriteBatch.DrawCircle(Agent.Owner.Position, AgroRange * Game.Unit, 24, Color.White * 0.5f);

            }
            else
            {
                spriteBatch.DrawCircle(Agent.Owner.Position, FollowRange * Game.Unit, 24, Color.Red);
            }

            foreach (var t in _targetsOnSight)
            {
                spriteBatch.DrawLine(t.Position, Agent.Owner.Position, CheckLineOfSight(t.GetTilePosition()) ? Color.Green : Color.Yellow);
            }

            

        }
    }
}