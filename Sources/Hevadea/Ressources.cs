using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea
{
    public static class Ressources
    {
        public static SpriteFont FontAlagard;
        public static SpriteFont FontAlagardBig;
        public static SpriteFont FontRomulus;

        public static Texture2D ImgIcons;
        public static Texture2D ImgTiles;
        public static Texture2D ImgItems;
        public static Texture2D ImgEntities;
        public static Texture2D ImgCreatures;
        public static Texture2D ImgGui;

        public static Texture2D ImgRock;

        public static Texture2D ImgLight;
        public static Texture2D ImgShadow;
        public static Texture2D ImgSwing;

        public static Texture2D ImgMakerLogo;
        public static Texture2D ImgEngineLogo;
        public static Texture2D ImgHevadeaLogo;

        public static Texture2D ImgForestBackground;
        public static Texture2D ImgForestLight;
        public static Texture2D ImgForestTrees0;
        public static Texture2D ImgForestTrees1;

        public static SpriteSheet TileTiles;
        public static SpriteSheet TileIcons;
        public static SpriteSheet TileItems;
        public static SpriteSheet TileEntities;
        public static SpriteSheet TileCreatures;
        public static SpriteSheet TileGui;
        public static SpriteSheet TileRock;

        public static ParalaxeBackground ParalaxeForest;

        public static Sprite SprPickup;
        public static Sprite SprUnderWater;
        public static SpriteFont FontHack;

        public static void Load()
        {
            FontAlagard = Rise.Ressource.GetSpriteFont("alagard");
            FontAlagardBig = Rise.Ressource.GetSpriteFont("alagard_big");
            FontRomulus = Rise.Ressource.GetSpriteFont("romulus");
            FontHack = Rise.Ressource.GetSpriteFont("hack");

            ImgTiles = Rise.Ressource.GetImage("tiles");
            ImgItems = Rise.Ressource.GetImage("items");
            ImgIcons = Rise.Ressource.GetImage("icons");
            ImgEntities = Rise.Ressource.GetImage("entities");
            ImgCreatures = Rise.Ressource.GetImage("creatures");
            ImgGui = Rise.Ressource.GetImage("gui");

            ImgRock = Rise.Ressource.GetImage("rock");

            ImgLight = Rise.Ressource.GetImage("light");
            ImgShadow = Rise.Ressource.GetImage("shadow");
            ImgSwing = Rise.Ressource.GetImage("swing");

            ImgMakerLogo = Rise.Ressource.GetImage("logo/maker");
            ImgEngineLogo = Rise.Ressource.GetImage("logo/engine");
            ImgHevadeaLogo = Rise.Ressource.GetImage("logo/hevadea");

            ImgForestBackground = Rise.Ressource.GetImage("background/forest");
            ImgForestLight = Rise.Ressource.GetImage("background/forest_light");
            ImgForestTrees0 = Rise.Ressource.GetImage("background/forest_trees0");
            ImgForestTrees1 = Rise.Ressource.GetImage("background/forest_trees1");

            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileGui = new SpriteSheet(ImgGui, new Point(32, 32));

            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));

            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(ImgForestBackground, 0.5f),
                new ParalaxeLayer(ImgForestTrees0, 1f),
                new ParalaxeLayer(ImgForestLight, 2f),
                new ParalaxeLayer(ImgForestTrees1, 4f)
            );

            SprUnderWater = new Sprite(Ressources.TileCreatures, 3, new Point(16, 32));
            SprPickup = new Sprite(Ressources.TileCreatures, 1, new Point(16, 32));
        }
    }
}