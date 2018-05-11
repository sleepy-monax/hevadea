namespace Hevadea.GameObjects.Entities.Components.States
{
    public class Experience : EntityComponent
    {
        public int XP { get; private set; } = 0;

        public void TakeXP(int points)
        {
            XP += points;
        }
        
        public void TakeXP(EntityXpOrb orb)
        {
            TakeXP(orb.Value);
        }
    }
}