using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.States;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class XpOrb : Entity
    {
        public int Value { get; private set; }

        public XpOrb(int value)
        {
            Value = value;
            AddComponent(new Move());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(X - 1 * Value, Y - 1 * Value, 2 * Value, 2 * Value, Color.Cyan);
            spriteBatch.FillRectangle(X - 2 * Value, Y - 2 * Value, 4 * Value, 4 * Value, Color.White * 0.1f);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            foreach (var e in Level.QueryEntity(Position, 16))
            {
                if (e != this && e.HasComponent<Experience>())
                {
                    var exp = e.GetComponent<Experience>();
                    GetComponent<Move>().MoveTo(e.X, e.Y);

                    if (Mathf.Distance(X, Y, e.X, e.Y) < 2f)
                    {
                        exp.TakeXP(this);
                        Remove();
                        break;
                    }
                }
            }
        }
    }
}