using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace DuanWu.Content.System
{
    internal class NetSpawnRate : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            DuanWuPlayer.SetSpwanRate=reader.ReadBoolean();
            if (Main.netMode==2)
            {
                NetSeed(-1,sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(true);
        }
    }
}
