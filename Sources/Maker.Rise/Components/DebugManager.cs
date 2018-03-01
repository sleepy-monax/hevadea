using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Hevadea.Framework.Graphic;

namespace Maker.Rise.Components
{
    public class DebugManager : GameComponent
    {
        SpriteBatch sb;
        public bool Visible = false;
        private Queue<int> renderTime;
        private Queue<int> updateTime;

        int MaxSample = 256;

        public DebugManager(InternalGame game) : base(game)
        {
            renderTime = new Queue<int>();
            updateTime = new Queue<int>();
        }

        public override void Initialize()
        {
            sb = Engine.Graphic.CreateSpriteBatch();
        }

        public override void Update(GameTime gameTime)
        {
            renderTime.Enqueue(Game.DrawTime);
            updateTime.Enqueue(Game.UpdateTime);

            if (renderTime.Count > MaxSample)
            {
                renderTime.Dequeue();
                updateTime.Dequeue();
            }

            if (Engine.Input.KeyPress(Keys.F3))
            {
                Visible = !Visible;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                sb.Begin();
                int avrRenderTime = 0;
                var index = 0;
                var update = updateTime.ToArray();
                
                var dp = new Point();
                var up = new Point();
                
                sb.FillRectangle(0, Engine.Graphic.GetHeight() - MaxSample / 2, MaxSample, MaxSample / 2, Color.White * 0.5f);
                
                foreach (var i in renderTime)
                {
                    avrRenderTime += i;
                    sb.DrawLine(dp.X, dp.Y, index, Engine.Graphic.GetHeight() - i * 5, Color.Green);
                    dp = new Point(index, Engine.Graphic.GetHeight() - i * 5);
                    
                    sb.DrawLine(up.X, up.Y, index, Engine.Graphic.GetHeight() - update[index] * 5, Color.Blue);
                    up = new Point(index, Engine.Graphic.GetHeight() - update[index] * 5);
                    index++;
                }

                
                sb.FillRectangle(0, Engine.Graphic.GetHeight() - (avrRenderTime / renderTime.Count * 5), MaxSample, avrRenderTime / renderTime.Count * 5, Color.Gold * 0.5f);
                
                var debugText = $@"
d: {Game.DrawTime,3}ms (avr {avrRenderTime / renderTime.Count, 3}ms)
u: {Game.UpdateTime,3}ms
s: {Engine.Graphic.GetWidth()} {Engine.Graphic.GetHeight()}
{Engine.Scene.CurrentScene?.GetDebugInfo() ?? "null"}";

                var debugTextSize = EngineRessources.FontHack.MeasureString(debugText);

                sb.DrawString(EngineRessources.FontHack, debugText, new Vector2(16, 16), Color.White);
                sb.FillRectangle(new Rectangle(Engine.Input.MousePosition, new Point(16,16)), Color.Red);
                sb.End();
            }
        }
    }
}