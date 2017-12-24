using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WorldOfImagination.Game;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.LevelGen;

namespace WorldOfImagination.Scenes
{
    public class GameScene : Scene
    {

        private SpriteBatch spriteBatch;
        private Level level;
        private Camera camera;
        
        public GameScene(RiseGame game) : base(game)
        {


        }

        private DialogBox dialog;
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            level = new Generator().GenerateEmptyLevel();
            level.AddEntity(new Player(Game) { Position = new EntityPosition((level.W / 2) * ConstVal.TileSize, (level.H / 2) * ConstVal.TileSize) });

            camera = new Camera(Game);
            camera.FocusEntity = level.Player;

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

            level.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, camera.GetTransform());
            level.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        public override void Unload()
        {
            
        }
    }
}