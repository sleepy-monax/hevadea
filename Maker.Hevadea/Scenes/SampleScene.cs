using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Rise.Graphic.Particles;
using Maker.Rise.UI.Widgets;
using Maker.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Scenes
{
    public class SampleScene : Scene
    {
        private SpriteBatch _sb;
        private ParticleSystem _ps;

        public override void Load()
        {
            _sb = Engine.Graphic.CreateSpriteBatch();
            _ps = new ParticleSystem();
            Container = new BlurPanel() { Color = Color.Transparent};
        }
        public override void OnUpdate(GameTime gameTime)
        {
            var mp = Engine.Input.MousePosition;
            var sc = Engine.Graphic.GetCenter();

            if (Engine.Input.MouseLeft)
            {
                for (int i = 0; i < 100; i++)
                _ps.EmiteAtAngle(new Particle { IsAffectedByGravity = true, Size = 10 },mp.X, mp.Y, (float)(Engine.Random.NextDouble()) * Mathf.TAU, (float)(Engine.Random.NextDouble() + 1f) * 25);
            }

            if (Engine.Input.MouseRight)
            {
                for (int i = 0; i < 100; i++)
                    _ps.EmiteAtAngle(new ColoredParticle { Color = new Color(Engine.Random.Next(255), Engine.Random.Next(255), Engine.Random.Next(255)), Size = 25 * (float)Engine.Random.NextDouble()}, mp.X, mp.Y, (float)(Engine.Random.NextDouble()) * Mathf.TAU, (float)(Engine.Random.NextDouble() + 1f) * 25);
            }



            _ps.Update(gameTime);
        }

        public override void OnDraw(GameTime gameTime)
        {
            _sb.BeginDrawEnd(_ps.Draw, gameTime);
        }

        public override string GetDebugInfo()
        {
            return $"Particle count: {_ps.Count()}";
        }

        public override void Unload()
        {
        }
    }
}