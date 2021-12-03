using System.Collections.Generic;
using System.Linq;
using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components.AI.Actions;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.AI.Behaviors
{
    public class BehaviorEnemy : BehaviorAnimal
    {
        public float AgroRange { get; set; } = 5f;
        public float FollowRange { get; set; } = 7f;
        public float MoveSpeedAgro { get; set; } = 0.5f;

        public Groupe<EntityBlueprint> Targets { get; set; } = new Groupe<EntityBlueprint>("Targets")
            {Members = {ENTITIES.PLAYER}};

        public Entity Target { get; private set; } = null;
        private Coordinates _lastTagetPosition = null;

        private List<Entity> _targetsOnSight;

        private bool CheckLineOfSight(Coordinates coords)
        {
            var result = true;
            Geometry.Line(Agent.Owner.Coordinates.ToPoint(), coords.ToPoint(),
                (p) => { result &= !Agent.Owner.Level.GetTile(p.X, p.Y).BlockLineOfSight; });
            return result;
        }

        public override void Update(GameTime gameTime)
        {
            if (Target != null && (Target.Removed ||
                    Target.Level != Agent.Owner.Level ||
                    Mathf.Distance(Agent.Owner.Position, Target.Position) > FollowRange * Game.Unit))
            {
                Agent.Abort(AgentAbortReason.TagetLost);
                Target = null;
            }

            if (Target == null)
            {
                _targetsOnSight = Agent.Owner.Level.QueryEntity(Agent.Owner.Position, AgroRange * Game.Unit)
                    .Where((e) => e.MemberOf(Targets) && CheckLineOfSight(e.Coordinates)).ToList();

                _targetsOnSight.Sort((a, b) => Mathf.Distance(a.Position, Agent.Owner.Position)
                    .CompareTo(Mathf.Distance(b.Position, Agent.Owner.Position)));

                if (_targetsOnSight.Any())
                {
                    Target = _targetsOnSight.First();
                    _lastTagetPosition = Target.Coordinates;
                    Agent.MoveTo(Target.Coordinates, MoveSpeedAgro, false, (int) (FollowRange + 4));
                }
            }
            else
            {
                if (Target.Coordinates != _lastTagetPosition &&
                    Mathf.Distance(Agent.Owner.Position, Target.Position) < FollowRange * Game.Unit &&
                    CheckLineOfSight(Target.Coordinates))
                {
                    Agent.Flush();
                    _lastTagetPosition = Target.Coordinates;
                    Agent.MoveTo(_lastTagetPosition, MoveSpeedAgro, false, (int) (FollowRange + 4));
                }
            }

            if (!Agent.IsBusy())
                base.Update(gameTime);
        }

        public override void DrawDebug(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Target == null)
                spriteBatch.DrawCircle(Agent.Owner.Position, AgroRange * Game.Unit, 24, Color.White * 0.5f);
            else
                spriteBatch.DrawCircle(Agent.Owner.Position, FollowRange * Game.Unit, 24, Color.Red);

            if (_targetsOnSight != null)
                foreach (var t in _targetsOnSight)
                    spriteBatch.DrawLine(t.Position, Agent.Owner.Position,
                        CheckLineOfSight(t.Coordinates) ? Color.Green : Color.Yellow);
        }
    }
}