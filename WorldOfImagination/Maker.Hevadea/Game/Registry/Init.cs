using Maker.Rise;
using Maker.Rise.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Registry
{
    public static class Init
    {

        public static void InitializeRegistry()
        {
            Logger.Log("Initializing registery.");


                TILES.Initialize();
                ITEMS.Initialize();
                RECIPIES.InitializeHandCraftedRecipe();

        }
    }
}
