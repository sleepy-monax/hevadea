using Maker.Hevadea.Game.Items;

namespace Maker.Hevadea.Game.Craftings
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