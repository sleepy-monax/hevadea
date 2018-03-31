using Hevadea.Entities.Blueprints;
using Hevadea.Registry;

namespace Hevadea.Loading.Tasks
{
    public class TaskNewPlayer : LoadingTask
    {        
        public override void Task(GameManager game)
        {
            SetStatus("Creating player...");
            game.MainPlayer = (EntityPlayer)ENTITIES.PLAYER.Construct();
        }
    }
}