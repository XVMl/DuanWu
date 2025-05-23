using DuanWu.Content.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace DuanWu.Content.System
{
    public class DuanWuConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        public bool LisaoQAHardmode;

        [DefaultValue(false)]
        public bool LisaoFullTextmode;

        [DefaultValue(false)]
        public bool RandResults;

        [DefaultValue(true)]
        public bool OpenReward;

        [DefaultValue(true)]
        public bool OpenPenalty;

        [DefaultValue(true)]
        public bool DuanWuMode;


        [Increment(1)]
        [DefaultValue(15)]
        [Range(1, 30)]
        [Slider]
        public int Answerquestiontime;



        public override void OnChanged()
        {
            DuanWuPlayer.Hardmode = LisaoQAHardmode;
            DuanWuPlayer.FullText = LisaoFullTextmode;
            DuanWuPlayer.AnswerQuestionTime = Answerquestiontime;
            DuanWuPlayer.RandResults = RandResults;
            DuanWuPlayer.OpenPenalty = OpenPenalty;
            DuanWuPlayer.OpenReward = OpenReward;
            DuanWuPlayer.DuanWuMode = DuanWuMode;
            if (LisaoQAHardmode&&Answerquestiontime<5)
            {
                DuanWuPlayer.AnswerQuestionTime = 5;
            }
        }

    }
}
