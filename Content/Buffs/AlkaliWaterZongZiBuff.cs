using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Luminance.Assets;

namespace DuanWu.Content.Buffs
{
    internal class AlkaliWaterZongZiBuff:ModBuff
    {
        public override string Texture => "DuanWu/Assets/UI/Buff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DuanWuPlayer>().AlkaliWaterBuff=true;
        }
    }
}
