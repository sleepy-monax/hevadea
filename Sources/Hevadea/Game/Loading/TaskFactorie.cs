﻿using Hevadea.Game.Loading.Tasks;
using Hevadea.Scenes.Menus;
using Hevadea.Scenes;

namespace Hevadea.Game.Loading
{
    public static class TaskFactorie
    {
        public static TaskCompound ConstructStartServer(GameManager game, int port = GameManager.PORT)
        {
            return new TaskCompound(game)
            {
                Tasks =
                {
                    new TaskSaveWorld(),
                    new TaskSetupServer{Port = port},
                }
            };
        }

        public static TaskCompound ConstructConnectToServer(string addr, int port = GameManager.PORT)
        {
            return new TaskCompound($"{addr}:{port}")
            {
                Tasks =
                {
                    new TaskConnectToServer(addr, port),
                    new TaskDownloadWorld(),
                    new TaskPlayerJoint(),
                }
            };
        }
            
        public static TaskCompound ConstructNewWorld(string path, int seed)
        {
            return new TaskCompound(path)
            {
                Tasks =
                {
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
                    new TaskSaveWorld(),
                    new TaskSwitchToScene(new MainMenu())
                }
            };
        }

        public static TaskCompound ConstructLoadWorld(string path)
        {
            return new TaskCompound(path)
            {
                Tasks =
                {
                    new TaskLoadWorld(),
                    new TaskInitializeGame(),
                    new TaskEnterGame()
                }
            };
        }

    }
}