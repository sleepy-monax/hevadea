using Hevadea.Game.Registry;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskGenerateWorld : LoadingTask
    {
        public override string TaskName => "world generation";

        private int _seed ;

        public LoadingTaskGenerateWorld(int seed)
        {
            _seed = seed;
        }

        public override void Task(GameManager game)
        {
            var generator = GENERATOR.DEFAULT;
            generator.Seed = _seed;
            var world = generator.Generate();
            game.World = world;
        }
    }
}
