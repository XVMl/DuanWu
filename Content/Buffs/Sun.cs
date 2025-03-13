using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DuanWu.Content.Buffs
{
    /// <summary>
    /// 太阳，增伤30%，减伤30%，不死，显示陷阱，宝藏。
    /// </summary>
    internal class Sun:ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            rare = ItemRarityID.Purple;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lavaImmune = true;
            
        }
    }
}
