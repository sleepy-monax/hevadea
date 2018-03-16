using Hevadea.Framework;
using Hevadea.Framework.Scening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Loading.Tasks
{
    public class TaskSwitchToScene : LoadingTask
    {
        private Scene _scene;

        public TaskSwitchToScene(Scene scene)
        {
            _scene = scene;
        }

        public override void Task(GameManager game)
        {
            SetStatus($"Switching to menu '{_scene.GetType().Name}'...");
            Rise.Scene.Switch(_scene);
        }
    }
}
