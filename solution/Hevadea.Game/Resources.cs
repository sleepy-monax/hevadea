using Hevadea.Framework;
using Hevadea.Framework.Audio;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Hevadea
{
    public static class Resources
    {
        public static ParalaxeBackground ParalaxeForest;
        public static ParalaxeBackground ParalaxeMontain;
        public static _SpriteAtlas Sprites;

        public static Sprite SprPickup;
        public static Sprite SprUnderWater;
        public static Sprite[] MinimapIcon = new Sprite[16];

        public static SpriteFont FontAlagard;
        public static SpriteFont FontHack;
        public static SpriteFont FontRomulus;

        public static SpriteSheet TileCreatures;
        public static SpriteSheet TileEntities;
        public static SpriteSheet TileGrass;
        public static SpriteSheet TileGui;
        public static SpriteSheet TileIcons;
        public static SpriteSheet TileItems;
        public static SpriteSheet TileMinimapIcon;
        public static SpriteSheet TileRock;
        public static SpriteSheet TileTiles;

        public static Texture2D ImgZombie;
        public static Texture2D ImgPlayer;
        public static Texture2D ImgChicken;
        public static Texture2D ImgDog;

        public static Texture2D ImgCreatures;
        public static Texture2D ImgEngineLogo;
        public static Texture2D ImgEntities;
        public static Texture2D ImgGrass;
        public static Texture2D ImgGui;
        public static Texture2D ImgHevadeaLogo;
        public static Texture2D ImgIcons;
        public static Texture2D ImgItems;
        public static Texture2D ImgMap;
        public static Texture2D ImgMapIcon;
        public static Texture2D ImgMapOver;
        public static Texture2D ImgHealthbar;
        public static Texture2D ImgRock;
        public static Texture2D ImgSwing;
        public static Texture2D ImgTiles;
        public static Texture2D MakerLogo;

        public static Texture2D ImgWater;
        public static Texture2D ImgLight;
        public static Texture2D ImgShadow;

        public static SoundEffect UiClick;
        public static SoundEffect UiOver;

        public static SoundEffectPool PoolSwings;

        public static Song Theme0;
        public static Song Overworld0;

        public static void Load()
        {
            MakerLogo = Rise.Resources.GetImage("maker-logo");
            Sprites = new _SpriteAtlas(512, 512, "assets/");
            Sprites.Bitmap.Save("test.png");


            FontAlagard = Rise.Resources.GetSpriteFont("alagard");
            FontHack = Rise.Resources.GetSpriteFont("hack");
            FontRomulus = Rise.Resources.GetSpriteFont("romulus");

            ImgCreatures = Rise.Resources.GetImage("creatures");
            ImgEntities = Rise.Resources.GetImage("entities");

            ImgGui = Rise.Resources.GetImage("gui");
            ImgIcons = Rise.Resources.GetImage("icons");
            ImgItems = Rise.Resources.GetImage("items");
            ImgMap = Rise.Resources.GetImage("map");
            ImgMapIcon = Rise.Resources.GetImage("map_icon");
            ImgMapOver = Rise.Resources.GetImage("map_overlay");
            ImgHealthbar = Rise.Resources.GetImage("healthbar");
            ImgRock = Rise.Resources.GetImage("rock");
            ImgGrass = Rise.Resources.GetImage("grass");
            ImgTiles = Rise.Resources.GetImage("tiles");

            ImgPlayer = Rise.Resources.GetTexture("entities/player");
            ImgZombie = Rise.Resources.GetTexture("entities/zombie");
            ImgChicken = Rise.Resources.GetTexture("entities/chicken");
            ImgDog = Rise.Resources.GetTexture("entities/dog");

            ImgShadow = Rise.Resources.GetTexture("effects/shadow");
            ImgLight = Rise.Resources.GetTexture("effects/light");
            ImgWater = Rise.Resources.GetTexture("effects/water");

            TileCreatures = new SpriteSheet(ImgCreatures, new Point(48, 128));
            TileEntities = new SpriteSheet(ImgEntities, new Point(16, 16));
            TileGui = new SpriteSheet(ImgGui, new Point(16, 16));
            TileIcons = new SpriteSheet(ImgIcons, new Point(16, 16));
            TileItems = new SpriteSheet(ImgItems, new Point(16, 16));
            TileMinimapIcon = new SpriteSheet(ImgMapIcon, new Point(8, 8));
            TileRock = new SpriteSheet(ImgRock, new Point(16, 16));
            TileGrass = new SpriteSheet(ImgGrass, new Point(16, 16));
            TileTiles = new SpriteSheet(ImgTiles, new Point(32, 32));

            for (var i = 0; i < 16; i++) MinimapIcon[i] = new Sprite(TileMinimapIcon, i);

            ParalaxeForest = new ParalaxeBackground(
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/forest0"), 0f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/forest1"), 64f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/forest2"), 96f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/forest3"), 128f)
            );

            ParalaxeMontain = new ParalaxeBackground(
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/montain0"), 0f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/montain1"), 64f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/montain2"), 96f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/montain3"), 128f),
                new ParalaxeLayer(Rise.Resources.GetTexture("backgrounds/montain4"), 160f)
            );

            SprUnderWater = new Sprite(TileCreatures, 3, new Point(16, 32));
            SprPickup = new Sprite(TileCreatures, 1, new Point(16, 32));

            // Sound effects --------------------------------------------------

            UiClick = Rise.Resources.GetSoundEffect("ui1");
            UiOver = Rise.Resources.GetSoundEffect("ui0");

            PoolSwings = new SoundEffectPool()
            {
                Sounds =
                {
                    Rise.Resources.GetSoundEffect("swing0"),
                    Rise.Resources.GetSoundEffect("swing1"),
                    Rise.Resources.GetSoundEffect("swing2"),
                }
            };

            // Songs ----------------------------------------------------------
            Theme0 = Rise.Resources.GetSong("theme0");
            Overworld0 = Rise.Resources.GetSong("overworld0");
        }
    }
}