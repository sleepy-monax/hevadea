using System;
using Hevadea.Entities;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.PlayerSystem
{
    public class PlayerInputProcessor : EntityUpdateSystem
    {
        public const float PLAYER_MOVE_SPEED = 1f;

        public PlayerInputProcessor()
        {
            Filter.AllOf(typeof(PlayerBody));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var g = entity.GameState;
            var i = Rise.Input;
            var m = entity.GetComponent<Move>();


        }
    }
}
