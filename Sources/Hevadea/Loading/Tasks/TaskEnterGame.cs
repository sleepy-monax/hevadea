using Hevadea.Framework;
using Hevadea.Scenes;

namespace Hevadea.Loading.Tasks
{
    public class TaskEnterGame : LoadingTask
    {
        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Entering game...");
            Rise.Scene.Switch(new SceneGameplay(game));
        }
    }
}