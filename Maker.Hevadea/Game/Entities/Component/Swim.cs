using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Swim : EntityComponent, IUpdatableComponent
    {
        public bool IsSwiming         { get; set; } = false;
        public bool IsSwimingPainfull { get; set; } = true;
        
        public void Update(GameTime gameTime)
        {
            var health   = Owner.Components.Get<Health>();
            var energy   = Owner.Components.Get<Energy>();
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
        }
    }
}