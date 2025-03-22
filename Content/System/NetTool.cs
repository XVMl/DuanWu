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
using DuanWu.Content.Utilities;

namespace DuanWu.Content.System
{
    public abstract class NetTool : ModSystem
    {
        public virtual void RecievePacket(BinaryReader reader, int sender) { }
        public virtual void SendPacket(BinaryWriter writer) { }

        public void NetSeed(int toClient = -1, int ignoreClient = -1)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
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
    public class NetTime : NetTool
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

    internal class NetScoreboard : NetTool
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
                //Main.NewText(num);
                var manager = new RecordManager();
                Scoreboard.UIGrid.Clear();
                for (int i = 0; i < num; i++)
                {
                    string name = reader.ReadString();
                    int numberofquestions = reader.ReadInt32();
                    int corrects = reader.ReadInt32();
                    manager.AddOrUpdate(name, corrects, numberofquestions);
                }
                foreach (var record in manager.GetSortedRecords())
                {
                    Scoreboard.UIGrid.Add(new ScoreboardElement(record.Name, record.Accuracy, record.CorrectCount));
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

    internal class Netsponse : NetTool
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime = 0;
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                base.NetSeed(-1, sender);
            }
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            Netsponse netQuickresponse = new Netsponse();
            netQuickresponse.RecievePacket(reader, sender);
        }
    }

    internal class NetSpawnRate : NetTool
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

    internal class NetProjectlies : NetTool
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

    internal class NetNPC : NetTool
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

    internal class ServeSetQustion : NetTool
    {
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && DuanWuPlayer.Quickresponse)
            {
                int ans = reader.ReadInt32();
                int lisaoquestion = reader.ReadInt32();
                List<int> nums = [];
                DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
                duanWuPlayer.Answer = ans;
                for (int i = 0; i < 8; i++)
                {
                    nums.Add(reader.ReadInt32());
                }
                for (int i = 0; i < 8; i++)
                {
                    duanWuPlayer.LisaoChoiceText[i] = LanguageHelper.GetQuestionTextValue(nums[(i + 8 - ans) % 8], (lisaoquestion + 1) % 2);
                }
                duanWuPlayer.LisaoActive = true;
                duanWuPlayer.ChoiceAnswer = -1;
                duanWuPlayer.counttime = DuanWuPlayer.AnswerQuestionTime * 60;
                duanWuPlayer.LisaoQuestionText = LanguageHelper.GetQuestionTextValue(nums[0], lisaoquestion);
                duanWuPlayer.QuestionAnswer = LanguageHelper.GetQuestionTextValue(nums[0], (lisaoquestion + 1) % 2);
            }
        }

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            ServeSetQustion serveSetQustion = new ServeSetQustion();
            serveSetQustion.RecievePacket(reader, sender);
        }
    }

    #region 记分储存

    internal class Record : IComparable<Record>
    {

        public string Name { get; }
        public float Accuracy { get; }
        public int CorrectCount { get; }

        public Record(string name, float accuracy, int correctCount)
        {
            Name = name;
            Accuracy = accuracy;
            CorrectCount = correctCount;
        }
        // 实现比较接口用于排序
        public int CompareTo(Record other)
        {
            // 降序排列
            return other.Accuracy.CompareTo(this.Accuracy);
        }
    }

    internal class RecordManager
    {
        private readonly Dictionary<string, Record> _records = new();

        // 添加或更新记录
        public void AddOrUpdate(string name, float accuracy, int correctCount)
        {
            _records[name] = new Record(name, accuracy, correctCount);
        }

        // 获取按正确率降序排列的记录
        public IEnumerable<Record> GetSortedRecords()
        {
            return _records.Values.OrderByDescending(x => x.Accuracy);
        }

    }
    #endregion


}
