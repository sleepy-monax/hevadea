using Maker.Hevadea.Game.Entities;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Player
{
    public class PlayerHandler
    {
        public int Index { get; private set; }
        public PlayerEntity Entity { get; private set; }
        public InputHandler Input  { get; private set; }

        public PlayerHandler(int index, PlayerEntity entity, InputHandler input)
        {
            Index = index;
            Entity = entity;
            Input = input;
        }

        public void Update(GameTime gameTime)
        {

        }

    }
}
