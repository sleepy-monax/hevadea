using Hevadea.Entities.Components;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenu : EntityComponent
    {
        CircleMenuLevel _current;

        public bool Visible { get; set; }
        public int SelectedItem { get; set; }
        public float Animation { get; set; }

        public CircleMenuLevel Root { get; set; }
        public CircleMenuLevel Current { get => _current ?? Root; set => _current = value; }

        public CircleMenu()
        {
        }
    }
}
