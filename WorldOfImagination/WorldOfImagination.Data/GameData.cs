namespace WorldOfImagination.Data
{
    enum DataType
    {
        Item,
        Classe,
        Skill,
        
    }
    public class GameData
    {
        public string Name { get; set; } = "null"; // Like stick
        public string LocalizedName { get; set; } = "null"; // Like item.stick.name
        public string Description { get; set; } = "null";   // Like item.stick.description
        public string Notes { get; set; } = "null"; // not localized this just for DEVs
        public string DataType { get; set; } = "null"; 
        // The type of the data... 
        // I think it will be better if it is a enum but string is fine 
        // TODO make datatype a enum.
    }
}