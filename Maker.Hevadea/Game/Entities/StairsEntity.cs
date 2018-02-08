using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Storage;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities
{
    public class StairsEntity : Entity
    {
        public static int Destination;
        public static bool GoUp = true;

        public StairsEntity(bool goUp, int destination) : this()
        {
            GoUp = goUp;
            Destination = destination;
        }

        public StairsEntity()
        {
            Width = Height = 16;
            Origin = new Point(0, 0);

            var interaction = new Interactable();
            Components.Add(interaction);
            interaction.OnInteracte += (sender, arg) => { };
        }


        public override void OnSave(EntityStorage store)
        {
            store.Set(nameof(Destination), Destination);
            store.Set(nameof(GoUp), GoUp);
        }

        public override void OnLoad(EntityStorage store)
        {
            Destination = store.Get(nameof(Destination), Destination);
            GoUp = store.Get(nameof(GoUp), GoUp);
        }
    }
}