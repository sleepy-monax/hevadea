using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Registry;

namespace Hevadea.Loading.Tasks
{
    public class TaskNewPlayer : LoadingTask
    {        
        public override void Task(GameManager game)
        {
            SetStatus("Creating player...");
            game.MainPlayer = (EntityPlayer)EntityFactory.PLAYER.Construct();
        }
    }
}