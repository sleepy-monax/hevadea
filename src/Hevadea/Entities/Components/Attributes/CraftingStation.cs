using Hevadea.Craftings;
using System.Collections.Generic;

namespace Hevadea.Entities.Components.Attributes
{
    public class CraftingStation : EntityComponent
    {
        public List<Recipe> Recipes { get; }

        public CraftingStation(List<Recipe> recipes)
        {
            Recipes = recipes;
        }
    }
}