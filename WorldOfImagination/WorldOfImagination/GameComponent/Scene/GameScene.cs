using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using WorldOfImagination.Game;
using WorldOfImagination.GameComponent.UI;
using Padding = WorldOfImagination.GameComponent.UI.Padding;

namespace WorldOfImagination.GameComponent.Scene
{
    public class GameScene : Scene
    {

        public List<Entity> Entities;
        private SpriteBatch spriteBatch;
        private GameState State;
       
        
        public GameScene(WorldOfImaginationGame game) : base(game)
        {
            State = new GameState(Game);

            var player = new Player(game, State);

            Entities = new List<Entity>
            {
                player
            };

            State.Camera.FocusEntity = player;
        }

        private DialogBox dialog;
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
            UiRoot.Padding = new Padding(16);
            dialog = new DialogBox(Game.UI)
            {
                Dock = Dock.Bottom,
                Bound = new Rectangle(0,0, 256, 256)
            }; 
            
            UiRoot.AddChild(dialog);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.Input.KeyPress(Keys.P)) dialog.Show("Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                                                         " In et ipsum massa." +
                                                         " Aenean euismod purus ipsum, in euismod dolor pretium et." +
                                                         " Sed id. ");
            if (Game.Input.KeyPress(Keys.O)) dialog.Hide();
            
            foreach (var e in Entities)
            {
                e.SpeedX = e.SpeedX * 0.90f;
                e.SpeedY = e.SpeedY * 0.90f;

                e.Update(gameTime);

                e.SpeedX = Math.Max(Math.Min(e.MaxSpeed, e.SpeedX), -e.MaxSpeed);
                e.SpeedY = Math.Max(Math.Min(e.MaxSpeed, e.SpeedY), -e.MaxSpeed);

                e.X += e.SpeedX;
                e.Y += e.SpeedY;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, Game.RasterizerState, null, State.Camera.GetTransform());
            spriteBatch.Draw(Game.Ress.img_terrain, Vector2.Zero, Color.White);
            foreach (var e in Entities)
            {


                e.Draw(spriteBatch,gameTime);
            }
            
            spriteBatch.End();
        }

        public override void Unload()
        {
            
        }
    }
}