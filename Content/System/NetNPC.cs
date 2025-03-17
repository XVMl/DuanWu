using Microsoft.Xna.Framework;
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
    internal class NetNPC : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            int type = reader.ReadInt32();
            Vector2 pos = reader.ReadVector2();
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, type);
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                Main.mouseX = (int)pos.X;
                Main.mouseY = (int)pos.Y;
                base.NetSeed(-1, sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(PenaltySystem.SelectNPCID);
            writer.WriteVector2(Main.MouseWorld);
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            NetNPC npc = new NetNPC();
            npc.RecievePacket(reader, sender);
        }

    }
}
