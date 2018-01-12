using Maker.Hevadea.Game.Items;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Registry
{
    public static class ITEMS
    {
        public static Item[] ById = new Item[256];

        public static RessourceItem WOOD_LOG;
        public static RessourceItem WOOD_PLANK;
        public static RessourceItem WOOD_STICK;
        public static RessourceItem PINE_CONE;

        public static void Initialize()
        {
            WOOD_LOG = new RessourceItem(0, "Wood Log", new Sprite(Ressources.tile_items, 6));
            WOOD_PLANK = new RessourceItem(1, "Wood Plank", new Sprite(Ressources.tile_items, new Point(6,1)));
            WOOD_STICK = new RessourceItem(2, "Wood Stick", new Sprite(Ressources.tile_items, 5));
            PINE_CONE = new RessourceItem(3, "Pine Cone", new Sprite(Ressources.tile_items, new Point(5,2)));
        }
    }
}
