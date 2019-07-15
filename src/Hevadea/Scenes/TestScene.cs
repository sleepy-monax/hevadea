using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Scening;
using Hevadea.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Hevadea.Scenes
{
    internal class TestScene : Scene
    {
        public Maze maze;

        private SpriteBatch sb;
        private int CellSize = 56;
        private int mazeSize = 8;

        public override void Load()
        {
            maze = new Maze(mazeSize, mazeSize, new Random());
            sb = Rise.Graphic.CreateSpriteBatch();
        }

        public override void OnDraw(GameTime gameTime)
        {
            Rise.Graphic.Clear(Color.White);
            sb.Begin();

            for (var x = 0; x < mazeSize; x++)
                for (var y = 0; y < mazeSize; y++)
                {
                    sb.FillRectangle(new Rectangle(x * CellSize + 4, y * CellSize + 4, CellSize - 8, CellSize - 8), Color.Black);
                }

            foreach (var d in maze.Doors)
            {
                if (d != null)
                    sb.DrawLine(d.Cell0.X * CellSize + CellSize / 2, d.Cell0.Y * CellSize + CellSize / 2,
                                d.Cell1.X * CellSize + CellSize / 2, d.Cell1.Y * CellSize + CellSize / 2, Color.Black, 16f);
            }

            sb.End();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Rise.Input.KeyDown(Keys.A))
            {
                maze = new Maze(mazeSize, mazeSize, new Random());
            }
        }

        public override void Unload()
        {
        }
    }
}