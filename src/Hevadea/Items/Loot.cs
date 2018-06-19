namespace Hevadea.Items
{
    public class Drop
    {
        public Item Item;
        public float Chance;
        public int Min;
        public int Max;

        public Drop(Item item, float chance, int min, int max)
        {
            Item = item;
            Chance = chance;
            Min = min;
            Max = max;
        }
    }
}