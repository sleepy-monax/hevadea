using Maker.Rise;
using Maker.Rise.GameComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game;


namespace WorldOfImagination.Scenes
{
    public class GameScene : Scene
    {

        private SpriteBatch spriteBatch;
        public World World;


        public GameScene(RiseGame game, World world) : base(game)
        {
            World = world;
        }
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            GenerateLevel(0);
            World.Initialize();

        }

        public void GenerateLevel(int seed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            World.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Game.Debug.Visible);
        }

        public override void Unload()
        {
            
        }
    }
}