using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class PenaltySystem
    {

        private int Penaltylevel;

        public static int SelectNPCID;
        public static int SelectProjectliesID;
        public static Vector2 SelectNPCpos;
        public static Vector2 SelectProjectliespos;
        public PenaltySystem(int penaltylevel)
        {
            this.Penaltylevel = penaltylevel;
            SetPenalty(this.Penaltylevel);
        }

        public PenaltySystem()
        {
            SetPenalty();
        }

        private void AverageChance()
        {
            if (DuanWuPlayer.RandResults)
            {
                this.Penaltylevel = Main.rand.Next(0, 10);
            }
        }

        public void SetPenalty(int penaltylevel = 1)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.Reward != false)
            {
                return;
            }

            Player player = Main.LocalPlayer;

            AverageChance();

            if (penaltylevel == 1)
            {
                int level1 = Main.rand.Next(0, 10);
                switch (level1)
                {
                    case 0:
                        //1
                        Main.NewText("These is no penaltly");
                        break;
                    case 1:
                        //猪鲨 1
                        Vector2 v2 = player.position + Main.rand.NextVector2Circular(10, 10) * 100;
                        QuickSpawnNPC(NPCID.DukeFishron,v2);
                        break;
                    case 2:
                        //继续答题1 1
                        duanWuPlayer.QuestionCount = 1;
                        Main.NewText("da1");

                        break;
                    case 3:
                        //换皮1 1
                        player.HeldItem.type = ItemID.DirtBlock;
                        break;
                    case 4:
                        //高斯模糊 1
                        OtherResults.SetBlur();

                        break;
                    case 5:
                        //白天地牢守卫
                        Main.dayTime = true;
                        Main.time = 0;
                        QuickSpawnNPC(35, player.Center);
                        break;
                    case 6:
                        //生成雷管 1
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 _v2 = Main.rand.NextVector2Circular(16, 16);
                            QuickSpawnProjectlies(29, player.Center + _v2 * 20);
                        }
                        break;
                    case 7:
                        //生成落石


                        break;
                    case 8:
                        //减速 1


                        break;
                    case 9:
                        //地球上投 1
                        player.velocity.Y -= 200;

                        break;
                    case 10:
                        //弹跳雷管

                        break;
                    case 11:
                        //5*100坑
                        Rectangle rectangle = new Rectangle((int)Main.LocalPlayer.Center.X / 16, (int)Main.LocalPlayer.Center.Y / 16, 5, 100);
                        KillTileRectangle(rectangle);

                        break;
                    case 12:
                        //水牢
                        break;
                    case 13:
                        //火牢
                        break;
                    case 14:
                        //落石牢
                        break;
                    case 15:
                        //大头
                        break;
                    case 16:
                        //极巨化
                        break;
                    case 17:
                        //蜂巢

                        break;
                    case 18:

                        break;
                    case 19:

                        break;
                    case 20:

                        break;

                    default:

                        break;

                }
            }
            else if (penaltylevel == 2)
            {
                int level2 = Main.rand.Next(0, 10);
                switch (level2)
                {
                    case 0:
                        //继续答题5 2
                        duanWuPlayer.QuestionCount = 5;
                        Main.NewText("da5");

                        break;
                    case 1:
                        //右转90°
                        OtherResults.SetCamera(-0.5f);

                        break;
                    case 2:
                        //左转90°
                        OtherResults.SetCamera(0.5f);

                        break;
                    case 3:
                        //玩家爆炸


                        break;
                    case 4:
                        //事件
                        break;
                    case 5:
                        //添加DEBUFF
                        break;
                    case 6:
                        //减少20生命上限 1
                        duanWuPlayer.SetLifeMax2 += 20;

                        break;
                    case 7:
                        //失去钱
                        break;
                    case 8:
                        //存款-50% 2
                        break;
                    case 9:
                        //白天光女
                        Main.dayTime = true;
                        Main.time = 0;
                        QuickSpawnNPC(636, player.Center);
                        break;
                    case 10:
                        //史莱姆NPC死亡 2
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (nPC.type == 678 || nPC.type == 670 || nPC.type == 679 || nPC.type == 680 || nPC.type == 681 || nPC.type == 682 || nPC.type == 683 || nPC.type == 684)
                            {
                                nPC.life = 0;
                                nPC.checkDead();
                            }
                        }
                        break;
                    case 11:
                        //不能跳

                        break;
                    case 12:
                        //不能飞
                        break;
                    case 13:
                        //马赛克8 2
                        OtherResults.SetPixelation(8);

                        break;
                    case 14:
                        //宠物猫狗兔死亡 2 
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (nPC.type == 656 || nPC.type == 637 || nPC.type == 638)
                            {
                                nPC.life = 0;
                                nPC.checkDead();
                            }
                        }
                        break;
                    case 15:
                        //机械三王
                        break;
                    case 16:

                        break;
                    case 17:

                        break;
                    case 18:

                        break;
                    case 19:

                        break;
                    case 20:

                        break;
                    default:
                        break;
                }
            }
            else if (penaltylevel == 3)
            {
                int level3 = Main.rand.Next(0, 10);
                switch (level3)
                {
                    case 0:
                        //传送所有敌人 3 
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (!nPC.friendly)
                            {
                                nPC.Center = player.Center;
                            }
                        }
                        Main.NewText("chuanshong");
                        break;
                    case 1:
                        //硬核 3 破坏性
                        player.difficulty = 2;
                        Main.NewText("硬核");

                        break;
                    case 2:
                        //镜头颠倒 2
                        duanWuPlayer.ScreenShakeUpDown = true;
                        
                        break;
                    case 3:
                        //一直转
                        OtherResults.SetCamera(0);

                        break;
                    case 4:
                        //一点血 3
                        duanWuPlayer.SetLifeMax2 = player.statLifeMax2 - 1;

                        break;
                    case 5:
                        //敌方回满血
                        foreach (NPC npc in Main.ActiveNPCs)
                        {
                            if (!npc.friendly)
                            {
                                npc.life = npc.lifeMax;
                            }
                        }
                        break;
                    case 6:
                        //存款-100% 3
                        break;
                    case 7:
                        //马赛克16 3
                        OtherResults.SetPixelation(16);

                        break;
                    case 8:
                        //NPC全死 3
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (nPC.friendly)
                            {
                                nPC.life = 0;

                                nPC.checkDead();
                            }
                        }
                        break;
                    case 9:
                        //物品更换 3
                        int x = Main.rand.Next(0, 59);
                        for (int i = 0; i < 59; i++)
                        {
                            player.inventory[i] = player.inventory[x];
                        }
                        break;
                    case 10:

                        break;
                    case 11:

                        break;
                    case 12:

                        break;
                    case 13:

                        break;
                    case 14:

                        break;
                    case 15:

                        
                        break;
                    default:
                        break;
                }
            }
            else if (penaltylevel == 4)
            {
                int level4 = Main.rand.Next(0, 10);
                switch (level4)
                {
                    case 0:
                        //删档 4
                        player.KillMeForGood();

                        break;
                    case 1:
                        //一点血 4
                        duanWuPlayer.SetLifeMax2 = player.statLifeMax2 - 1;

                        break;
                    case 2:
                        //马赛克64 4
                        OtherResults.SetPixelation(64);

                        break;
                    case 3:
                        //一直答题
                        break;
                    case 4:
                        //全BOSS
                        break;
                    case 5:
                        //清空物品
                        break;
                    case 6:
                        //摄像头模式
                        duanWuPlayer.Screenpos = Main.LocalPlayer.Center-new Vector2(Main.screenWidth/2,Main.screenHeight/2);
                        duanWuPlayer.StartScreenpos = true;
                        break;
                    case 7:
                        //时间加速
                        break;
                    case 8:
                        //刷怪提升
                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    default:
                        break;
                }
            }

        }

        public void KillTileRectangle(Rectangle safeBox)
        {

            for (int i = safeBox.X - safeBox.Width; i < safeBox.X + safeBox.Width; i++)
            {
                for (int j = safeBox.Y; j < safeBox.Y + safeBox.Height; j++)
                {
                    WorldGen.KillTile(i, j, false, false,true);
                }
            }
        }

        public static void QuickSpawnNPC(int id , Vector2 pos)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                NPC.NewNPC(null, (int)pos.X, (int)pos.Y, id);
            }
            SelectNPCID = id;
            SelectNPCpos = pos;
            ModContent.GetInstance<NetNPC>().NetSeed(-1, -1);
        }

        public static void QuickSpawnProjectlies(int id ,Vector2 pos)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Projectile.NewProjectile(null, pos, Vector2.Zero, id, 99, 1);
            }
            SelectNPCID = id;
            SelectProjectliespos = pos;
            ModContent.GetInstance<NetProjectlies>().NetSeed(-1, -1);
        }

        public static void KillTileRectangle(Rectangle safeBox,bool noItem=false)
        {
            
            for (int i = safeBox.X; i < safeBox.X+safeBox.Width; i++)
            {
                for (int j = safeBox.Y; j < safeBox.Y +safeBox.Height; j++)
                {
                    WorldGen.KillTile(i, j, false, false, noItem);
                }
            }
        }


    }
}
