using Hevadea.Entities;

namespace Hevadea.Systems
{
	public enum GameSystemType 
	{
		Render, Logic
	}

    public class GameSystem 
    {
		public GameSystemType Type { get; set; } = GameSystemType.Logic;
		public Filter Filter { get; set; } = new Filter();

		public virtual void Process(Entity entity)
		{}
    }
}
