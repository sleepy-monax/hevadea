using Hevadea.Framework;
using Hevadea.Framework.Extension;

namespace Hevadea.Entities.Components
{
    public class ComponentFlammable : EntityComponent
    {
        public bool IsBurning { get; private set; } = false;
        public bool GotExtinguish { get; set; } = false;
        public float BurnningTimer { get; set; } = 0f;

        public void SetInFire()
        {
            if (!IsBurning)
            {
                IsBurning = true;
                BurnningTimer = Rise.Rnd.NextFloat(7) + 3f;
            }
        }

        public void Extinguish()
        {
            if (IsBurning)
            {
                IsBurning = false;
                GotExtinguish = true;
                BurnningTimer = 0f;
            }
        }
    }
}