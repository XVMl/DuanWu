using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    internal class DuanWuGlobalNPCs:GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (DuanWuPlayer.SetSpwanRate)
            {
                spawnRate = 0;
                maxSpawns = (int)(maxSpawns * 100f);
            }
        }
    }
}
