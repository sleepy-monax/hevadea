using System;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Blueprints.Legacy
{
    public class EntityXpOrb : Entity
    {
        public int Value { get; private set; }
        
        public EntityXpOrb(int value)
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
            Level.GetEntitiesOnRadius(X, Y, 16);

            foreach (var e in Level.GetEntitiesOnRadius(X, Y, 16))
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