using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.GameComponent
{
    public class Ressources
    {
        private readonly RiseGame Game;

        // Fonts --------------------------------------------------------------
        public SpriteFont font_alagard;
        public SpriteFont font_arial;
        public SpriteFont font_arial_tiny;
        public SpriteFont font_romulus;
        public SpriteFont font_bebas;
        public SpriteFont font_bebas_big;
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

        public Texture2D img_menu_background;
        public Texture2D img_maker_logo;

        public Texture2D img_forest_background;
        public Texture2D img_forest_light;
        public Texture2D img_forest_trees0;
        public Texture2D img_forest_trees1;
        public SoundEffect menu_select;
        public SoundEffect menu_pick;


        public Ressources(RiseGame game)
        {
            Game = game;
        }

        public void Load()
        {
            font_alagard = Game.Ressource.GetSpriteFont("alagard");
            font_arial = Game.Ressource.GetSpriteFont("arial");
            font_arial_tiny = Game.Ressource.GetSpriteFont("arial_tiny");
            font_romulus = Game.Ressource.GetSpriteFont("romulus");
            font_bebas = Game.Ressource.GetSpriteFont("bebas");
            font_bebas_big = Game.Ressource.GetSpriteFont("bebas_big");
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

            img_menu_background = Game.Ressource.GetImage("menu_background");
            img_maker_logo = Game.Ressource.GetImage("maker_logo");
            
            img_forest_background = Game.Ressource.GetImage("forest_background");
            img_forest_light = Game.Ressource.GetImage("forest_light");
            img_forest_trees0 = Game.Ressource.GetImage("forest_trees0");
            img_forest_trees1 = Game.Ressource.GetImage("forest_trees1");
            menu_select = Game.Content.Load<SoundEffect>("Sounds/menu_select");
            menu_pick = Game.Content.Load<SoundEffect>("Sounds/menu_pick");

        }
    }
}