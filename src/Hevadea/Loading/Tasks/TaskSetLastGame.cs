using Hevadea.Framework;
using System.IO;

namespace Hevadea.Loading.Tasks
{
    public class TaskSetLastGame : LoadingTask
    {
        public override void Task(GameManager game)
        {
            SetStatus("Registering last game...");
            File.WriteAllText(Rise.Platform.GetStorageFolder() + "/.lastgame", game.SavePath);
        }
    }
}