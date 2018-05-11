using Hevadea.Loading.Tasks;
using Hevadea.Scenes.MainMenu;
using Hevadea.Scenes.Menus;

namespace Hevadea.Loading
{
    public static class TaskFactorie
    {
        public static TaskCompound ConstructNewWorld(string path, int seed)
        {
            return new TaskCompound(path)
            {
                Tasks =
                {
                    new TaskSetLastGame(),
                    new TaskGenerateWorld(seed),
                    new TaskNewPlayer(),
                    new TaskInitializeGame(),
                    new TaskSaveWorld(),
                    new TaskEnterGame()
                }
            };
        }

        public static TaskCompound ConstructSaveWorld(GameManager game)
        {
            return new TaskCompound(game)
            {
                Tasks =
                {
                    new TaskSetLastGame(),
                    new TaskSaveWorld(),
                    new TaskSwitchToMenu(new MenuInGame(game))
                }
            };
        }

        public static TaskCompound ConstructSaveWorldAndExit(GameManager game)
        {
            return new TaskCompound(game)
            {
                Tasks =
                {
                    new TaskSetLastGame(),
                    new TaskSaveWorld(),
                    new TaskSwitchToScene(new SceneMainMenu())
                }
            };
        }

        public static TaskCompound ConstructLoadWorld(string path)
        {
            return new TaskCompound(path)
            {
                Tasks =
                {
                    new TaskSetLastGame(),
                    new TaskLoadWorld(),
                    new TaskInitializeGame(),
                    new TaskEnterGame()
                }
            };
        }
    }
}