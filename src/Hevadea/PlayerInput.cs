using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Utils;
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
        OpenMenu, ZoomIn, ZoomOut,

        DEBUGInspect
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
            var game = Player.GameState;
            var input = Rise.Input;
            var screenBound = Rise.Graphic.GetBound();

            if (!(game.CurrentMenu?.PauseGame ?? false))
            {
                if (game.CurrentMenu == null || !game.CurrentMenu.PauseGame)
                {
                    if (input.KeyDown(Keys.J)) HandleInput(PlayerInput.Attack);
                    if (input.KeyDown(Keys.Q) != input.KeyDown(Keys.D))
                    {
                        if (input.KeyDown(Keys.Q)) HandleInput(PlayerInput.MoveLeft);
                        if (input.KeyDown(Keys.D)) HandleInput(PlayerInput.MoveRight);
                    }
                    if (input.KeyDown(Keys.Z) != input.KeyDown(Keys.S))
                    {
                        if (input.KeyDown(Keys.Z)) HandleInput(PlayerInput.MoveUp);
                        if (input.KeyDown(Keys.S)) HandleInput(PlayerInput.MoveDown);
                    }
                    if (input.KeyTyped(Keys.A)) HandleInput(PlayerInput.DropItem);
                    if (input.KeyTyped(Keys.Add) || input.KeyTyped(Keys.Up)) HandleInput(PlayerInput.ZoomIn);
                    if (input.KeyTyped(Keys.K)) HandleInput(PlayerInput.Action);
                    if (input.KeyTyped(Keys.L)) HandleInput(PlayerInput.Pickup);
                    if (input.KeyTyped(Keys.Subtract) || input.KeyTyped(Keys.Down)) HandleInput(PlayerInput.ZoomOut);
                    if (input.KeyTyped(Keys.X)) HandleInput(PlayerInput.AddWaypoint);
                    if (input.KeyTyped(Keys.W)) HandleInput(PlayerInput.DEBUGInspect);
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

            if (input.KeyTyped(Keys.E) || input.KeyTyped(Keys.Escape)) HandleInput(PlayerInput.OpenMenu);
        }

        public void HandleInput(PlayerInput input)
        {
            var game = Player.GameState;
            var playerMovement = Player.GetComponent<Move>();

            switch (input)
            {
                case PlayerInput.MoveLeft:
                    Player.Facing = Direction.West;
                    playerMovement.Do(-1f, 0f);
                    break;

                case PlayerInput.MoveRight:
                    Player.Facing = Direction.East;
                    playerMovement.Do(1f, 0f);
                    break;

                case PlayerInput.MoveUp:
                    Player.Facing = Direction.North;
                    playerMovement.Do(0f, -1f);
                    break;

                case PlayerInput.MoveDown:
                    Player.Facing = Direction.South;
                    playerMovement.Do(0f, 1f);
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
                    var facingTile = Player.FacingCoordinates;
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
                    var pos = Player.Coordinates;
                    Player.Level.Minimap.Waypoints.Add(new MinimapWaypoint { X = pos.X, Y = pos.Y, Icon = 0 });
                    break;

                case PlayerInput.DEBUGInspect:
                    Player.Inspect();
                    break;
            }
        }
    }
}