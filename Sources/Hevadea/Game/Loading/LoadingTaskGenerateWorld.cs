using Hevadea.Game.Registry;
using Hevadea.WorldGenerator;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskGenerateWorld : LoadingTask
    {
        public override string TaskName => _generator?.CurrentLevel?.CurrentFeature != null ? _generator.CurrentLevel.CurrentFeature.GetName() : "World Generator";
        

        private int _seed ;
        private Generator _generator;

        public LoadingTaskGenerateWorld(int seed)
        {
            _seed = seed;
        }

        public override float GetProgress()
        {
            return _generator?.CurrentLevel?.CurrentFeature != null ? _generator.CurrentLevel.GetProgress() : 0f;
        }

        public override void Task(GameManager game)
        {
            _generator = GENERATOR.DEFAULT;
            _generator.Seed = _seed;
            var world = _generator.Generate();
            game.World = world;
        }
    }
}
