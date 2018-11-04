﻿using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components.Ai.Actions;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Registry;
using Hevadea.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Entities.Components.Ai.Behaviors
{
    public class BehaviorEnemy : BehaviorAnimal
    {
        public float AgroRange { get; set; } = 5f;
        public float FollowRange { get; set; } = 7f;
        public float MoveSpeedAgro { get; set; } = 0.5f;
        public BlueprintGroupe<EntityBlueprint> Targets { get; set; } = new BlueprintGroupe<EntityBlueprint>("Targets") { Members = { ENTITIES.PLAYER } };

        public Entity Target { get; private set; } = null;
        private Coordinates _lastTagetPosition = null;

        private List<Entity> _targetsOnSight;

        public override void IaAborted(AgentAbortReason why)
        {
            base.Update(null);
        }

        private bool CheckLineOfSight(Coordinates to)
        {
            bool result = true;
            LoopUtils.Line(Agent.Owner.Coordinates.ToPoint(), to.ToPoint(), (p) =>
            {
                result &= !Agent.Owner.Level.GetTile(p.X, p.Y).BlockLineOfSight;
            });
            return result;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Agent.IsBusy() && Target != null && (Target.Level != Agent.Owner.Level ||
                Mathf.Distance(Agent.Owner.Position2D, Target.Position2D) > FollowRange * Game.Unit))
            {
                Agent.Abort(AgentAbortReason.TagetLost);
                Target = null;
            }

            if (Target == null)
            {
                _targetsOnSight = Agent.Owner.Level.GetEntitiesOnArea(Agent.Owner.Position2D, AgroRange * Game.Unit)
                                        .Where((e) => e.MemberOf(Targets) && CheckLineOfSight(e.Coordinates)).ToList();

                _targetsOnSight.Sort((a, b) => Mathf.Distance(a.Position2D, Agent.Owner.Position2D)
                     .CompareTo(Mathf.Distance(b.Position2D, Agent.Owner.Position2D)));

                if (_targetsOnSight.Any())
                {
                    Target = _targetsOnSight.First();
                    _lastTagetPosition = Target.Coordinates;
                    Agent.MoveTo(Target.Coordinates, MoveSpeedAgro, true, (int)(FollowRange + 4));
                }
            }
            else
            {
                if (Target.Coordinates != _lastTagetPosition &&
                    Mathf.Distance(Agent.Owner.Position2D, Target.Position2D) < FollowRange * Game.Unit &&
                    CheckLineOfSight(Target.Coordinates))
                {
                    Agent.Flush();
                    _lastTagetPosition = Target.Coordinates;
                    Agent.MoveTo(_lastTagetPosition, MoveSpeedAgro, true, (int)(FollowRange + 4));
                }
            }

            if (!Agent.IsBusy())
                base.Update(gameTime);
        }

        public override void DrawDebug(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Target == null)
            {
                spriteBatch.DrawCircle(Agent.Owner.Position2D, AgroRange * Game.Unit, 24, Color.White * 0.5f);
            }
            else
            {
                spriteBatch.DrawCircle(Agent.Owner.Position2D, FollowRange * Game.Unit, 24, Color.Red);
            }

            foreach (var t in _targetsOnSight)
            {
                spriteBatch.DrawLine(t.Position2D, Agent.Owner.Position2D, CheckLineOfSight(t.Coordinates) ? Color.Green : Color.Yellow);
            }
        }
    }
}