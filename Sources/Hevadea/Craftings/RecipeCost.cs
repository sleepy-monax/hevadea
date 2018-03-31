using Hevadea.Items;

namespace Hevadea.Craftings
{
    public class RecipeCost
    {
        public RecipeCost(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public Item Item { get; }
        public int Count { get; }
    }
}