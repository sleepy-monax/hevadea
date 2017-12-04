using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.GameComponent
{
    public class Ressources
    {
        private WorldOfImaginationGame Game;

        // Fonts --------------------------------------------------------------
        
        public SpriteFont alagard;
        public SpriteFont arial;
        public SpriteFont romulus;
        public SpriteFont bebas;
        
        public Texture2D icon_back;
        public Texture2D icon_close;
        public Texture2D icon_add;
        public Texture2D icon_play;
        public Texture2D icon_people;
        public Texture2D icon_delete;

        public Texture2D img_terrain;
        
        public Ressources(WorldOfImaginationGame game)
        {
            Game = game;
        }

        public void Load()
        {
            alagard = Game.Ressource.GetSpriteFont("alagard");
            arial = Game.Ressource.GetSpriteFont("arial");
            romulus = Game.Ressource.GetSpriteFont("romulus");
            bebas = Game.Ressource.GetSpriteFont("bebas");

            icon_back = Game.Ressource.GetIcon("back_arrow");
            icon_close = Game.Ressource.GetIcon("close");
            icon_play = Game.Ressource.GetIcon("play");
            icon_add = Game.Ressource.GetIcon("add");
            icon_people = Game.Ressource.GetIcon("people");
            icon_delete = Game.Ressource.GetIcon("delete");

            img_terrain = Game.Ressource.GetImage("Tiles");
        }
    }
}