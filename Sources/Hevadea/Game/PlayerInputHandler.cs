using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Hevadea.Framework;
using Hevadea.Game.Entities.Component.Ai;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Hevadea.Framework.Utils;

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

        public void Update(GameTime gameTime)
        {
            var game = _player.Game;
            
            var input = Rise.Input;
            var screenBound = Rise.Graphic.GetBound();
            
            if (Rise.Pointing.AreaDown(screenBound))
            {
                var mousePositionOnScreen = Rise.Pointing.GetAreaOver(screenBound)[0].ToVector2();
                var mousePositionInWorld = game.Camera.ToWorldSpace(mousePositionOnScreen);
                var screenCenter = Rise.Graphic.GetCenter();

                if (Mathf.Distance(mousePositionOnScreen.X, mousePositionOnScreen.Y, screenCenter.X, screenCenter.Y) < Rise.Graphic.GetHeight() / 2)
                {
                    _player.Get<Agent>().MoveTo(mousePositionInWorld.X, mousePositionInWorld.Y, 1f);
                }
            }
            
            if (game.CurrentMenu == null || !game.CurrentMenu.PauseGame)
            {                
                if (input.KeyDown(Keys.Z)) HandleInput(PlayerInput.MoveUp);
                if (input.KeyDown(Keys.S)) HandleInput(PlayerInput.MoveDown);
                if (input.KeyDown(Keys.Q)) HandleInput(PlayerInput.MoveLeft);
                if (input.KeyDown(Keys.D)) HandleInput(PlayerInput.MoveRight);
                
                if (input.KeyPress(Keys.E)) HandleInput(PlayerInput.OpenInventory);

                if (input.KeyDown(Keys.J)) HandleInput(PlayerInput.Attack);
                if (input.KeyPress(Keys.K)) HandleInput(PlayerInput.Action);
            }

            if (input.KeyPress(Keys.Escape)) HandleInput(PlayerInput.OpenPauseMenu);
        }
        
        public void HandleInput(PlayerInput input)
        {
            var game = _player.Game;
            var playerMovement = _player.Get<Move>();
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
                    game.CurrentMenu = new InventoryMenu(_player, RECIPIES.HandCrafted, game);
                    break;
                case PlayerInput.OpenPauseMenu:
                    if (game.CurrentMenu is HudMenu)
                    {
                        game.CurrentMenu = new PauseMenu(game);
                    }
                    else
                    {
                        game.CurrentMenu = new HudMenu(game);
                    }
                    break;
              
            }
        }
    }
}
