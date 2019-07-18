using System;
using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Platform;
using Hevadea.Scenes.Menus;
using Hevadea.Systems.InventorySystem;
using Hevadea.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Systems.PlayerSystem
{
    public enum PlayerInput
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Action,
        Attack,
        Pickup,
        DropItem,
        AddWaypoint,
        ZoomIn,
        ZoomOut,

        DebugInspect
    }

    public class PlayerInputProcessor : EntityUpdateSystem
    {
        public const float PLAYER_MOVE_SPEED = 1f;

        public PlayerInputProcessor()
        {
            Filter.AllOf(typeof(ComponentPlayerBody));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var i = Rise.Input;

            if (i.KeyDown(Keys.J)) HandleInput(entity, PlayerInput.Attack);
            if (i.KeyDown(Keys.Q) != i.KeyDown(Keys.D))
            {
                if (i.KeyDown(Keys.Q)) HandleInput(entity, PlayerInput.MoveLeft);
                if (i.KeyDown(Keys.D)) HandleInput(entity, PlayerInput.MoveRight);
            }

            if (i.KeyDown(Keys.Z) != i.KeyDown(Keys.S))
            {
                if (i.KeyDown(Keys.Z)) HandleInput(entity, PlayerInput.MoveUp);
                if (i.KeyDown(Keys.S)) HandleInput(entity, PlayerInput.MoveDown);
            }

            if (i.KeyTyped(Keys.A)) HandleInput(entity, PlayerInput.DropItem);
            if (i.KeyTyped(Keys.Add) || i.KeyTyped(Keys.Up)) HandleInput(entity, PlayerInput.ZoomIn);
            if (i.KeyTyped(Keys.K)) HandleInput(entity, PlayerInput.Action);
            if (i.KeyTyped(Keys.L)) HandleInput(entity, PlayerInput.Pickup);
            if (i.KeyTyped(Keys.Subtract) || i.KeyTyped(Keys.Down)) HandleInput(entity, PlayerInput.ZoomOut);
            if (i.KeyTyped(Keys.X)) HandleInput(entity, PlayerInput.AddWaypoint);
            if (i.KeyTyped(Keys.W)) HandleInput(entity, PlayerInput.DebugInspect);
        }

        public void HandleInput(Entity player, PlayerInput input)
        {
            var game = player.GameState;
            var playerMovement = player.GetComponent<ComponentMove>();

            switch (input)
            {
                case PlayerInput.MoveLeft:
                    player.Facing = Direction.West;
                    playerMovement.Do(-1f, 0f);
                    break;

                case PlayerInput.MoveRight:
                    player.Facing = Direction.East;
                    playerMovement.Do(1f, 0f);
                    break;

                case PlayerInput.MoveUp:
                    player.Facing = Direction.North;
                    playerMovement.Do(0f, -1f);
                    break;

                case PlayerInput.MoveDown:
                    player.Facing = Direction.South;
                    playerMovement.Do(0f, 1f);
                    break;

                case PlayerInput.Action:
                    if (player.GetComponent<Inventory>().Content.Count(player.HoldedItem()) == 0)
                        player.HoldItem(null);

                    player.GetComponent<ComponentInteract>().Do(player.HoldedItem());
                    break;

                case PlayerInput.Attack:
                    player.GetComponent<ComponentAttack>().Do(player.HoldedItem());
                    break;

                case PlayerInput.Pickup:
                    player.GetComponent<ComponentPickup>().Do();
                    break;

                case PlayerInput.DropItem:
                    var level = player.Level;
                    var item = player.HoldedItem();
                    var facingTile = player.FacingCoordinates;
                    player.GetComponent<Inventory>().Content.DropOnGround(level, item, facingTile, 1);

                    break;

                case PlayerInput.ZoomIn:
                    if (game.Camera.Zoom < 8) game.Camera.Zoom /= 0.8f;
                    break;

                case PlayerInput.ZoomOut:
                    if (game.Camera.Zoom > 2) game.Camera.Zoom *= 0.8f;
                    break;

                case PlayerInput.AddWaypoint:
                    game.CurrentMenu = new MenuAddMinimapWaypoint(game);

                    break;

                case PlayerInput.DebugInspect:
                    player.Inspect();
                    break;
            }
        }
    }
}