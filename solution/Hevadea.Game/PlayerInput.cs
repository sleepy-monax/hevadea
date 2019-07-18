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


    }
}