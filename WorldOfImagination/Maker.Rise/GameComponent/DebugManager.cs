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
                    
                    sb.FillRectangle(new Rectangle(index, Game.Graphics.GetHeight() - i * 5, 1, i * 5), Color.Red);
                    index++;
                }

                index = 0;
                foreach (var i in updateTime)
                {

                    sb.FillRectangle(new Rectangle(index + 256, Game.Graphics.GetHeight() - i * 5, 1, i * 5), Color.Blue);
                    index++;
                }


                sb.DrawString(Game.Ress.font_hack, 
$@"
Render: {Game.DrawTime}ms
Tick: {Game.UpdateTime}ms
Scene: {Game.Scene.CurrentScene.GetType().FullName}", new Vector2(32f,16f), Color.White);



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