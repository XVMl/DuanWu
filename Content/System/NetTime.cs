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
    public class NetTime : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.dayTime=reader.ReadBoolean();
            Main.time=reader.ReadDouble();
            if (Main.netMode==NetmodeID.Server)
            {
                base.NetSeed(-1,sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(OtherResults.Setday);
            writer.Write(OtherResults.SetTime);
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            NetTime netTime = new NetTime();
            netTime.RecievePacket(reader,sender);
        }


    }
}
