using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.Rise.Components
{
    public class DebugManager : GameComponent
    {
        
        SpriteBatch sb;
        public bool Visible = false;
        private Queue<int> renderTime;
        private Queue<int> updateTime;

        int MaxSample = 256;

        public DebugManager(RiseGame game) : base(game)
        {
            renderTime = new Queue<int>();
            updateTime = new Queue<int>();
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
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
                foreach (var i in renderTime)
                {
                    avrRenderTime += i;
                    sb.FillRectangle(new Rectangle(index, Engine.Graphic.GetHeight() - i * 5, 1, i * 5), i > 10 ? Color.Red : Color.Green);
                    index++;
                }

                sb.DrawString(EngineRessources.font_arial_tiny, "Render", new Vector2(16, Engine.Graphic.GetHeight() - 256),  Color.White);
                index = 0;
                foreach (var i in updateTime)
                {

                    sb.FillRectangle(new Rectangle(index + 264, Engine.Graphic.GetHeight() - i * 5, 1, i * 5), i > 10 ? Color.Magenta : Color.Blue);
                    index++;
                }
                
                sb.DrawString(EngineRessources.font_hack, 
$@"
Render: {Game.DrawTime, 3}ms (avr {avrRenderTime/renderTime.Count}ms)
Tick: {Game.UpdateTime}ms
Scene: {Engine.Scene.CurrentScene.GetType().FullName}
DisplayMode: {Game.GraphicsDevice.Adapter.CurrentDisplayMode}
Triangle: {Game.GraphicsDevice.Metrics.PrimitiveCount}", new Vector2(32f,16f), Color.White);



                sb.End();
            }
        }

        public void DrawUiGraph(SpriteBatch sb, Control c, int x, ref int y)
        {
            sb.DrawString(EngineRessources.font_hack, c.GetType().Name, new Vector2(x, y), Color.Black);

            foreach (var child in c.Childs)
            {
                y += 16;
                DrawUiGraph(sb, child, x + 16, ref y);
            }
        }
    }
}