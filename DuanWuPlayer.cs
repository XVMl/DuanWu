﻿using DuanWu.Content.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
namespace DuanWu
{
    public class DuanWuPlayer:ModPlayer
    {

        public int Answer;
        /// <summary>  
        /// 只在SetQuestion()时乘以帧数60  
        /// </summary>  
        public static int AnswerQuestionTime;
        public int ChoiceAnswer;
        public bool LisaoActive;
        public bool LisaoChoiceActive = true;
        /// <summary>  
        /// 上文还是下文  
        /// </summary>  
        public int counttime;
        public int QuestionCount;
        public string QuestionAnswer = "";
        public bool KeepQuestionActive;
        public int lisaoquestion;
        public bool? Reward;
        public string LisaoQuestionText = "";
        public string LisaoChoiceText1 = "";
        public string LisaoChoiceText2 = "";
        public string LisaoChoiceText3 = "";
        public string LisaoChoiceText4 = "";
        public string LisaoChoiceText5 = "";
        public string LisaoChoiceText6 = "";
        public string LisaoChoiceText7 = "";
        public string LisaoChoiceText8 = "";
        public int ShowAnswer;
        public static bool Hardmode;
        public static bool FullText;
        public static bool RandResults;
        public bool ScreenShakeUpDown;
        public static bool ScreenZhuan=true;
        private Vector2 screenCache;

        public bool ShowPlayHitBox;
        public bool ShowNPCHitBox;
        public bool ShowProjectileBox;
        public bool ForverShowPlayHitBox;
        public bool ForverShowProjectileBox;
        public bool ForverShowNPCHitBox;
        public int SetLifeMax2;
        public int SetLifemana2;
        public int Setmovespeed;
        public int Setendurance;
        public int SetMinions;
        public bool Fly;
        public bool SetMana;
        public override void PostUpdate()
        {
            //OtherQusetionAvtive();
            if (LisaoActive)
            {
                if (counttime==0)
                {
                    LanguageHelper.CheckAnswer();   
                }
                counttime--;
                ShowAnswer--;
                if (ShowAnswer==-1)
                {
                    LanguageHelper.EndQnestion();
                }
            }

            //if (KeepQuestionActive)
            //{
            //    LanguageHelper.SetQuestion();
            //}

        }

        public override void UpdateDead()
        {
            if (Filters.Scene["DuanWuShader:Zhuan"].IsActive())
            {
                Filters.Scene["DuanWuShader:Zhuan"].Deactivate();
            }
            if (Filters.Scene["DuanWuShader:Zhuan"].IsActive())
            {
                Filters.Scene["DuanWuShader:Zhuan"].Deactivate();
            }
            if (Filters.Scene["DuanWuShader:Pixelation"].IsActive())
            {
                Filters.Scene["DuanWuShader:Pixelation"].Deactivate();
            }
            if (!ForverShowProjectileBox)
            {
                ShowProjectileBox = false;
            }
            if (!ForverShowPlayHitBox)
            {
                ShowPlayHitBox = false;
            }
            if (!ForverShowNPCHitBox)
            {
                ShowNPCHitBox = false;
            }
            KeepQuestionActive = false;
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            return base.ConsumableDodge(info);
        }

        public override void PostUpdateEquips()
        {
            Player.statLifeMax2 += SetLifeMax2;
            Player.statManaMax2 += SetLifemana2;
            Player.maxMinions += SetMinions;
            Player.endurance += Setendurance;
            Player.moveSpeed += Setmovespeed;
            if (Fly)
            {
                Player.wingTime = Player.wingTimeMax;
            }
            if (SetMana)
            {
                Player.statMana = Player.statManaMax2;
            }
            
        }

        public override void ModifyScreenPosition()
        {
            if (ScreenShakeUpDown)
            {
                Vector2 centerScreen;
            centerScreen=new Vector2((Main.screenWidth / 2), (Main.screenHeight / 2));
            screenCache = Vector2.Lerp(screenCache, new Vector2(Main.LocalPlayer.Center.X, Main.LocalPlayer.Center.Y + 200f) - centerScreen, 0.1f);
            //Main.screenPosition = this.screenCache;
            float _ =Math.Min(100, Player.statLifeMax2 / Player.statLife+1);
                
            Main.screenPosition = new Vector2(Main.LocalPlayer.Center.X-Main.screenWidth/2, (float)(Main.LocalPlayer.Center.Y-Main.screenHeight/2 +_*5f*Math.Sin(0.31415*Main.time)));
            }
            

        }

        #region 受击时
        //public override void OnHurt(Player.HurtInfo info)
        //{
           
        //    if (info.Damage>=250&&info.Damage< Player.statLife)
        //    {
        //        LanguageHelper.SetQuestion(info.);
        //    }
        //    else if (info.Damage>=100)
        //    {
        //        if (Main.rand.Next(0, 4) == 1)
        //        {
        //            LanguageHelper.SetQuestion();
        //        }
        //    }
        //    else
        //    {
        //        if (Main.rand.Next(0, 16) == 1)
        //        {
        //            LanguageHelper.SetQuestion();
        //        }
        //    }
        //}
        #endregion

        #region 召唤特定NPC时
        private void NPCQuestionActive()
        {
            
        }
        #endregion

        #region 其他
        //private void OtherQusetionAvtive()
        //{
        //    if (Player.breath<=0)
        //    {
        //        LanguageHelper.SetQuestion();
        //    }
        //}
        #endregion
    }
}
