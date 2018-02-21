using System;
using System.Collections.Generic;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes.Toys
{

    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float R { get; set; }
        
        public float VX { get; set; } = 0;
        public float VY { get; set; } = 0;
        
        public float AX { get; set; } = 0;
        public float AY { get; set; } = 0;

        public Ball(float r, float x, float y)
        {
            R = r;
            X = x;
            Y = y;
        }
    }
    
    public class PhysicTest : Scene
    {
        private SpriteBatch _sb;
        private List<Ball> _balls = new List<Ball>();
        private Ball bs = new Ball(20, 0, 0);
        
        public override void Load()
        {
            _sb = Engine.Graphic.CreateSpriteBatch();
            for (int i = 0; i < 300; i++)
            {
                _balls.Add(new Ball(Engine.Random.Next(5, 35), Engine.Random.Next(Engine.Graphic.GetWidth()), Engine.Random.Next(Engine.Graphic.GetHeight())));
            }
            _balls.Add(bs);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            foreach (var ba in _balls)
            {
                ba.Y += 0.5f;
                foreach (var bb in _balls)
                {
                    if (ba == bb) continue;
                    
                    var dist = Mathf.Distance(ba.X, ba.Y, bb.X, bb.Y);

                    if (Mathf.Abs(dist) < bb.R + ba.R)
                    {
                        var dir = new Vector2(ba.X - bb.X, ba.Y - bb.Y);
                        dir.Normalize();

                        ba.X += dir.X * 2;
                        ba.Y += dir.Y * 2;
                    }
                }

                ba.X = Mathf.Clamp(ba.X, 0, Engine.Graphic.GetWidth());
                ba.Y = Mathf.Clamp(ba.Y, 0, Engine.Graphic.GetHeight());
            }

            var pos = Engine.Input.MousePosition;

            bs.X = pos.X;
            bs.Y = pos.Y;
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin();
            foreach (var b in _balls)
            {
                _sb.DrawCircle(b.X, b.Y, b.R, 8, Color.White);
            }
            _sb.End();
        }

        public override void Unload()
        {
            
        }
    }
}