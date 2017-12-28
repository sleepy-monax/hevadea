using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise
{
    public static class EngineRessources
    {

        // Fonts --------------------------------------------------------------
        public static SpriteFont font_arial;
        public static SpriteFont font_hack;

        public static SpriteFont font_alagard;
        public static SpriteFont font_alagard_big;
        public static SpriteFont font_arial_tiny;
        public static SpriteFont font_romulus;
        public static SpriteFont font_bebas;
        public static SpriteFont font_bebas_big;
    
        public static Texture2D icon_back;
        public static Texture2D icon_edit;
        public static Texture2D icon_close;
        public static Texture2D icon_add;
        public static Texture2D icon_play;
        public static Texture2D icon_people;
        public static Texture2D icon_single_people;
        public static Texture2D icon_delete;
        public static Texture2D icon_settings;

        public static Texture2D img_menu_background;
        public static Texture2D img_maker_logo;

        public static Texture2D img_forest_background;
        public static Texture2D img_forest_light;
        public static Texture2D img_forest_trees0;
        public static Texture2D img_forest_trees1;

        public static SoundEffect menu_select;
        public static SoundEffect menu_pick;


        public static void Load()
        {
            font_alagard = Engine.Ressource.GetSpriteFont("alagard");
            font_alagard_big = Engine.Ressource.GetSpriteFont("alagard_big");
            font_arial = Engine.Ressource.GetSpriteFont("arial");
            font_arial_tiny = Engine.Ressource.GetSpriteFont("arial_tiny");
            font_romulus = Engine.Ressource.GetSpriteFont("romulus");
            font_bebas = Engine.Ressource.GetSpriteFont("bebas");
            font_bebas_big = Engine.Ressource.GetSpriteFont("bebas_big");
            font_hack = Engine.Ressource.GetSpriteFont("hack");

            icon_back = Engine.Ressource.GetIcon("back_arrow");
            icon_close = Engine.Ressource.GetIcon("close");
            icon_play = Engine.Ressource.GetIcon("play");
            icon_add = Engine.Ressource.GetIcon("add");
            icon_people = Engine.Ressource.GetIcon("people");
            icon_single_people = Engine.Ressource.GetIcon("single_people");
            icon_delete = Engine.Ressource.GetIcon("delete");
            icon_settings = Engine.Ressource.GetIcon("settings");
            icon_edit = Engine.Ressource.GetIcon("edit");

            img_menu_background = Engine.Ressource.GetImage("menu_background");
            img_maker_logo = Engine.Ressource.GetImage("maker_logo");

            img_forest_background = Engine.Ressource.GetImage("forest_background");
            img_forest_light = Engine.Ressource.GetImage("forest_light");
            img_forest_trees0 = Engine.Ressource.GetImage("forest_trees0");
            img_forest_trees1 = Engine.Ressource.GetImage("forest_trees1");
            menu_select = Engine.Ressource.GetSoundEffect("menu_select");
            menu_pick = Engine.Ressource.GetSoundEffect("menu_pick");

        }

    }
}
