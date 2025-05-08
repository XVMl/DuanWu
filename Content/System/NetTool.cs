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
using DuanWu.Content.MyUtilities;
using static System.Net.Mime.MediaTypeNames;
using Luminance.Common.Utilities;
using Terraria.ModLoader.IO;

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
                SendPacket(-1, sender);
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
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.LisaoActive)
            {
                if (duanWuPlayer.ShowAnswer > 0)
                {
                    return;
                }
                else
                {
                    LanguageHelper.CheckAnswer();
                    LanguageHelper.EndQnestion();
                }
            }
            DuanWuPlayer.WaitingForQuestionEnd = false;
            if (Main.netMode == 2)
            {
                Time = -1;
                SendPacket(-1, sender);
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
                string type = reader.ReadString();
                ModPacket packet = ModContent.GetInstance<DuanWu>().GetPacket();
                if (type == "Adjust")
                {
                    packet.Write(Name);
                    packet.Write("Increase");
                    string name =reader.ReadString();
                    int rate = reader.ReadInt32();
                    List<int> score = [];
                    List<string> player = [];
                    foreach (var item in correct)
                    {
                        if (!item.Key.Equals(name)&&(Main.rand.Next(1,2)+rate>1))
                        {
                            player.Add(item.Key);
                            score.Add(item.Value);
                        }
                    }
                    packet.Write(score.Count);
                    foreach (var item in score)
                    {
                        packet.Write(item);
                    }
                    packet.Send(sender);
                    ModPacket packet1 = ModContent.GetInstance<DuanWu>().GetPacket();
                    packet1.Write(Name);
                    packet1.Write("Reduce");
                    packet1.Write(player.Count);
                    foreach (var item in player)
                    {
                        packet1.Write(item);
                    }
                    packet1.Send(-1,sender);
                }
                else if (type == "Normal")
                {
                    string name = reader.ReadString();
                    int corrects = reader.ReadInt32();
                    int count = reader.ReadInt32();
                    if (!correct.TryAdd(name, corrects))
                    {
                        correct[name] = corrects;
                    }
                    if (!numberofquestion.TryAdd(name, count))
                    {
                        numberofquestion[name] = count;
                    }
                    packet.Write(Name);
                    packet.Write("NormalClient");
                    packet.Write(correct.Keys.Count);
                    for (int i = 0; i < correct.Keys.Count; i++)
                    {
                        packet.Write(correct.Keys.ElementAt(i));
                        packet.Write(correct[correct.Keys.ElementAt(i)]);
                        packet.Write(numberofquestion[numberofquestion.Keys.ElementAt(i)]);
                    }
                    packet.Send(-1, -1);
                }
            }
            else
            {
                string type = reader.ReadString();
                if (type == "Reduce")
                {
                    int num = reader.ReadInt32();
                    for (int i = 0; i < num; i++)
                    {
                        if (Main.LocalPlayer.name.Equals(reader.ReadString()))
                        { 
                            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy = 0;
                            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount = 0;
                        }
                    }
                    SubmitPacket();
                }
                else if (type == "Increase")
                {
                    int num = reader.ReadInt32();
                    for (int i = 0; i < num; i++)
                    {
                        Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy +=reader.ReadInt32();
                    }
                    SubmitPacket();
                }
                else if (type== "NormalClient")
                {
                    int num = reader.ReadInt32();
                    RecordManager recordManager = new RecordManager();
                    for (int i = 0; i < num; i++)
                    {
                        string name = reader.ReadString();
                        float corrects = reader.ReadInt32();
                        int numberofquestions = reader.ReadInt32();
                        recordManager.AddOrUpdate(name, corrects, numberofquestions);
                    }
                    Scoreboard.CaleElement(num, recordManager._records);
                    int count = 0;
                    foreach (var record in recordManager.GetSortedRecords())
                    {
                        Scoreboard.TryUpdata(record.Name, record.Accuracy, record.CorrectCount, count++);
                        //Scoreboard.UIGrid.Add(new ScoreboardElement(record.Name, record.Accuracy, record.CorrectCount));
                    }
                    Scoreboard.CalcBox();
                }
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
            writer.Write("Normal");
            writer.Write(Main.LocalPlayer.name);
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy);
            writer.Write(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount);
            writer.Send(-1, -1);
        }
    }

    internal class NetSpawnRate : NetTool
    {
        public override string TypeName => Name;
        public override void RecievePacket(BinaryReader reader, int sender)
        {
            //DuanWuPlayer.SetSpwanRate = reader.ReadBoolean();
            DuanWuPlayer.SetSpwanRate=true;
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
            int damage = reader.ReadInt32();
            Utilities.NewProjectileBetter(null, pos, Vector2.Zero, type, damage, 0);
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
            writer.Write(PenaltySystem.SelectProjectilesDamage);
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
            if (Main.netMode == NetmodeID.Server && sender >= 0)
            {
                SendPacket(-1, sender);
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
            //服务端设置题目
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
            //客户端接收题目设置
            if (Main.netMode == NetmodeID.MultiplayerClient && DuanWuPlayer.Quickresponse)
            {

                DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
                duanWuPlayer.Answer = reader.ReadInt32();
                int lisaoquestion = reader.ReadInt32();
                int ans_text = 0;
                for (int i = 0; i < 8; i++)
                {
                    int index = reader.ReadInt32();
                    if (i == duanWuPlayer.Answer)
                        ans_text = index;
                    duanWuPlayer.LisaoChoiceText[i] = LanguageHelper.GetQuestionTextValue(index, lisaoquestion);
                }
                duanWuPlayer.lisaoquestion = lisaoquestion;
                duanWuPlayer.ChoiceAnswer = -1;
                duanWuPlayer.counttime = 600;
                duanWuPlayer.LisaoQuestionText = LanguageHelper.GetQuestionTextValue(ans_text, lisaoquestion ^ 1);
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
                if (Time==-1)
                {
                    ModPacket packet = ModContent.GetInstance<DuanWu>().GetPacket();
                    packet.Write("Netsponse");
                    packet.Send(-1, -1);
                }
                if (Time == -180)
                {
                    duanWuPlayer.LisaoActive = false;
                }
            }
        }

    }

    #region 记分储存

    public class Record : IComparable<Record>
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

    public class RecordManager
    {
        public readonly Dictionary<string, Record> _records = new();

        // 添加或更新记录
        public void AddOrUpdate(string name, float accuracy, int correctCount)
        {
            _records[name] = new Record(name, accuracy, correctCount);
        }

        // 获取按正确率降序排列的记录
        public IEnumerable<Record> GetSortedRecords()
        {
            return _records.Values.OrderByDescending(r => r.Accuracy);
        }
        //public static IEnumerable<(int Rank,Record Record)> GetSortedRecords()
        //{
        //    var sorted = _records.Values
        //        .OrderByDescending(r=>r.Accuracy)
        //        .ToList();
        //    if (sorted.Count == 0)
        //    {
        //        yield break;
        //    }
        //    int currentRank = 1;
        //    float lastAccuracy = sorted[0].Accuracy;
        //    for (int i = 0; i < sorted.Count; i++)
        //    {
        //        // 当准确率变化时更新当前排名
        //        if (sorted[i].Accuracy != lastAccuracy)
        //        {
        //            currentRank = i + 1;
        //            lastAccuracy = sorted[i].Accuracy;
        //        }

        //        yield return (currentRank, sorted[i]);
        //    }

        //}

    }
    #endregion


}
