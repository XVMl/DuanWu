using DuanWu.Content.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using DuanWu.Content.System;

namespace DuanWu.Content.Items
{
    internal class Su_styleZongzi:ModItem
    {
        public override string Texture => "DuanWU/Content/Items/ZongZi";
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = 0;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 2;
            Item.consumable = true;
            ItemID.Sets.ItemNoGravity[Item.type] = false;
            Item.ResearchUnlockCount = 0;
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
        }

        public override bool? UseItem(Player player)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            duanWuPlayer.Reward = true;
            RewardSystem rewardSystem = new RewardSystem(2);
            duanWuPlayer.Reward = null;
            return new bool?(true);
        }
    }
}
