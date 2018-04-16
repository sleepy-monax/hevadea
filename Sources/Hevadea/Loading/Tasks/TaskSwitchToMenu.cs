using Hevadea.Scenes.Menus;

namespace Hevadea.Loading.Tasks
{
    public class TaskSwitchToMenu : LoadingTask
    {
        private Menu _menu;

        public TaskSwitchToMenu(Menu menu)
        {
            _menu = menu;
        }

        public override void Task(GameManager.GameManager game)
        {
            SetStatus($"Switching to menu '{_menu.GetType().Name}'...");
            game.CurrentMenu = _menu;
        }
    }
}
