using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    internal class DuanWuRecipes:ModSystem
    {
        public static int AnyMeat;
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new(() => Language.GetTextValue("Mods.DuanWu.Other.Recipes"), [2889, 2890, 2891, 4340, 2892, 4274, 2893, 4362, 2894, 4482, 3564, 4419, 2895, 2015, 2016, 2019, 4838, 4839, 4840, 4841, 4842, 4843, 4844, 2017, 5312, 5313, 2123, 2122, 261, 4374, 2018]);
            AnyMeat = RecipeGroup.RegisterGroup("SomeMeat", group);
        }

    }
}
