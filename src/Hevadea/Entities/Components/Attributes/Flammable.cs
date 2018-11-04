using System;
namespace Hevadea.Entities.Components.Attributes
{
    public class Flammable : EntityComponent
    {
        public bool IsOnFire { get; set; } = false;

        public void SetInFire()
        {
            IsOnFire = true;
        }
    }
}
