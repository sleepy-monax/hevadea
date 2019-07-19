using Hevadea.Systems;
using Hevadea.Systems.CircleMenuSystem;
using Hevadea.Systems.ElementalSystem;
using Hevadea.Systems.HealthSystem;
using Hevadea.Systems.MinimapSystem;
using Hevadea.Systems.PlayerSystem;
using System.Collections.Generic;
using Hevadea.Systems.LightingSystem;
using Hevadea.Systems.RenderingSystem;

namespace Hevadea.Registry
{
    public static class SYSTEMS
    {
        public static List<EntitySystem> Systems { get; } = new List<EntitySystem>();
        public static List<EntityUpdateSystem> UpdateSystems { get; } = new List<EntityUpdateSystem>();
        public static List<EntityDrawSystem> DrawSystems { get; } = new List<EntityDrawSystem>();

        public static void Register<T>() where T : EntitySystem, new()
        {
            var system = new T();

            if (system is EntityUpdateSystem updateSystem) UpdateSystems.Add(updateSystem);
            if (system is EntityDrawSystem drawSystem) DrawSystems.Add(drawSystem);
        }

        public static void Initialize()
        {
            Register<HealthBarRenderer>();
            Register<PlayerInputProcessor>();

            Register<CircleMenuRenderer>();
            Register<CircleMenuProcessor>();

            Register<RevealProcessor>();

            // Fire
            Register<FireProcessor>();
            Register<FireParticles>();

            // Light and shadow
            Register<LightSystem>();
            Register<ShadowSystem>();

            Register<SpriteRenderSystem>();
        }
    }
}