using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Entities.Components
{
    public class Grow : EntityComponent, IEntityComponentUpdatable
    {
        private float _currentAge;
        private int _nbStages;
        private float _timePerStage;

        public Grow(int nbState, int timePerStage)
        {
            _currentAge = 0;
            _nbStages = nbState;
            _timePerStage = timePerStage;
        }

        public void Update(GameTime gameTime)
        {
            _currentAge += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public int GetState()
        {
            var age = Math.Min(_currentAge, _nbStages * _timePerStage);
            return (int)(age / _timePerStage);
        }
    }
}