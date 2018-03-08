using Hevadea.Framework;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Interaction
{
    public class Swim : EntityComponent, IEntityComponentUpdatable
    {
        public bool IsSwiming   { get; set; } = false;
        public bool WasSwiming { get; set; } = false;
        public bool IsSwimingPainfull { get; set; } = true;
        
        public void Update(GameTime gameTime)
        {
            var health   = Owner.Get<Health>();
            var energy   = Owner.Get<Energy>();
            var position = Owner.GetTilePosition(true);
            
            if (Owner.Level.IsAll<Tags.Liquide>(Owner.Bound))
            {
                IsSwiming = true;
                if (energy != null && IsSwimingPainfull)
                {
                    energy.EnableNaturalRegeneration = false;
                    if (health != null)
                    {
                        energy.Reduce(0.01f);
                        if (energy.Value < 0.01f){health.Hurt(Owner.GetTileOnMyOrigin(), 0.01f, position.X, position.Y );}
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
                    Owner.Level.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.Azure, Life = 0.5f, FadeOut = 0.15f}, Owner.X + Owner.Origin.X, Owner.Y + Owner.Height, (float)(Rise.Random.NextDouble() - 0.5f) * 64f, (float)(Rise.Random.NextDouble() - 0.75f) * 20f);
                    Owner.ParticleSystem.EmiteAt(new ColoredParticle{ Color = Color.LightBlue, Life = 0.5f, FadeOut = 0.15f}, Owner.X + Owner.Origin.X, Owner.Y + Owner.Height, (float)(Rise.Random.NextDouble() - 0.5f) * 64f, (float)(Rise.Random.NextDouble() - 0.75f) * 20f);
                }
            }
            
            WasSwiming = IsSwiming;
        }
    }
}