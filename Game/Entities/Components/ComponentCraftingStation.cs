using System.Collections.Generic;
using Hevadea.Craftings;

namespace Hevadea.Entities.Components
{
    public class ComponentCraftingStation : EntityComponent
    {
        public List<Recipe> Recipes { get; }

        public ComponentCraftingStation(List<Recipe> recipes)
        {
            Recipes = recipes;
        }
    }
}