using Hevadea.Framework;
using Hevadea.Scenes;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskEnterGame : LoadingTask
    {
        public override string TaskName => "enter_game";
        public override void Task(GameManager game)
        {
            Rise.Scene.Switch(new SceneGameplay(game));
        }
    }
}