using Hevadea.Framework;
using Hevadea.Framework.Extension;
using System;

namespace Hevadea.Entities.Components
{
    public class ComponentDropExperience : EntityComponent
    {
        public int OrbCount { get; set; }

        public ComponentDropExperience(int orbCount)
        {
            OrbCount = orbCount;
        }

        public void Drop()
        {
            var AmountToDrop = OrbCount;

            for (var i = 0; i < OrbCount; i++)
            {
                var xpOrb = Registry.ENTITIES.XPORB.Construct();
                Owner.Level.AddEntity(xpOrb);
                xpOrb.Position = Owner.Position + Rise.Rnd.NextVector2(-16, 16);
            }
        }
    }
}
