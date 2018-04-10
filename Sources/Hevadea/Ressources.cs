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

        public static Sprite SprPickup;
        public static Sprite SprUnderWater;
        public static Sprite[]    MinimapIcon = new Sprite[16];

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
        public static SpriteSheet TileTiles;

        public static Texture2D CompanyLogo;

        public static Texture2D ImgCreatures;
        public static Texture2D ImgEngineLogo;
        public static Texture2D ImgEntities;
        public static Texture2D ImgForestBackground;
        public static Texture2D ImgForestLight;
        public static Texture2D ImgForestTrees0;
        public static Texture2D ImgForestTrees1;
        public static Texture2D ImgGui;
        public static Texture2D ImgHevadeaLogo;
        public static Texture2D ImgIcons;
        public static Texture2D ImgItems;
        public static Texture2D ImgLight;
        public static Texture2D ImgMap;
        public static Texture2D ImgMapIcon;
        public static Texture2D ImgMapOver;
        public static Texture2D ImgRock;
        public static Texture2D ImgShadow;
        public static Texture2D ImgSwing;
        public static Texture2D ImgTiles;

        public static void Load()
        {
            CompanyLogo = Rise.Ressource.GetTexture("logo/company");

            FontAlagard = Rise.Ressource.GetSpriteFont("alagard");
            FontAlagardBig = Rise.Ressource.GetSpriteFont("alagard_big");
            FontHack = Rise.Ressource.GetSpriteFont("hack");
            FontRomulus = Rise.Ressource.GetSpriteFont("romulus");

            ImgCreatures = Rise.Ressource.GetImage("creatures");
            ImgEntities = Rise.Ressource.GetImage("entities");
            ImgForestBackground = Rise.Ressource.GetImage("background/forest");
            ImgForestLight = Rise.Ressource.GetImage("background/forest_light");
            ImgForestTrees0 = Rise.Ressource.GetImage("background/forest_trees0");
            ImgForestTrees1 = Rise.Ressource.GetImage("background/forest_trees1");
            ImgGui = Rise.Ressource.GetImage("gui");
            ImgIcons = Rise.Ressource.GetImage("icons");
            ImgItems = Rise.Ressource.GetImage("items");
            ImgLight = Rise.Ressource.GetImage("light");
            ImgMap = Rise.Ressource.GetImage("map");
            ImgMapIcon = Rise.Ressource.GetImage("map_icon");
            ImgMapOver = Rise.Ressource.GetImage("map_overlay");
            ImgRock = Rise.Ressource.GetImage("rock");
            ImgShadow = Rise.Ressource.GetImage("shadow");
            ImgSwing = Rise.Ressource.GetImage("swing");
            ImgTiles = Rise.Ressource.GetImage("tiles");

            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileGui = new SpriteSheet(ImgGui, new Point(16, 16));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileMinimapIcon = new SpriteSheet(ImgMapIcon, new Point(8, 8));
            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));
            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));

            for (int i = 0; i < 16; i++)
            {
                MinimapIcon[i] = new Sprite(TileMinimapIcon, i);
            }
            
            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(ImgForestBackground, 32f),
                new ParalaxeLayer(ImgForestTrees0, 64f),
                new ParalaxeLayer(ImgForestLight, 128f),
                new ParalaxeLayer(ImgForestTrees1, 256f)
            );

            SprUnderWater = new Sprite(TileCreatures, 3, new Point(16, 32));
            SprPickup = new Sprite(TileCreatures, 1, new Point(16, 32));
        }
    }
}