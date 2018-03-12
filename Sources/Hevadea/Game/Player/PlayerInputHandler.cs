using Hevadea.Framework;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Game
{
    public enum PlayerInput
    {
        MoveLeft, MoveRight, MoveUp, MoveDown, Action, Attack, OpenInventory, OpenPauseMenu, Zoom, Dzoom
    }

    public class PlayerInputHandler
    {
        public EntityPlayer Player;

        public PlayerInputHandler(EntityPlayer player)
        {
            Player = player;
        }

        public void Update(GameTime gameTime)
        {
            var game = Player.Game;
            var input = Rise.Input;
            var screenBound = Rise.Graphic.GetBound();
            
            if (!game.CurrentMenu?.PauseGame ?? false)
            {  
                if (Rise.Pointing.AreaDown(screenBound))
                {
                    var mousePositionOnScreen = Rise.Pointing.GetAreaOver(screenBound)[0].ToVector2();
                    var mousePositionInWorld = game.Camera.ToWorldSpace(mousePositionOnScreen);
                    var screenCenter = Rise.Graphic.GetCenter();
    
                    if (Mathf.Distance(mousePositionOnScreen.X, mousePositionOnScreen.Y, screenCenter.X, screenCenter.Y) < Rise.Graphic.GetHeight() / 2)
                    {
                        Player.Get<Move>().MoveTo(mousePositionInWorld.X, mousePositionInWorld.Y);
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

                    if (input.KeyPress(Keys.Add)) HandleInput(PlayerInput.Zoom);
                    if (input.KeyPress(Keys.Subtract)) HandleInput(PlayerInput.Dzoom);

                }
            }

            if (input.KeyPress(Keys.Escape)) HandleInput(PlayerInput.OpenPauseMenu);
        }
        
        public void HandleInput(PlayerInput input)
        {
            var game = Player.Game;
            var playerMovement = Player.Get<Move>();
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
                    if (Player.Get<Inventory>().Content.Count(Player.HoldingItem) == 0)
                        Player.HoldingItem = null;

                    Player.Get<Interact>().Do(Player.HoldingItem);
                    break;
                case PlayerInput.Attack:
                    Player.Get<Attack>().Do(Player.HoldingItem);
                    break;
                case PlayerInput.OpenInventory:
                    game.CurrentMenu = new MenuPlayerInventory(Player, RECIPIES.HandCrafted, game);
                    break;
                case PlayerInput.OpenPauseMenu:
                    if (game.CurrentMenu is MenuInGame)
                    {
                        game.CurrentMenu = new MenuGamePaused(game);
                    }
                    else
                    {
                        game.CurrentMenu = new MenuInGame(game);
                    }
                    break;
                case PlayerInput.Zoom:
                    {
                        if(game.Camera.Zoom < 10)game.Camera.Zoom /= 0.9f;

                    }
                    break;
                case PlayerInput.Dzoom:
                    {
                        if(game.Camera.Zoom > 1)game.Camera.Zoom *= 0.9f;

                    }
                    break;
            }
        }
    }
}
