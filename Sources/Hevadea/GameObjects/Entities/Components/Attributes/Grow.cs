using System;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    class Grow : Component
    {
        private float _currentAge;
        private int _nbStages;
        private float _timePerStage;
        public Grow(int nb_state, int time_per_stage)
        {
            _currentAge = 0;
            _nbStages = nb_state;
            _timePerStage = time_per_stage;
        }
        public void Update(GameTime gameTime)
        {
            _currentAge += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public int GetState()
        {
            var age = Math.Min(_currentAge, _nbStages * _timePerStage);
            return (int)(age/_timePerStage);
        }
    }
}
