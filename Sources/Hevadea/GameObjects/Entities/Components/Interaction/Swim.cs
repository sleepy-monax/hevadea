using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.Interaction
{
    public class Swim : Component
    {
        public bool IsSwiming   { get; set; } = false;
        public bool WasSwiming { get; set; } = false;
        public bool IsSwimingPainfull { get; set; } = true;
        
        public void Update(GameTime gameTime)
        {
            var health   = Entity.GetComponent<Health>();
            var energy   = Entity.GetComponent<Energy>();
            var position = Entity.GetTilePosition();
            
            if (Entity.Level.IsAll<Tags.Liquide>(new Rectangle((int)Entity.X - 4, (int)Entity.Y - 4, 8,8)))
            {
                IsSwiming = true;
                if (energy != null && IsSwimingPainfull)
                {
                    energy.EnableNaturalRegeneration = false;
                    if (health != null)
                    {
                        energy.Reduce(0.01f);
                        if (energy.Value < 0.01f){health.Hurt(Entity.GetTileOnMyPosition(), 0.01f, position.X, position.Y );}
                    }
                }
            }
            else
            {
                IsSwiming = false;
                if (energy!=null) energy.EnableNaturalRegeneration = true;
            }

            if (!WasSwiming && IsSwiming)
            {
                for (int i = 0; i < 10; i++)
                {
                    Entity.Level.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.Azure, Life = 0.5f, FadeOut = 0.15f}, Entity.X, Entity.Y, (float)(Rise.Rnd.NextDouble() - 0.5f) * 64f, (float)(Rise.Rnd.NextDouble() - 0.75f) * 20f);
                    Entity.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.LightBlue, Life = 0.5f, FadeOut = 0.15f}, Entity.X , Entity.Y, (float)(Rise.Rnd.NextDouble() - 0.5f) * 64f, (float)(Rise.Rnd.NextDouble() - 0.75f) * 20f);
                }
            }
            
            WasSwiming = IsSwiming;
        }
    }
}