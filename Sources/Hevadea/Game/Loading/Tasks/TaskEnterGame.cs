using Hevadea.Framework;
using Hevadea.Scenes;

namespace Hevadea.Game.Loading.Tasks
{
    public class TaskEnterGame : LoadingTask
    {
        public override void Task(GameManager game)
        {
            SetStatus("Entering game...");
            Rise.Scene.Switch(new SceneGameplay(game));
        }
    }
}