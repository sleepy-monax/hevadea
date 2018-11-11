using Hevadea.Systems;
using Hevadea.Systems.ElementalSystem;
using Hevadea.Systems.PhysicSystem;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class SYSTEMS
    {
        public static List<GameSystem> Systems = new List<GameSystem>();

        public static void Initialize()
        {
            // Fire
            Systems.Add(new FireProcessor());
            Systems.Add(new FireParticles());

            // Light and shadow
            Systems.Add(new LightSystem());
            Systems.Add(new ShadowSystem());

            // Physic
            Systems.Add(new PhysicProcessor());
            Systems.Add(new PhysicRenderer());

            Systems.Add(new SpriteRenderSystem());

            Systems.Add(new SwimmingEffect());
        }
    }
}