using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;

namespace Maker.Hevadea.Scenes.Menus
{
    public class ChestMenu : Menu
    {
        public ChestMenu(Entity entity, Entity chest, GameManager game) : base(game)
        {
            PauseGame = true;
        }
    }
}