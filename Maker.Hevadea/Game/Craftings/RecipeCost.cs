using Maker.Hevadea.Game.Items;

namespace Maker.Hevadea.Game.Craftings
{
    public class RecipeCost
    {
        public Item Item { get; private set; }
        public int Count { get; private set; }

        public RecipeCost(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}
