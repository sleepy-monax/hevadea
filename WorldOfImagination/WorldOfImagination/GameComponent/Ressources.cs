using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent
{
    public class Ressources
    {
        private WorldOfImaginationGame Game;

        // Fonts --------------------------------------------------------------
        
        public SpriteFont font_alagard;
        public SpriteFont font_arial;
        public SpriteFont font_romulus;
        public SpriteFont font_bebas;
        public SpriteFont font_hack;
        
        public Texture2D icon_back;
        public Texture2D icon_edit;
        public Texture2D icon_close;
        public Texture2D icon_add;
        public Texture2D icon_play;
        public Texture2D icon_people;
        public Texture2D icon_single_people;
        public Texture2D icon_delete;
        public Texture2D icon_settings;

        public Texture2D img_terrain;
        public Texture2D img_characters;
        public Texture2D img_menu_background;

        public Ressources(WorldOfImaginationGame game)
        {
            Game = game;
        }

        public void Load()
        {
            font_alagard = Game.Ressource.GetSpriteFont("alagard");
            font_arial = Game.Ressource.GetSpriteFont("arial");
            font_romulus = Game.Ressource.GetSpriteFont("romulus");
            font_bebas = Game.Ressource.GetSpriteFont("bebas");
            font_hack = Game.Ressource.GetSpriteFont("hack");

            icon_back = Game.Ressource.GetIcon("back_arrow");
            icon_close = Game.Ressource.GetIcon("close");
            icon_play = Game.Ressource.GetIcon("play");
            icon_add = Game.Ressource.GetIcon("add");
            icon_people = Game.Ressource.GetIcon("people");
            icon_single_people = Game.Ressource.GetIcon("single_people");
            icon_delete = Game.Ressource.GetIcon("delete");
            icon_settings = Game.Ressource.GetIcon("settings");
            icon_edit = Game.Ressource.GetIcon("edit");

            img_terrain = Game.Ressource.GetImage("Tiles");
            img_characters = Game.Ressource.GetImage("Characters");
            img_menu_background = Game.Ressource.GetImage("menu_background");
        }
    }
}