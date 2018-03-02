using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game
{
    public enum PlayerInput
    {
        MoveLeft, MoveRight, MoveUp, MoveDown, Action, Attack, OpenInventory, OpenPauseMenu
    }

    public class PlayerInputHandler
    {
        public PlayerEntity _player;

        public PlayerInputHandler(PlayerEntity player)
        {
            _player = player;
            
        }

        public void HandleInput(PlayerInput input)
        {
            var playerMovement = _player.Get<Move>();
            var _game = _player.Game;
            switch (input)
            {
                case PlayerInput.MoveLeft:
                    playerMovement.Do(-1, 0, Direction.Left);
                    break;
                case PlayerInput.MoveRight:
                    playerMovement.Do(+1, 0, Direction.Right);
                    break;
                case PlayerInput.MoveUp:
                    playerMovement.Do(0, -1, Direction.Up);
                    break;
                case PlayerInput.MoveDown:
                    playerMovement.Do(0, +1, Direction.Down);
                    break;
                case PlayerInput.Action:
                    if (_player.Get<Inventory>().Content.Count(_player.HoldingItem) == 0)
                        _player.HoldingItem = null;

                    _player.Get<Interact>().Do(_player.HoldingItem);
                    break;
                case PlayerInput.Attack:
                    _player.Get<Attack>().Do(_player.HoldingItem);
                    break;
                case PlayerInput.OpenInventory:
                    _game.CurrentMenu = new InventoryMenu(_player, RECIPIES.HandCrafted, _game);
                    break;
                case PlayerInput.OpenPauseMenu:
                    if (_game.CurrentMenu is HudMenu)
                    {
                        _game.CurrentMenu = new PauseMenu(_game);
                    }
                    else
                    {
                        _game.CurrentMenu = new HudMenu(_game);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
