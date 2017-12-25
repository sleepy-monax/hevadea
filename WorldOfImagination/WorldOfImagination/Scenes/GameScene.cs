using Maker.Rise;
using Maker.Rise.GameComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.LevelGen;
using WorldOfImagination.Game.LevelGen.Features.Overworld;


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
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            level = new Generator
            {
                Seed = 0,
                Features =
                {
                    new OverworldBaseTerrain(),
                    //new TreeFeature()
                }

            }.Generate();

            level.AddEntity(new Player(Game) { Position = new EntityPosition((level.W / 2) * ConstVal.TileSize, (level.H / 2) * ConstVal.TileSize) });

            camera = new Camera(Game) { FocusEntity = level.Player };
        }

        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, camera.GetTransform());

            level.Draw(spriteBatch, camera, gameTime);

            spriteBatch.End();
        }

        public override void Unload()
        {
            
        }
    }
}