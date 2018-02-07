using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.UI;
using Maker.Rise.Enums;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Menus
{
    public class ChestMenu : Menu
    {
        
        public ChestMenu(Entity entity, Entity chest, GameManager game) : base(game)
        {
            PauseGame = true;
        }
    }
}