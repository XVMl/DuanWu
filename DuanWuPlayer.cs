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
using Terraria.ModLoader.UI.Elements;
using DuanWu.Content.Items;

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
        public static bool OpenReward;
        public static bool OpenPenalty;
        public bool ScreenShakeUpDown;
        public static bool ScreenZhuan = true;
        public Vector2 screenCache;

        public static bool Quickresponse;
        public bool ShowPlayHitBox;
        public bool ShowNPCHitBox;
        public bool ShowProjectileBox;
        public bool ForverShowPlayHitBox;
        public bool ForverShowProjectileBox;
        public bool ForverShowNPCHitBox;
        public int SetLifeMax2;
        public int SetLifemana2;
        public float Setmovespeed = 1;
        public float Setmovespeed1 = 1;
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
        public float SetwingTime = 1;
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
        /// <summary>
        /// 抢答模式下同步答题结束
        /// </summary>
        public static bool WaitingForQuestionEnd;
        public static bool QustionActive;
        public static bool Scoreboard;
        public static string ScoreboardText;

        public int Cutscene;

        public bool GaussBlurActive;
        public bool CameraActive;
        public bool PixelationActive;
        public bool MagnifierActive;
        public bool MatrixActive;
        public Matrix Matrixfilter;
        public float Pixelationintensity;
        public float Cameraintensity;
        public bool confusion;
        public float hitdamage;

        public bool AlkaliWaterBuff;
        public bool BambooBuff;
        public bool CandiedDateBuff;
        public bool MeatZongziBuff;
        public bool PurpleRiceBuff;
        public bool SaltedDuckBuff;
        public bool ZongZiBuff;
        public bool RealgarWineBuff;
        public override void OnEnterWorld()
        {
            WaitingForQuestionEnd = false;
            NetScoreboard.SubmitPacket();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["PlayerQuestioncount"] = PlayerQuestioncount;
            tag["PlayerAccuracy"] = PlayerAccuracy;
        }

        public override void PostUpdateMiscEffects()
        {
            if (confusion && Main.myPlayer == Player.whoAmI)
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
            SetMagnifier();
            SetMatrix();
            if (LisaoActive)
            {
                ShowAnswer--;
                counttime--;
                if (counttime == 0 && !WaitingForQuestionEnd)
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

            if (Player.breath == 0)
            {
                if (Main.rand.Next(0, 3) == 1)
                {
                    LanguageHelper.SetQuestion();
                }
            }

        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Sun>()))
            {
                return false;
            }
            if (RealgarWineBuff)
            {
                Main.LocalPlayer.ClearBuff(ModContent.BuffType<RealgarWineBuff>());
                Main.LocalPlayer.statLife = (int)(Main.LocalPlayer.statLifeMax2 * 0.3f);
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        public override void ResetEffects()
        {
            AlkaliWaterBuff = false;
            BambooBuff = false;
            CandiedDateBuff = false;
            MeatZongziBuff = false;
            PurpleRiceBuff = false;
            SaltedDuckBuff = false;
            ZongZiBuff = false;
            RealgarWineBuff = false;
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
            CameraActive = false;
            PixelationActive = false;
            GaussBlurActive = false;
            Cameraintensity = 0;
            Pixelationintensity = 0;
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
            Player.moveSpeed *= Setmovespeed;
            Player.wingTime *= SetwingTime;
            if (Fly)
            {
                Player.wingTime = Player.wingTimeMax;
            }
            if (SetMana)
            {
                Player.statMana = Player.statManaMax2;
            }
            if (BambooBuff)
            {
                Player.statLifeMax2 += 400;
            }

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            float modify = 1f;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                modify += 0.5f;
            }
            if (RealgarWineBuff)
            {
                modify += 0.3f;
            }
            modifiers.FinalDamage *= modify;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            float modify = 1f;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                modify += 0.5f;
            }
            if (RealgarWineBuff)
            {
                modify += 0.3f;
            }
            modifiers.FinalDamage *= modify;
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            float finaldamafe = hitdamage;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                finaldamafe -= 0.5f;
            }
            if (PurpleRiceBuff)
            {
                finaldamafe -= 0.3f;
            }
            modifiers.FinalDamage *= (1 + finaldamafe);
            if (MeatZongziBuff)
            {
                Player.Heal(55);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life < damageDone)
            {
                if (Main.rand.Next(0, 32) == 1 && Main.myPlayer == Player.whoAmI)
                {
                    LanguageHelper.SetQuestion();
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            float finaldamafe = hitdamage;
            if (Player.HasBuff(ModContent.BuffType<Sun>()))
            {
                finaldamafe -= 0.5f;
            }
            if (PurpleRiceBuff)
            {
                finaldamafe -= 0.3f;
            }
            modifiers.FinalDamage *= (1 + finaldamafe);
            if (MeatZongziBuff)
            {
                Player.Heal(55);
            }
        }

        public override void UpdateLifeRegen()
        {
            if (CandiedDateBuff)
            {
                Player.lifeRegen += 20;
            }
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
                if (Main.MouseWorld.X - Main.screenPosition.X > Main.screenWidth - 5)
                {
                    Screenpos.X += 20;
                }
                if (Main.MouseWorld.Y - Main.screenPosition.Y < 5)
                {
                    Screenpos.Y -= 20;
                }
                if (Main.MouseWorld.Y - Main.screenPosition.Y > Main.screenHeight - 5)
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
            if (Main.dedServ || Main.myPlayer != Player.whoAmI)
            {
                return;
            }
            if (info.Damage >= 250 && info.Damage < Player.statLife)
            {
                LanguageHelper.SetQuestion();
            }
            else if (info.Damage >= 100)
            {
                if (Main.rand.Next(0, 12) == 1)
                {
                    LanguageHelper.SetQuestion();
                }
            }
            else
            {
                if (Main.rand.Next(0, 32) == 1)
                {
                    LanguageHelper.SetQuestion();
                }
            }
        }
        #endregion

        #region 召唤特定NPC时
        private void NPCQuestionActive()
        {
            if (NPC.AnyNPCs(4))
            {

            }
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

        public void SetMagnifier()
        {
            if (!MagnifierActive) return;
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.magnifier");
            if (!distortion.IsActive)
            {
                distortion.TrySetParameter("screenscalerevise", new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom);
                distortion.TrySetParameter("targetposition", new Vector2(0.5f, 0.5f));
                distortion.Activate();
            }
        }

        public void SetMatrix()
        {
            if (!MatrixActive) return;
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.MatrixFilter");
            if (!distortion.IsActive)
            {
                distortion.TrySetParameter("screenscalerevise", new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom);
                distortion.TrySetParameter("transmartix", Matrixfilter);
                distortion.Activate();
            }
        }

    }
}
