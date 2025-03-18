using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace DuanWu.Content.System
{
    internal class Netsponse : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.NewText("!!!");
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime = 0;
            
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                base.NetSeed(-1, sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {

            Netsponse netQuickresponse = new Netsponse();
            netQuickresponse.RecievePacket(reader, sender);
        }
    }
}
