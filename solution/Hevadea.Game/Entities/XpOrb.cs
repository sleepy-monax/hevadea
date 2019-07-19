using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class XpOrb : Entity
    {
        public int Value { get; private set; } = 1;

        public XpOrb()
        {
            AddComponent(new ComponentMove());

            Value = Rise.Rnd.Next(1, 16);
        }


        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var size = 1;
            spriteBatch.FillRectangle(X - 2 * size, Y - 2 * size, 4 * size, 4 * size, Color.Yellow * 0.5f);
            spriteBatch.FillRectangle(X - 1 * size, Y - 1 * size, 2 * size, 2 * size, Color.Yellow);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            foreach (var e in Level.QueryEntity(Position, 16))
            {
                if (e != this && e.HasComponent<ComponentExperience>())
                {
                    var exp = e.GetComponent<ComponentExperience>();
                    GetComponent<ComponentMove>().MoveTo(e.X, e.Y);

                    if (Mathf.Distance(X, Y, e.X, e.Y) < 2f)
                    {
                        exp.TakeXP(this);
                        Remove();
                        break;
                    }
                }
            }

            foreach (var e in Level.QueryEntity(Position, Game.Unit))
            {
                if (e.HasComponent<ComponentExperience>(out var i))
                {
                    GetComponent<ComponentMove>().MoveTo(e.X, e.Y, 2f);

                    if (Mathf.Distance(e.X, e.Y, X, Y) < 3)
                        e.GetComponent<ComponentExperience>().TakeXP(Value);
                }
            }
        }
    }
}