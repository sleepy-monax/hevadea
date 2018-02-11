using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Component.Render;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Creatures
{
    public class NpcEntity : Entity
    {
        public NpcEntity()
        {
            Components.Adds
            (
                new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))),
                new Interact(),
                new Attack(),
                new Target()
            );
        }
    }
}