using System;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Maker.Utils.Json;

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
                foreach (var i in renderTime)
                {
                    avrRenderTime += i;
                    sb.FillRectangle(new Rectangle(index, Engine.Graphic.GetHeight() - i * 5 - update[index] * 5, 1, i * 5),
                        i > 10 ? Color.Red : Color.Green);
                    sb.FillRectangle(new Rectangle(index, Engine.Graphic.GetHeight() - update[index] * 5, 1, update[index] * 5),
                        i > 10 ? Color.Magenta : Color.Blue);
                    index++;
                }



                var debugText = $@"
Render: {Game.DrawTime,3}ms (avr {avrRenderTime / renderTime.Count}ms)
Tick: {Game.UpdateTime,3}ms
Scene: {Engine.Scene.CurrentScene.GetType().FullName}
DisplayMode: {Game.GraphicsDevice.Adapter.CurrentDisplayMode}
--- Curent Scene Debug Info ---
{Engine.Scene.CurrentScene.GetDebugInfo()}";

                var debugTextSize = EngineRessources.FontHack.MeasureString(debugText);

                sb.DrawString(EngineRessources.FontHack, debugText, new Vector2(16, 16), Color.White);

                sb.End();
            }
        }
    }
}