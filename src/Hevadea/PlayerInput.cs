using Hevadea.Framework;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.Scenes.Menus;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Hevadea
{
    public enum PlayerInput
    {
        MoveLeft, MoveRight, MoveUp, MoveDown,
        Action, Attack, Pickup, DropItem,
        AddWaypoint,
        OpenMenu, ZoomIn, ZoomOut
    }

    public class PlayerInputHandler
    {
        public Player Player;

        public PlayerInputHandler(Player player)
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
                if (game.CurrentMenu == null || !game.CurrentMenu.PauseGame)
                {
                    if (input.KeyDown(Keys.Z) != input.KeyDown(Keys.S))
                    {
                        if (input.KeyDown(Keys.Z)) HandleInput(PlayerInput.MoveUp);
                        if (input.KeyDown(Keys.S)) HandleInput(PlayerInput.MoveDown);
                    }

                    if (input.KeyDown(Keys.Q) != input.KeyDown(Keys.D))
                    {
                        if (input.KeyDown(Keys.Q)) HandleInput(PlayerInput.MoveLeft);
                        if (input.KeyDown(Keys.D)) HandleInput(PlayerInput.MoveRight);
                    }

                    if (input.KeyDown(Keys.J)) HandleInput(PlayerInput.Attack);
                    if (input.KeyPress(Keys.K)) HandleInput(PlayerInput.Action);
                    if (input.KeyPress(Keys.L)) HandleInput(PlayerInput.Pickup);
                    if (input.KeyPress(Keys.A)) HandleInput(PlayerInput.DropItem);
                    if (input.KeyPress(Keys.X)) HandleInput(PlayerInput.AddWaypoint);
                    if (input.KeyPress(Keys.Add) || input.KeyPress(Keys.Up)) HandleInput(PlayerInput.ZoomIn);
                    if (input.KeyPress(Keys.Subtract) || input.KeyPress(Keys.Down)) HandleInput(PlayerInput.ZoomOut);
                }

                if (Rise.Platform.Family == PlatformFamily.Mobile && Rise.Pointing.AreaDown(screenBound))
                {
                    var mousePositionOnScreen = Rise.Pointing.GetAreaOver(screenBound)[0].ToVector2();
                    var mousePositionInWorld = game.Camera.ToWorldSpace(mousePositionOnScreen);
                    var screenCenter = Rise.Graphic.GetCenter();

                    if (Mathf.Distance(mousePositionOnScreen.X, mousePositionOnScreen.Y, screenCenter.X, screenCenter.Y) < Math.Min(Rise.Graphic.GetHeight(), Rise.Graphic.GetWidth()) / 2)
                    {
                        Player.GetComponent<Move>().MoveTo(mousePositionInWorld.X, mousePositionInWorld.Y, 1f, true);
                    }
                }
            }

            if (input.KeyPress(Keys.E) || input.KeyPress(Keys.Escape)) HandleInput(PlayerInput.OpenMenu);
        }

        public void HandleInput(PlayerInput input)
        {
            var game = Player.Game;
            var playerMovement = Player.GetComponent<Move>();

            switch (input)
            {
                case PlayerInput.MoveLeft:
                    playerMovement.Do(-1, 0, Direction.West);
                    break;

                case PlayerInput.MoveRight:
                    playerMovement.Do(+1, 0, Direction.East);
                    break;

                case PlayerInput.MoveUp:
                    playerMovement.Do(0, -1, Direction.North);
                    break;

                case PlayerInput.MoveDown:
                    playerMovement.Do(0, +1, Direction.South);
                    break;

                case PlayerInput.Action:
                    if (Player.GetComponent<Inventory>().Content.Count(Player.HoldingItem) == 0)
                        Player.HoldingItem = null;
                    Player.GetComponent<Interact>().Do(Player.HoldingItem);
                    break;

                case PlayerInput.Attack:
                    Player.GetComponent<Attack>().Do(Player.HoldingItem);
                    break;

                case PlayerInput.Pickup:
                    Player.GetComponent<Pickup>().Do();
                    break;

                case PlayerInput.DropItem:
                    var level = Player.Level;
                    var item = Player.HoldingItem;
                    var facingTile = Player.GetFacingTile();
                    Player.GetComponent<Inventory>().Content.DropOnGround(level, item, facingTile, 1);
                    break;

                case PlayerInput.OpenMenu:
                    if (game.CurrentMenu is MenuInGame)
                        game.CurrentMenu = new PlayerInventoryMenu(game);
                    else
                        game.CurrentMenu = new MenuInGame(game);
                    break;

                case PlayerInput.ZoomIn:
                    if (game.Camera.Zoom < 8) game.Camera.Zoom /= 0.8f;
                    break;

                case PlayerInput.ZoomOut:
                    if (game.Camera.Zoom > 2) game.Camera.Zoom *= 0.8f;
                    break;

                case PlayerInput.AddWaypoint:
                    var pos = game.MainPlayer.GetTilePosition();
                    game.MainPlayer.Level.Minimap.Waypoints.Add(new MinimapWaypoint { X = pos.X, Y = pos.Y, Icon = 0 });
                    break;
            }
        }
    }
}