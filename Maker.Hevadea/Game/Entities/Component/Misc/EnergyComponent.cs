using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class EnergyComponent : EntityComponent, IUpdatableComponent
    {

        public float Energy = 10f;
        public float MaxEnergy = 10f;
        public float regeneration = 0.01f;
        public float MaxRegenerationSpeed = 1f;

        public bool Reduce(float value)
        {
            if (Energy >= value)
            {
                Energy -= value;
                regeneration = 0.01f;
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            Energy = Math.Min(MaxEnergy, Energy + regeneration);
            regeneration = Math.Min(MaxRegenerationSpeed, regeneration * 1.02f);
        }
    }
}
