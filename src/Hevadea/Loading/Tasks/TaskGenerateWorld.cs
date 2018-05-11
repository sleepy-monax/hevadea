using Hevadea.Registry;
using Hevadea.WorldGenerator;

namespace Hevadea.Loading.Tasks
{
    public class TaskGenerateWorld : LoadingTask
    {
        private int _seed;
        private Generator _generator;

        public TaskGenerateWorld(int seed)
        {
            _seed = seed;
        }

        public override string GetStatus()
        {
            return _generator?.CurrentLevel?.CurrentFeature != null ? _generator.CurrentLevel.CurrentFeature.GetName() : "Generating world...";
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