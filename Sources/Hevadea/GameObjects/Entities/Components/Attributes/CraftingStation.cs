using System.Collections.Generic;
using Hevadea.Craftings;

namespace Hevadea.GameObjects.Entities.Components.Attributes
{
    public class CraftingStation : EntityComponent
    {
        public List<Recipe> Recipies { get; }

        public CraftingStation(List<Recipe> recipes)
        {
            Recipies = recipes;
        }
    }
}