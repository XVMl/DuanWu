using DuanWu.Content.MyUtilities;
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
using DuanWu.Content.System;
using DuanWu.Content.UI;
using Luminance.Core.Graphics;
using Luminance.Common.Utilities;
using Terraria.ModLoader.IO;
namespace DuanWu
{
    public class DuanWuPlayer : ModPlayer
    {
        /// <summary>
        /// 设置答案选项
        /// </summary>
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
        public int lisaoquestion;
        public int counttime;
        /// <summary>
        /// 展示的答案
        /// </summary>
        public string QuestionAnswer = "";
        public bool KeepQuestionActive;
        public bool? Reward;
        /// <summary>
        /// 设置题目文本
        /// </summary>
        public string LisaoQuestionText = "";
        /// <summary>
        /// 设置选项文本
        /// </summary>
        public string[] LisaoChoiceText = new string[8];
        public int ShowAnswer;
        public static bool Hardmode;
        public static bool FullText;
        public static bool RandResults;
        public bool ScreenShakeUpDown;
        public static bool ScreenZhuan = true;
        private Vector2 screenCache;

        public static bool Quickresponse;
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
        public bool FreeScreen;

        public static bool SetSpwanRate;
        public static bool PlayerQuestionEnd;
        /// <summary>
        /// 总回答题数
        /// </summary>
        public int PlayerQuestioncount;
        /// <summary>
        /// 回答正确数
        /// </summary>
        public int PlayerAccuracy;
        public static bool QustionActive;

        public int Cutscene;

        public bool GaussBlurActive;
        public bool CameraActive;
        public bool PixelationActive;
        public float Pixelationintensity;
        public float Cameraintensity;
        public bool confusion;
        public float hitdamage;
        public override void OnEnterWorld()
        {
            NetScoreboard.SubmitPacket();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["PlayerQuestioncount"] = PlayerQuestioncount;
            tag["PlayerAccuracy"]= PlayerAccuracy;
        }

        public override void PostUpdateMiscEffects()
        {
            if (confusion&&Main.myPlayer == Player.whoAmI)
            {
                int x = Main.rand.Next(0, 49);
                int y = Main.rand.Next(0, 49);
                (Player.inventory[y], Player.inventory[x]) = (Player.inventory[x], Player.inventory[y]);
            }

        }
        public override void LoadData(TagCompound tag)
        {
            PlayerAccuracy = tag.GetInt("PlayerAccuracy");
            PlayerQuestioncount = tag.GetInt("PlayerQuestioncount");
        }

        public override void PostUpdate()
        {
            SetBlur();
            SetPixelation();
            SetCamera();

            //OtherQusetionAvtive();
            if (LisaoActive)
            {
                counttime--;
                ShowAnswer--;
                if (counttime == -1)
                {
                    LanguageHelper.CheckAnswer();
                }
                if (ShowAnswer == 0)
                {
                    LanguageHelper.EndQnestion();
                }
            }

            foreach (Player player in Main.ActivePlayers)
            {
                if (!player.HasBuff(ModContent.BuffType<Sun>()))
                {
                    break;
                }
                Lighting.AddLight(player.Center, new Vector3(100, 100, 100));
                player.detectCreature = true;
                player.dangerSense = true;
                player.findTreasure = true;
            }

            if (QustionActive)
            {
                LanguageHelper.SetQuestion();
                QustionActive = false;
            }

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
            CameraActive= false;
            PixelationActive= false;
            GaussBlurActive= false;
            Cameraintensity = 0;
            Pixelationintensity= 0;
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
            float finaldamafe= hitdamage;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                finaldamafe += 0.5f;   
            }
            modifiers.FinalDamage *= (1 + finaldamafe );
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            float finaldamafe = hitdamage;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                finaldamafe += 0.5f;
            }
            modifiers.FinalDamage *= (1 + finaldamafe);
        }

        public override void ModifyScreenPosition()
        {
            if (ScreenShakeUpDown)
            {
                float _ = Math.Min(100, Player.statLifeMax2 / Player.statLife + 1);
                Main.screenPosition = new Vector2(Main.LocalPlayer.Center.X - Main.screenWidth / 2, (float)(Main.LocalPlayer.Center.Y - Main.screenHeight / 2 + _ * 5f * Math.Sin(0.31415 * Main.time)));
            }
            if (FreeScreen)
            {
                if (Main.MouseWorld.X - Main.screenPosition.X < 5)
                {
                    Screenpos.X -= 20;    
                }
                if (Main.MouseWorld.X - Main.screenPosition.X > Main.screenWidth-5)
                {
                    Screenpos.X += 20;
                }
                if (Main.MouseWorld.Y - Main.screenPosition.Y < 5)
                {
                    Screenpos.Y -= 20;
                }
                if (Main.MouseWorld.Y - Main.screenPosition.Y > Main.screenHeight-5)
                {
                    Screenpos.Y += 20;
                }
                screenCache = Vector2.Lerp(screenCache, Screenpos, 0.1f);
                Main.screenPosition = screenCache;
            }


            if (StartScreenpos)
            {

                if (Main.LocalPlayer.Center.X - Screenpos.X < Main.screenWidth)
                {
                    Screenpos.X -= Main.screenWidth;
                }
                if (Main.LocalPlayer.Center.X - Screenpos.X > Main.screenWidth)
                {
                    Screenpos.X += Main.screenWidth;
                }
                if (Main.LocalPlayer.Center.Y - Screenpos.Y < Main.screenHeight)
                {
                    Screenpos.Y -= Main.screenHeight;
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
            if (Main.dedServ|| Main.myPlayer != Player.whoAmI)
            {
                return;
            }
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


        public void ServeQuickresponse()
        {
            if (!Quickresponse||LisaoActive)
            {
                return;
            }
            if (!LisaoActive)
            {
                LisaoActive = true;
            }
            Main.NewText("server");


        }


        public void SetCamera()
        {
            if (!CameraActive) return;
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Zhuan");
            if (!distortion.IsActive)
            {
                distortion.TrySetParameter("intensity", Cameraintensity);
                distortion.Activate();
            }
        }

        public void SetBlur()
        {
            if (!GaussBlurActive) return;
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.GaussBlur");
            if (!distortion.IsActive)
            {
                distortion.TrySetParameter("screenscalerevise", new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom);
                distortion.Activate();
            }
        }

        public void SetPixelation()
        {
            if (!PixelationActive) return;
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Pixelation");
            if (!distortion.IsActive)
            {
                distortion.TrySetParameter("screenscalerevise", new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom);
                distortion.TrySetParameter("intensity", Pixelationintensity);
                distortion.Activate();
            }
        }


    }
}
