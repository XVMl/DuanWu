﻿using DuanWu.Content.UI;
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
using DuanWu.Content.MyUtilities;
using static System.Net.Mime.MediaTypeNames;
using Luminance.Common.Utilities;

namespace DuanWu.Content.System
{
    public abstract class NetTool : ModSystem
    {
        protected static int Time;
        
        public abstract string TypeName { get; }

        public static readonly Dictionary<string, Type> PacketHandlers = new();
        public override void Load()
        {
            PacketHandlers[TypeName] = GetType();
            base.Load();
        }
        public virtual void RecievePacket(BinaryReader reader, int sender) { }
        public virtual void WriterPacket(BinaryWriter writer) { }
        public void SendPacket(int toClient = -1, int ignoreClient = -1)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return;
            }
            ModPacket packet = ModContent.GetInstance<DuanWu>().GetPacket();
            packet.Write(TypeName);
            WriterPacket(packet);
            packet.Send(toClient, ignoreClient);
        }

    }
    public class NetTime : NetTool
    {
        public override string TypeName => Name;

        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.dayTime = reader.ReadBoolean();
            Main.time = reader.ReadDouble();
            if (Main.netMode == NetmodeID.Server)
            {
                base.SendPacket(-1, sender);
            }
        }

        public override void WriterPacket(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(Main.dayTime);
            writer.Write(Main.time);
        }
    }

    internal class Netsponse : NetTool
    {
        public override string TypeName => Name;
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime = 0;
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                Time = 0;
                base.SendPacket(-1, sender);
            }
        }

    }

    internal class NetScoreboard : NetTool
    {
        public override string TypeName => Name;

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
                Scoreboard.CalcBox();
            }

        }

        public override void WriterPacket(BinaryWriter writer)
        {
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount);
            writer.Write(Main.LocalPlayer.name);
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy);
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

    internal class NetSpawnRate : NetTool
    {
        public override string TypeName => Name;
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            DuanWuPlayer.SetSpwanRate = reader.ReadBoolean();
            if (Main.netMode == 2)
            {
                SendPacket(-1, sender);
            }
        }

    }

    internal class NetProjectlies : NetTool
    {
        public override string TypeName => Name;
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            int type = reader.ReadInt32();
            Vector2 pos = reader.ReadVector2();
            Utilities.NewProjectileBetter(null, pos, Vector2.Zero, type, 66,0);
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                Main.mouseX = (int)pos.X;
                Main.mouseY = (int)pos.Y;
                SendPacket(-1, sender);
            }
        }

        public override void WriterPacket(BinaryWriter writer)
        {
            writer.Write(PenaltySystem.SelectProjectliesID);
            writer.WriteVector2(Main.MouseWorld);
        }

    }

    internal class NetNPC : NetTool
    {
        public override string TypeName => Name;

        public override void RecievePacket(BinaryReader reader, int sender)
        {

            int type = reader.ReadInt32();
            Vector2 pos = reader.ReadVector2();
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, type);
            Main.NewText(sender);
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                base.SendPacket(-1, sender);
            }
        }

        public override void WriterPacket(BinaryWriter writer)
        {
            writer.Write(PenaltySystem.SelectNPCID);
            writer.WriteVector2(Main.MouseWorld);
        }

    }

    internal class ServeSetQustion : NetTool
    {
        public override string TypeName => Name;
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            if (Main.netMode == NetmodeID.Server && DuanWuPlayer.Quickresponse)
            {
                //writer.Write(text);
                //writer.Write(numberofchoise);
                string type = reader.ReadString();
                if (type == "SetQustion")
                {
                    DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
                    if (duanWuPlayer.LisaoActive)
                    {
                        return;
                    }
                    List<int> nums = LanguageHelper.GetUniqueRandomNumbers(8, 0, 37);
                    ModPacket writer = ModContent.GetInstance<DuanWu>().GetPacket();
                    writer.Write("ServeSetQustion");
                    writer.Write(Main.rand.Next(0, 8));
                    writer.Write(Main.rand.Next(2));
                    for (int i = 0; i < 8; i++)
                    {
                        writer.Write(nums[i]);
                    }
                    writer.Send(-1, -1);
                    duanWuPlayer.LisaoActive = true;
                    Time = 600;
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && DuanWuPlayer.Quickresponse)
            {
                DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
                duanWuPlayer.Answer = reader.ReadInt32();
                int lisaoquestion = reader.ReadInt32();
                int ans_text=0;
                for (int i = 0; i < 8; i++)
                {
                    int index = reader.ReadInt32();
                    if (i== duanWuPlayer.Answer)
                        ans_text = index;
                    duanWuPlayer.LisaoChoiceText[i] = LanguageHelper.GetQuestionTextValue(index, lisaoquestion);
                }
                duanWuPlayer.lisaoquestion = lisaoquestion;
                duanWuPlayer.ChoiceAnswer = -1;
                duanWuPlayer.counttime = 600;
                duanWuPlayer.LisaoQuestionText = LanguageHelper.GetQuestionTextValue(ans_text, lisaoquestion^1);
                duanWuPlayer.QuestionAnswer = LanguageHelper.GetQuestionTextValue(ans_text, lisaoquestion);
                duanWuPlayer.LisaoActive = true;
            }
        }

        public override void PostUpdateTime()
        {
            if (Main.netMode == NetmodeID.Server && DuanWuPlayer.Quickresponse)
            {
                DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
                if (duanWuPlayer.LisaoActive)
                {
                    Time--;
                }
                if (Time == -180)
                {
                    duanWuPlayer.LisaoActive = false;
                }
            }
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
