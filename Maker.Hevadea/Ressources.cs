using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea
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

        public static Sprite SprUnderWater;

        public static void Load()
        {
            FontAlagard = Engine.Ressource.GetSpriteFont("alagard");
            FontAlagardBig = Engine.Ressource.GetSpriteFont("alagard_big");
            FontRomulus = Engine.Ressource.GetSpriteFont("romulus");

            ImgTiles = Engine.Ressource.GetImage("tiles");
            ImgItems = Engine.Ressource.GetImage("items");
            ImgIcons = Engine.Ressource.GetImage("icons");
            ImgEntities = Engine.Ressource.GetImage("entities");
            ImgCreatures = Engine.Ressource.GetImage("creatures");
            ImgGui = Engine.Ressource.GetImage("gui");

            ImgRock = Engine.Ressource.GetImage("rock");

            ImgLight = Engine.Ressource.GetImage("light");
            ImgShadow = Engine.Ressource.GetImage("shadow");
            ImgSwing = Engine.Ressource.GetImage("swing");

            ImgMakerLogo = Engine.Ressource.GetImage("logo/maker");
            ImgEngineLogo = Engine.Ressource.GetImage("logo/engine");
            ImgHevadeaLogo = Engine.Ressource.GetImage("logo/hevadea");

            ImgForestBackground = Engine.Ressource.GetImage("background/forest");
            ImgForestLight = Engine.Ressource.GetImage("background/forest_light");
            ImgForestTrees0 = Engine.Ressource.GetImage("background/forest_trees0");
            ImgForestTrees1 = Engine.Ressource.GetImage("background/forest_trees1");

            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileGui = new SpriteSheet(ImgGui, new Point(32, 32));

            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));

            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(ImgForestBackground, 1.1f),
                new ParalaxeLayer(ImgForestTrees0, 1.5f),
                new ParalaxeLayer(ImgForestLight, 2f),
                new ParalaxeLayer(ImgForestTrees1, 2.5f)
            );

            SprUnderWater = new Sprite(Ressources.TileCreatures, 3, new Point(16, 32));
        }
    }
}