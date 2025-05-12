using DuanWu.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace DuanWu.Content.System
{
    internal class DuanWuGlobalNPCs:GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (DuanWuPlayer.SetSpwanRate)
            {
                spawnRate = (int)(spawnRate * 0.1);
                maxSpawns = (int)(maxSpawns * 10f);
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZongYe>(), 50));
            npcLoot.Add(ItemDropRule.OneFromOptions(50, ModContent.ItemType<ZongZi>(), ModContent.ItemType<AlkaliWaterzongzi>(), ModContent.ItemType<SaltedDuckzongzi>(), ModContent.ItemType<MeatZongzi>(), ModContent.ItemType<CandiedDateZongzi>(), ModContent.ItemType<PurpleRiceZongzi>(), ModContent.ItemType<BambooZongzi>()));
        }

    }
}
