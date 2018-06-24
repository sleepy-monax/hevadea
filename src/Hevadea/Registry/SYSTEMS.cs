using Hevadea.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Registry
{
    public static class SYSTEMS
    {
        public static List<GameSystem> Systems = new List<GameSystem>();

        public static void Initialize()
        {
            Systems.Add(new LightSystem());
            Systems.Add(new ShadowSystem());
            Systems.Add(new PhysicSystem());
        }
    }
}
