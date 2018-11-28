using Hevadea.Entities.Components;

namespace Hevadea.Systems.CircleMenuSystem
{
	public class CircleMenu : EntityComponent
    {
        public bool Visible { get; set; }
        public int SelectedItem { get; set; }

        public CircleMenuLevel Root { get; set; }

        public CircleMenu()
        {
        }
    }
}
