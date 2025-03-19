using DuanWu.Content.System;
using DuanWu.Content.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DuanWu.Content.Utilities
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
            else
            {
                return Language.GetTextValue("Mods.DuanWu.ChoiceQuestion.Excerpt."+id.ToString()+".Question."+n.ToString());
            }
        }


        public static List<int> GetUniqueRandomNumbers(int count, int min, int max)
        {
            HashSet<int> result = new HashSet<int>();
            while (result.Count < count)
            {
                result.Add(Main.rand.Next(min, max + 1));
            }
            return new List<int>(result);
        }

        public static void SetQuestion()
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (!duanWuPlayer.LisaoActive)
            {
                duanWuPlayer.LisaoActive = true;
            }
            else
            {
                return;
            }
            List<int> nums;
            int ans;
            int conunts;
            int t;
            duanWuPlayer.counttime = DuanWuPlayer.AnswerQuestionTime * 60;
            if (DuanWuPlayer.FullText)
            {
                t = 186;
            }
            else
            {
                t = 37;
            }

            if (DuanWuPlayer.Hardmode)
            {
                nums = LanguageHelper.GetUniqueRandomNumbers(8, 0, t);
                ans = Main.rand.Next(0, 8);
                duanWuPlayer.Answer = ans;
                conunts = 8;
            }
            else
            {
                nums = LanguageHelper.GetUniqueRandomNumbers(4, 0, t);
                ans = Main.rand.Next(0, 4);
                duanWuPlayer.Answer = ans;
                conunts = 4;
            }
            duanWuPlayer.ChoiceAnswer = -1;
            duanWuPlayer.lisaoquestion = Main.rand.Next(2);
            int n = duanWuPlayer.lisaoquestion == 0 ? 1 : 0;
            duanWuPlayer.LisaoQuestionText = LanguageHelper.GetQuestionTextValue(nums[0], duanWuPlayer.lisaoquestion);
            duanWuPlayer.QuestionAnswer = LanguageHelper.GetQuestionTextValue(nums[0], n);
            for (int i = 0; i < 8; i++)
            {
                duanWuPlayer.LisaoChoiceText[i] = LanguageHelper.GetQuestionTextValue(nums[(i + conunts - ans) % conunts], n);
            }

        }

        public static void CheckAnswer()
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.Answer==duanWuPlayer.ChoiceAnswer)
            {
                string s = Language.GetTextValue("Mods.DuanWu.Judging.Success");
                Main.NewText(s,Color.Green);
                duanWuPlayer.Reward = true;
                RewardSystem reward = new(1);
                DuanWuPlayer.PlayerQuestionEnd = true;
                ModContent.GetInstance<Netsponse>().NetSeed(-1, -1);
            }
            else
            {
                string s = Language.GetTextValue("Mods.DuanWu.Judging.Fail");
                Main.NewText(s,Color.Red);
                duanWuPlayer.Reward = false;
                PenaltySystem penaltySystem = new(1);
            }
            duanWuPlayer.ShowAnswer = 180;
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
