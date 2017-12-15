using System.Collections.Generic;

namespace WorldOfImagination.Data
{
    public class GameDataBase
    {
        public List<Classe> Classes;
        public List<Item> Items;

        public GameDataBase()
        {
            Classes = new List<Classe>();
            Items = new List<Item>();
        }
    }
}