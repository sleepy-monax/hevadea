using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components
{
    class Grow : EntityComponent, IEntityComponentUpdatable
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
