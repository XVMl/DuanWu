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
        public virtual void RecievePacket(BinaryReader reader, int sender) { }
        public virtual void SendPacket(BinaryWriter writer) { }

        public void NetSeed(int toClient = -1, int ignoreClient = -1)
        {
            if (Main.netMode==NetmodeID.SinglePlayer)
            {
                return;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModContent.GetInstance<DuanWu>().Logger.Info($"Sending packet for tool ({Name}) from server");
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModContent.GetInstance<DuanWu>().Logger.Info($"Sending packet for tool ({Name}) from {Main.LocalPlayer.whoAmI}");
            }

            ModPacket packet = ModContent.GetInstance<DuanWu>().GetPacket();
            packet.Write(Name);
            SendPacket(packet);
            packet.Send(toClient, ignoreClient);
            
        }

    }
}
