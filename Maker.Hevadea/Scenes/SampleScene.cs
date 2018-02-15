using System;
using System.Collections.Generic;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Scenes
{
    public class SampleScene : Scene
    {
        public class Particle
        {
            private Color _color;
            private float _x, _y;
            private float _ax, _ay;
            private float _size = 10f;
            private float _life = 5f;

            public Particle()
            {
                
                Reset();
            }

            void Reset()
            {
                _color = Color.SkyBlue ;

                _x =  (float)(Engine.Random.NextDouble()) * Engine.Graphic.GetWidth();
                _y = (float)(Engine.Random.NextDouble()) * Engine.Graphic.GetHeight();
                _size = 2;
                _life = 500f;
            }
            
            public void Update(GameTime gt)
            {
                _x += _ax * (float)gt.ElapsedGameTime.TotalSeconds;
                _y += _ay * (float)gt.ElapsedGameTime.TotalSeconds;

                if (Engine.Input.KeyDown(Keys.Space))
                {   
                    var dir = new Vector2(_x - Engine.Input.MousePosition.X, _y - Engine.Input.MousePosition.Y);

                    var dist = dir.Length();
                    dir.Normalize();
                    
                    _ax -= dir.X * 2;
                    _ay -= dir.Y * 2;
    
                    _ax = Math.Max(Math.Min(_ax, 500), -500);
                    _ay = Math.Max(Math.Min(_ay, 500), -500);
                }

                _ax *= 0.999f;
                _ay *= 0.999f;
                
                _life -= (float) gt.ElapsedGameTime.TotalSeconds;

                if (_life < 0f)
                {
                    Reset();
                }
            }

            public void Draw(SpriteBatch sb)
            {
                sb.PutPixel(new Vector2(_x, _y), _color, _size);
            }
        }

        private List<Particle> _particles = new List<Particle>();
        private SpriteBatch _sb;
        
        
        public override void Load()
        {
            _sb = Engine.Graphic.CreateSpriteBatch();
            
            for (int i = 0; i < 25000; i++)
            {
                _particles.Add(new Particle());
                _counter++;
                
            }
        }

        private int _counter = 0;
        
        public override void OnUpdate(GameTime gameTime)
        {
            foreach (var p in _particles)
            {
                p.Update(gameTime);
            }
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp);
            foreach (var p in _particles)
            {
                p.Draw(_sb);
            }
            _sb.End();
        }

        public override string GetDebugInfo()
        {
            return _counter.ToString();
        }

        public override void Unload()
        {
        }
    }
}