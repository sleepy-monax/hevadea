using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise
{
    public static class EngineRessources
    {
        public static SpriteFont FontArial;

        public static SpriteFont FontHack;
        public static SpriteFont FontArialTiny;
        public static SpriteFont FontBebas;
        public static SpriteFont FontBebasBig;

        public static Texture2D IconBack;
        public static Texture2D IconEdit;
        public static Texture2D IconClose;
        public static Texture2D IconAdd;
        public static Texture2D IconPlay;
        public static Texture2D IconPeople;
        public static Texture2D IconSinglePeople;
        public static Texture2D IconDelete;
        public static Texture2D IconSettings;

        public static SoundEffect MenuSelect;
        public static SoundEffect MenuPick;

        public static void Load()
        {

            FontArial = Engine.Ressource.GetSpriteFont("arial");
            FontArialTiny = Engine.Ressource.GetSpriteFont("arial_tiny");
            FontBebas = Engine.Ressource.GetSpriteFont("bebas");
            FontBebasBig = Engine.Ressource.GetSpriteFont("bebas_big");
            FontHack = Engine.Ressource.GetSpriteFont("hack");

            IconBack = Engine.Ressource.GetIcon("back_arrow");
            IconClose = Engine.Ressource.GetIcon("close");
            IconPlay = Engine.Ressource.GetIcon("play");
            IconAdd = Engine.Ressource.GetIcon("add");
            IconPeople = Engine.Ressource.GetIcon("people");
            IconSinglePeople = Engine.Ressource.GetIcon("single_people");
            IconDelete = Engine.Ressource.GetIcon("delete");
            IconSettings = Engine.Ressource.GetIcon("settings");
            IconEdit = Engine.Ressource.GetIcon("edit");

            MenuSelect = Engine.Ressource.GetSoundEffect("menu_select");
            MenuPick = Engine.Ressource.GetSoundEffect("menu_pick");
        }
    }
}