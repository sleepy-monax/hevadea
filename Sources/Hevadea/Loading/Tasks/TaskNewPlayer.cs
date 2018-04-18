using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.Registry;

namespace Hevadea.Loading.Tasks
{
    public class TaskNewPlayer : LoadingTask
    {        
        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Creating player...");
            game.MainPlayer = (EntityPlayer)EntityFactory.PLAYER.Construct();
        }
    }
}