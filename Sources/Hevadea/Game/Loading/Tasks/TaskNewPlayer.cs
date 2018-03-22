using Hevadea.Game.Entities;
using Hevadea.Game.Registry;

namespace Hevadea.Game.Loading.Tasks
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