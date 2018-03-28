using System;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Utils;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes
{
    class TestScene : Scene
    {
        public BspTree tree;
        private SpriteBatch sb;
        public override void Load()
        {
            tree = BspTree.BuildBspTree(0, 0, 400, 400, 4, new Random());
            sb = Rise.Graphic.CreateSpriteBatch();
        }

        public override void OnDraw(GameTime gameTime)
        {
            sb.Begin();
            Drawt(tree.Root);
            sb.End();
        }

        void Drawt(BspTreeNode node)
        {
            if (node.HasChildrens)
            {
                sb.DrawRectangle(node.Item0.ToRectangle(), Color.Blue);
                sb.DrawRectangle(node.Item1.ToRectangle(), Color.Blue);
                sb.DrawLine(node.Item0.GetCenter().ToVector2(), node.Item1.GetCenter().ToVector2(), Color.Red);

                
                Drawt(node.Item0);
                Drawt(node.Item1);
            }
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Rise.Input.KeyDown(Keys.A))
            {
                tree = BspTree.BuildBspTree(0, 0, 400, 400, 3, new Random());
            }    
        }

        public override void Unload()
        {

        }
    }
}
