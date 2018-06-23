using Hevadea.Entities;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
	public enum GameSystemType 
	{
		Render, Logic
	}

    public interface IProcessSystem
    {
        void Process(Entity entity, GameTime gameTime);
    }

    public interface IRenderSystem
    {
        void Render(Entity entity, LevelSpriteBatchPool pool,  GameTime gameTime);
    }

    public class GameSystem 
    {
        public bool Enable { get; set; } = true;
		public GameSystemType Type { get; set; } = GameSystemType.Logic;
		public Filter Filter { get; set; } = new Filter();
    }
}
