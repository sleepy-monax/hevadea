namespace Hevadea.Entities.Components
{
    public class ComponentExperience : EntityComponent
    {
        public int XP { get; private set; } = 0;

        public void TakeXP(int points)
        {
            XP += points;
        }

        public void TakeXP(XpOrb orb)
        {
            if (!orb.Removed)
            {
                TakeXP(orb.Value);
                orb.Remove();
            }
        }
    }
}