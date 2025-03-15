using DuanWu.Content.Utilities;
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
using DuanWu.Content.Buffs;
namespace DuanWu
{
    public class DuanWuPlayer : ModPlayer
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
        public string[] LisaoChoiceText = new string[8];
        public int ShowAnswer;
        public static bool Hardmode;
        public static bool FullText;
        public static bool RandResults;
        public bool ScreenShakeUpDown;
        public static bool ScreenZhuan = true;
        private Vector2 screenCache;

        public bool ShowPlayHitBox;
        public bool ShowNPCHitBox;
        public bool ShowProjectileBox;
        public bool ForverShowPlayHitBox;
        public bool ForverShowProjectileBox;
        public bool ForverShowNPCHitBox;
        public int SetLifeMax2;
        public int SetLifemana2;
        public float Setmovespeed;
        public float Setmovespeed1=1;
        /// <summary>
        /// 减伤
        /// </summary>
        public int Setendurance;
        /// <summary>
        /// 召唤物上限
        /// </summary>
        public int SetMinions;
        public int SetDefense;
        public bool Fly;
        public bool SetMana;
        public Vector2 Screenpos;
        public bool StartScreenpos;
        public static bool SetSpwanRate;
        public override void PostUpdate()
        {
            //OtherQusetionAvtive();
            if (LisaoActive)
            {
                if (counttime == 0)
                {
                    LanguageHelper.CheckAnswer();
                }
                counttime--;
                ShowAnswer--;
                if (ShowAnswer == -1)
                {
                    LanguageHelper.EndQnestion();
                }
            }


            foreach (Player player in Main.ActivePlayers)
            {
                if (player.HasBuff(ModContent.BuffType<Sun>()))
                {
                    Lighting.AddLight(player.Center, new Vector3(100, 100, 100));
                    player.detectCreature = true;
                    player.dangerSense = true;
                    player.findTreasure = true;
                }
            }

            //if (KeepQuestionActive)
            //{
            //    LanguageHelper.SetQuestion();
            //}

        }


        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Sun>()))
            {
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
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
            Player.statDefense += SetDefense;
            Player.maxMinions += SetMinions;
            Player.endurance += Setendurance;
            Player.moveSpeed += Setmovespeed;
            Player.moveSpeed *= Setmovespeed1;
            if (Fly)
            {
                Player.wingTime = Player.wingTimeMax;
            }
            if (SetMana)
            {
                Player.statMana = Player.statManaMax2;
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                modifiers.FinalDamage *= 1.5f;
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                modifiers.FinalDamage *= 0.5f;
            }
        }

        public override void ModifyScreenPosition()
        {
            if (ScreenShakeUpDown)
            {
                float _ = Math.Min(100, Player.statLifeMax2 / Player.statLife + 1);
                Main.screenPosition = new Vector2(Main.LocalPlayer.Center.X - Main.screenWidth / 2, (float)(Main.LocalPlayer.Center.Y - Main.screenHeight / 2 + _ * 5f * Math.Sin(0.31415 * Main.time)));
            }

            if (StartScreenpos)
            {

                if (Main.LocalPlayer.Center.X - Screenpos.X < Main.screenWidth)
                {
                    Screenpos.X -= Main.screenWidth;
                }
                if (Main.LocalPlayer.Center.Y - Screenpos.Y < Main.screenHeight)
                {
                    Screenpos.Y -= Main.screenHeight;
                }
                if (Main.LocalPlayer.Center.X - Screenpos.X > Main.screenWidth)
                {
                    Screenpos.X += Main.screenWidth;
                }
                if (Main.LocalPlayer.Center.Y - Screenpos.Y > Main.screenHeight)
                {
                    Screenpos.Y += Main.screenHeight;
                }
                screenCache = Vector2.Lerp(screenCache, Screenpos, 0.1f);
                Main.screenPosition = screenCache;
            }
        }

        #region 受击时
        public override void OnHurt(Player.HurtInfo info)
        {

            if (info.Damage >= 250 && info.Damage < Player.statLife)
            {
                LanguageHelper.SetQuestion();
            }
            else if (info.Damage >= 100)
            {
                if (Main.rand.Next(0, 4) == 1)
                {
                    LanguageHelper.SetQuestion();
                }
            }
            else
            {
                if (Main.rand.Next(0, 16) == 1)
                {
                    LanguageHelper.SetQuestion();
                }
            }
        }
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
