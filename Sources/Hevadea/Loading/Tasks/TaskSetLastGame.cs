using System.IO;
using Hevadea.Framework;

namespace Hevadea.Loading.Tasks
{
    public class TaskSetLastGame : LoadingTask
    {
        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Registering last game...");
            File.WriteAllText(Rise.Platform.GetStorageFolder() + "/.lastgame", game.SavePath);
        }
    }
}