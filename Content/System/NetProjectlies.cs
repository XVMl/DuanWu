using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace DuanWu.Content.System
{
    internal class NetProjectlies : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            int type = reader.ReadInt32();
            Vector2 pos = reader.ReadVector2();
            
            Projectile.NewProjectile(null, pos, Vector2.Zero, type, 99, 1);
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                Main.mouseX = (int)pos.X;
                Main.mouseY = (int)pos.Y;
                base.NetSeed(-1, sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(PenaltySystem.SelectProjectliesID);
            writer.WriteVector2(Main.MouseWorld);
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            NetProjectlies packet = new NetProjectlies();
            packet.RecievePacket(reader, sender);
        }
    }
}
