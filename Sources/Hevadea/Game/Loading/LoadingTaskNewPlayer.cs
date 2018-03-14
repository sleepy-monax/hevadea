using Hevadea.Game.Entities;
using Hevadea.Game.Items.Tags;
using Hevadea.Game.Registry;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskNewPlayer : LoadingTask
    {
        public override string TaskName => "create_player";
        
        public override void Task(GameManager game)
        {
            game.MainPlayer = (EntityPlayer)ENTITIES.PLAYER.Construct();
        }
    }
}