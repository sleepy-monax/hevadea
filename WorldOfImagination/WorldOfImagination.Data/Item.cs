namespace WorldOfImagination.Data
{   
    public class Item : GameData
    {
        public string IconName    { get; set; }
        public int    StackSize   { get; set; }
        public float  Weight      { get; set; }
        public int    Cost        { get; set; }

        public Item()
        {
            DataType = nameof(Item);
        }
    }
}