using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public abstract class Spawner :ModSystem
    {
        public abstract void RecievePacket(BinaryReader reader, int sender);
        public abstract void SendPacket(BinaryWriter writer);

        public void NetSeed(int toClient = -1, int ignoreClient = -1)
        {
            if (Main.netMode==NetmodeID.SinglePlayer)
            {
                return;
            }
            
            ModPacket packet = Mod.GetPacket();
            packet.Write(Name);
            SendPacket(packet);
            packet.Send(toClient, ignoreClient);
            
        }

    }
}
