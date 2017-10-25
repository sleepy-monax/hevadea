using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;

namespace WorldOfImagination.Framework
{
    public class Host
    {
        Game HostedGame;
        GameWindow GameWindow;

        public Host(Game hostedGame, int windowWidth = 800, int windowHeight = 600)
        {
            HostedGame = hostedGame;
            HostedGame.Host = this;
            GameWindow = new GameWindow(windowWidth, windowHeight);
            GameWindow.RenderFrame += DrawHandle;
            GameWindow.UpdateFrame += UpdateHandle;
            GameWindow.Load += LoadHandle;
            GameWindow.Closing += ExitHandle;
            GameWindow.Resize += ResizeHandle;
        }
        private void LoadHandle(object sender, EventArgs e)
        { HostedGame.OnLoad(); }
        private void DrawHandle(object sender, FrameEventArgs e)
        { HostedGame.OnDraw(); GameWindow.SwapBuffers(); }
        private void UpdateHandle(object sender, FrameEventArgs e)
        { HostedGame.OnUpdate(e.Time); }
        private void ExitHandle(object sender, EventArgs e)
        { HostedGame.OnExit(); }
        private void ResizeHandle(object sender, EventArgs e)
        { GL.Viewport(0, 0, GameWindow.Width, GameWindow.Height); }
        public void Run()
        {
            GameWindow.Run();
        }

        public void Clear(Color4 clearColor)
        {
            GL.ClearColor(clearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit  | ClearBufferMask.DepthBufferBit);
        }
    }
}