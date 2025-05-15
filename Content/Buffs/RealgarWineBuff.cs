using Luminance.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace DuanWu.Content.Buffs
{
    internal class RealgarWineBuff:ModBuff
    {
        public override string Texture => "DuanWu/Assets/WineBuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DuanWuPlayer>().RealgarWineBuff = true;
        }
    }
}
