using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea
{
    public static class Ressources
    {
        public static ParalaxeBackground ParalaxeForest;
        public static ParalaxeBackground ParalaxeMontain;

        public static Sprite SprPickup;
        public static Sprite SprUnderWater;
        public static Sprite[] MinimapIcon = new Sprite[16];

        public static SpriteFont FontAlagard;
        public static SpriteFont FontAlagardBig;
        public static SpriteFont FontHack;
        public static SpriteFont FontRomulus;
        public static SpriteSheet TileCreatures;
        public static SpriteSheet TileEntities;
        public static SpriteSheet TileGui;
        public static SpriteSheet TileIcons;
        public static SpriteSheet TileItems;
        public static SpriteSheet TileMinimapIcon;
        public static SpriteSheet TileRock;
        public static SpriteSheet TileGrass;
        public static SpriteSheet TileTiles;

        public static Texture2D MakerLogo;

        public static Texture2D ImgCreatures;
        public static Texture2D ImgEngineLogo;
        public static Texture2D ImgEntities;
        public static Texture2D ImgGui;
        public static Texture2D ImgHevadeaLogo;
        public static Texture2D ImgIcons;
        public static Texture2D ImgItems;
        public static Texture2D ImgLight;
        public static Texture2D ImgMap;
        public static Texture2D ImgMapIcon;
        public static Texture2D ImgMapOver;
        public static Texture2D ImgRock;
        public static Texture2D ImgGrass;
        public static Texture2D ImgShadow;
        public static Texture2D ImgSwing;
        public static Texture2D ImgTiles;

        public static void Load()
        {
            MakerLogo = Rise.Ressource.GetImage("maker-logo");

            FontAlagard = Rise.Ressource.GetSpriteFont("alagard");
            FontAlagardBig = Rise.Ressource.GetSpriteFont("alagard_big");
            FontHack = Rise.Ressource.GetSpriteFont("hack");
            FontRomulus = Rise.Ressource.GetSpriteFont("romulus");

            ImgCreatures = Rise.Ressource.GetImage("creatures");
            ImgEntities = Rise.Ressource.GetImage("entities");

            ImgGui = Rise.Ressource.GetImage("gui");
            ImgIcons = Rise.Ressource.GetImage("icons");
            ImgItems = Rise.Ressource.GetImage("items");
            ImgLight = Rise.Ressource.GetImage("light");
            ImgMap = Rise.Ressource.GetImage("map");
            ImgMapIcon = Rise.Ressource.GetImage("map_icon");
            ImgMapOver = Rise.Ressource.GetImage("map_overlay");
            ImgRock = Rise.Ressource.GetImage("rock");
            ImgGrass = Rise.Ressource.GetImage("grass");
            ImgShadow = Rise.Ressource.GetImage("shadow");
            ImgTiles = Rise.Ressource.GetImage("tiles");

            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileGui = new SpriteSheet(ImgGui, new Point(16, 16));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileMinimapIcon = new SpriteSheet(ImgMapIcon, new Point(8, 8));
            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));
            TileGrass = new SpriteSheet(ImgGrass, new Point(16, 16));
            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));

            for (int i = 0; i < 16; i++)
            {
                MinimapIcon[i] = new Sprite(TileMinimapIcon, i);
            }

            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(Rise.Ressource.GetImage("background/forest0"), 0f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/forest1"), 64f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/forest2"), 96f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/forest3"), 128f)
            );

            ParalaxeMontain = new ParalaxeBackground(
                new ParalaxeLayer(Rise.Ressource.GetImage("background/montain0"), 0f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/montain1"), 64f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/montain2"), 96f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/montain3"), 128f),
                new ParalaxeLayer(Rise.Ressource.GetImage("background/montain4"), 160f)
            );

            SprUnderWater = new Sprite(TileCreatures, 3, new Point(16, 32));
            SprPickup = new Sprite(TileCreatures, 1, new Point(16, 32));
        }
    }
}