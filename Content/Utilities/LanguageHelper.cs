using DuanWu.Content.System;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace DuanWu.Content.Utilities
{
    public class LanguageHelper
    {
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
            if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive = true;
            }
            else
            {
                return;
            }
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            List<int> nums;
            int ans;
            int conunts;
            duanWuPlayer.counttime = DuanWuPlayer.AnswerQuestionTime * 60;
            duanWuPlayer.ShowAnswer = duanWuPlayer.counttime;
            int t;
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
            duanWuPlayer.LisaoChoiceText1 = LanguageHelper.GetQuestionTextValue(nums[(0 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText2 = LanguageHelper.GetQuestionTextValue(nums[(1 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText3 = LanguageHelper.GetQuestionTextValue(nums[(2 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText4 = LanguageHelper.GetQuestionTextValue(nums[(3 + conunts - ans) % conunts], n);

            duanWuPlayer.LisaoChoiceText5 = LanguageHelper.GetQuestionTextValue(nums[(4 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText6 = LanguageHelper.GetQuestionTextValue(nums[(5 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText7 = LanguageHelper.GetQuestionTextValue(nums[(6 + conunts - ans) % conunts], n);
            duanWuPlayer.LisaoChoiceText8 = LanguageHelper.GetQuestionTextValue(nums[(7 + conunts - ans) % conunts], n);
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
        }

    }
}
