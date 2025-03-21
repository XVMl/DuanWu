using DuanWu.Content.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace DuanWu.Content.System
{
    internal class NetTool : Spawner
    {
    }

    public class NetTime : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.dayTime = reader.ReadBoolean();
            Main.time = reader.ReadDouble();
            if (Main.netMode == NetmodeID.Server)
            {
                base.NetSeed(-1, sender);
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
            netTime.RecievePacket(reader, sender);
        }
    }

    internal class NetScoreboardRefresh : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Scoreboard.Refresh();
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
            NetScoreboardRefresh netScoreboardRefresh = new NetScoreboardRefresh();
            netScoreboardRefresh.RecievePacket(reader, sender);
        }
    }

    internal class NetScoreboard : Spawner
    {
        public static Dictionary<string, int> correct = [];
        public static Dictionary<string, int> numberofquestion = [];
        public override void RecievePacket(BinaryReader reader, int sender)
        {

            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                int count = reader.ReadInt32();
                string name = reader.ReadString();
                int corrects = reader.ReadInt32();
                if (!correct.TryAdd(name, corrects))
                {
                    correct[name] = corrects;
                }
                if (!numberofquestion.TryAdd(name, count))
                {
                    numberofquestion[name] = count;
                }
                ModPacket packet = ModContent.GetInstance<DuanWu>().GetPacket();
                packet.Write(Name);
                packet.Write(correct.Keys.Count);
                for (int i = 0; i < correct.Keys.Count; i++)
                {
                    packet.Write(numberofquestion.Keys.ElementAt(i));
                    packet.Write(numberofquestion[numberofquestion.Keys.ElementAt(i)]);
                    packet.Write(correct[correct.Keys.ElementAt(i)]);
                }
                packet.Send(-1, -1);
            }
            else
            {
                int num = reader.ReadInt32();
                Main.NewText(num);
                for (int i = 0; i < num; i++)
                {
                    string name = reader.ReadString();
                    int corrects = reader.ReadInt32();
                    int numberofquestions = reader.ReadInt32();
                    Main.NewText(name);
                    Main.NewText(corrects);
                    Main.NewText(numberofquestions);
                }
            }

        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount);
            writer.Write(Main.LocalPlayer.name);
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy);
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            NetScoreboard netScoreboard = new NetScoreboard();
            netScoreboard.RecievePacket(reader, sender);
        }

        public static void SubmitPacket()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return;
            }
            ModPacket writer = ModContent.GetInstance<DuanWu>().GetPacket();
            writer.Write("NetScoreboard");
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount);
            writer.Write(Main.LocalPlayer.name);
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy);
            writer.Send(-1, -1);
        }
    }



    internal class Netsponse : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
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

    internal class NetSpawnRate : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            DuanWuPlayer.SetSpwanRate = reader.ReadBoolean();
            if (Main.netMode == 2)
            {
                NetSeed(-1, sender);
            }
        }

        public override void SendPacket(BinaryWriter writer)
        {
            writer.Write(true);
        }
    }


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


    internal class NetNPC : Spawner
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {

            int type = reader.ReadInt32();
            Vector2 pos = reader.ReadVector2();
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, type);
            Main.NewText(sender);
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
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
