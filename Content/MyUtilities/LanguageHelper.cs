using DuanWu.Content.System;
using DuanWu.Content.UI;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DuanWu.Content.MyUtilities
{
    public class LanguageHelper
    {
        /// <summary>
        /// 问答题目
        /// </summary>
        /// <param name="id">那一句</param>
        /// <param name="n">上句还是下句</param>
        /// <returns></returns>
        public static string GetQuestionTextValue(int id,int n)
        {
            if(DuanWuPlayer.FullText)
            {
                return Language.GetTextValue("Mods.DuanWu.ChoiceQuestion.FullText."+id.ToString()+".Question."+n.ToString());
            }
            return Language.GetTextValue("Mods.DuanWu.ChoiceQuestion.Excerpt."+id.ToString()+".Question."+n.ToString());
        }

        public static List<int> GetUniqueRandomNumbers(int count, int min, int max)
        {
            HashSet<int> result = [];
            while (result.Count < count)
            {
                result.Add(Main.rand.Next(min, max + 1));
            }
            return new List<int>(result);
        }

        public static void SetQuestion()
        {
            if (DuanWuPlayer.WaitingForQuestionEnd)    return;
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.LisaoActive)    return;
            if (Main.netMode == NetmodeID.MultiplayerClient && DuanWuPlayer.Quickresponse)
            {
                ModContent.GetInstance<ServeSetQustion>().SendPacket((writer) =>
                {
                    writer.Write("SetQuestion");
                }, -1, -1);
                //ModPacket writer = ModContent.GetInstance<DuanWu>().GetPacket();
                //writer.Write("ServeSetQustion");
                //writer.Write("SetQuestion");
                //writer.Send(-1, -1);
                return;
            }
            duanWuPlayer.ChoiceAnswer = -1;
            duanWuPlayer.counttime = DuanWuPlayer.AnswerQuestionTime * 60;
            duanWuPlayer.LisaoActive = true;
            int text = DuanWuPlayer.FullText ? 186 : 37;
            int numberofchoise = DuanWuPlayer.Hardmode ? 8 : 4;
            List<int> nums = GetUniqueRandomNumbers(numberofchoise, 0, text);
            int ans = Main.rand.Next(0, numberofchoise);
            duanWuPlayer.lisaoquestion = Main.rand.Next(2);
            duanWuPlayer.Answer = ans;
            duanWuPlayer.LisaoQuestionText = GetQuestionTextValue(nums[ans], duanWuPlayer.lisaoquestion^1);
            duanWuPlayer.QuestionAnswer = GetQuestionTextValue(nums[ans], duanWuPlayer.lisaoquestion);
            for (int i = 0; i < numberofchoise; i++)
            {
                duanWuPlayer.LisaoChoiceText[i] = GetQuestionTextValue(nums[i],duanWuPlayer.lisaoquestion);
            }
        }

        public static void CheckAnswer()
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (DuanWuPlayer.WaitingForQuestionEnd || !duanWuPlayer.LisaoActive)
            {
                return;
            }
            duanWuPlayer.ShowAnswer = 180;
            if (duanWuPlayer.counttime != 0)
            {
                duanWuPlayer.counttime =0;
            } 
            if (duanWuPlayer.Answer==duanWuPlayer.ChoiceAnswer)
            {
                Main.NewText(Language.GetTextValue("Mods.DuanWu.Judging.Success"), Color.Green);
                duanWuPlayer.PlayerAccuracy++;
                if (DuanWuPlayer.OpenReward)
                {
                    duanWuPlayer.Reward = true;
                    RewardSystem reward = new(Main.rand.Next(1, 5));
                }
                DuanWuPlayer.PlayerQuestionEnd = true;
                ModContent.GetInstance<Netsponse>().SendPacket((write) => write.Write("EndQuestion"), -1, -1);
                return;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && DuanWuPlayer.Quickresponse)
            {
                WaitingUI.Number = Main.rand.Next(0, 6);
                DuanWuPlayer.WaitingForQuestionEnd = true;
            }
            Main.NewText(Language.GetTextValue("Mods.DuanWu.Judging.Fail"), Color.Red);
            if (DuanWuPlayer.OpenPenalty)
            {
                duanWuPlayer.Reward = false;
                PenaltySystem penaltySystem = new(Main.rand.Next(1, 5));
            }
            duanWuPlayer.PlayerQuestioncount++;
            NetScoreboard.SubmitPacket();
        }

        public static void EndQnestion()
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            duanWuPlayer.LisaoActive = false;
            duanWuPlayer.Answer = 0;
            duanWuPlayer.ChoiceAnswer = -1;
            duanWuPlayer.Reward = null;
            DuanWuPlayer.PlayerQuestionEnd = false;
        }

    }
}
