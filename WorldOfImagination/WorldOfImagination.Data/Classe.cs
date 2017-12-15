namespace WorldOfImagination.Data
{
    public class Classe : GameData
    {
        public int[] MeleeDamages { get; set; }
        public int[] MagicDamages { get; set; }
        public int[] XpToLevelUp  { get; set; }

        public Classe()
        {
            DataType = nameof(Classe);
        }
    }
}