namespace WorldOfImagination.Data
{   
    public class Item
    {
        public string Name        { get; set; }
        public string Description { get; set; }
        public string IconName    { get; set; }
        
        public int    StackSize   { get; set; }
        public float  Weight      { get; set; }
        public int    Cost        { get; set; }
    }
}