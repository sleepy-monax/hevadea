using Maker.Rise.GameComponent.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.Rise.GameComponent
{
    public class DebugManager : GameComponent
    {
        
        SpriteBatch sb;
        public bool Visible = false;
        private Queue<int> renderTime;
        
        public DebugManager(RiseGame game) : base(game)
        {
            renderTime = new Queue<int>();
        }

        public override void Initialize()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            renderTime.Enqueue(Game.DrawTime);
            if (renderTime.Count > Game.Graphics.GetWidth())
            {
                renderTime.Dequeue();
            }
            
            if (Game.Input.KeyPress(Keys.F3))
            {
                Visible = !Visible;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {            
                sb.Begin();
                
                var index = 0;
                foreach (var i in renderTime)
                {
                    sb.FillRectangle(new Rectangle(index, Game.Graphics.GetHeight() - i * 5, 1, i * 5), Color.Red * 0.1f * i);
                    index++;
                }
                
                sb.DrawString(Game.Ress.font_hack, $"Draw time : {Game.DrawTime}ms\nCurrent Scene: {Game.Scene.CurrentScene.GetType().FullName}", new Vector2(16f,16f), Color.White);
                var y = 64;
                DrawUiGraph(sb, Game.Scene.CurrentScene.UiRoot, 64, ref y);


                sb.End();
            }
        }

        public void DrawUiGraph(SpriteBatch sb, Control c, int x, ref int y)
        {
            sb.DrawString(Game.Ress.font_hack, c.GetType().Name, new Vector2(x, y), Color.Black);

            foreach (var child in c.Childs)
            {
                y += 16;
                DrawUiGraph(sb, child, x + 16, ref y);
            }
        }
    }
}