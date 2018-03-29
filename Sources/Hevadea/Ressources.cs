using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game;
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

        public static Texture2D ImgMap;
        public static Texture2D ImgMapOver;
        public static Texture2D ImgMapIcon;
        
        public static Texture2D ImgLight;
        public static Texture2D ImgShadow;
        public static Texture2D ImgSwing;

        public static Texture2D CompanyLogo;
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
        
        public static SpriteSheet TileMinimapIcon;
        public static Sprite[]    MinimapIcon = new Sprite[16];

        public static void Load()
        {
            FontAlagard = Rise.Ressource.GetSpriteFont("alagard");
            FontAlagardBig = Rise.Ressource.GetSpriteFont("alagard_big");
            FontRomulus = Rise.Ressource.GetSpriteFont("romulus");
            FontHack = Rise.Ressource.GetSpriteFont("hack");

            ImgMap = Rise.Ressource.GetImage("map");
            ImgMapOver = Rise.Ressource.GetImage("map_overlay");
            ImgMapIcon = Rise.Ressource.GetImage("map_icon");
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

            CompanyLogo = Rise.Ressource.GetTexture("logo/company");

            ImgForestBackground = Rise.Ressource.GetImage("background/forest");
            ImgForestLight = Rise.Ressource.GetImage("background/forest_light");
            ImgForestTrees0 = Rise.Ressource.GetImage("background/forest_trees0");
            ImgForestTrees1 = Rise.Ressource.GetImage("background/forest_trees1");

            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileGui = new SpriteSheet(ImgGui, new Point(16, 16));

            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));

            TileMinimapIcon = new SpriteSheet(ImgMapIcon, new Point(8, 8));

            for (int i = 0; i < 16; i++)
            {
                MinimapIcon[i] = new Sprite(TileMinimapIcon, i);
            }
            
            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(ImgForestBackground, 0.5f),
                new ParalaxeLayer(ImgForestTrees0, 1f),
                new ParalaxeLayer(ImgForestLight, 2f),
                new ParalaxeLayer(ImgForestTrees1, 4f)
            );

            SprUnderWater = new Sprite(TileCreatures, 3, new Point(16, 32));
            SprPickup = new Sprite(TileCreatures, 1, new Point(16, 32));
        }
    }
}