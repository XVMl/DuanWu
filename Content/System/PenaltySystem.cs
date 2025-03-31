using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.RGB;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class PenaltySystem
    {

        private int Penaltylevel;

        public static int SelectNPCID=1;
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
                        QuickSpawnNPC(NPCID.DukeFishron, v2);
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
                        duanWuPlayer.Setmovespeed -= 0.5f;

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
                        //UFO
                        List<short> UFO = [392, 392, 395, 395];
                        break;
                    case 16:
                        //飞船

                        break;
                    case 17:
                        //蜂巢

                        break;
                    case 18:
                        //骷髅
                        List<short> rust = [269, 270, 271, 272, 273, 274, 275, 276];

                        break;
                    case 19:
                        //史莱姆
                        break;
                    case 20:
                        //眼球
                        List<short> list = [-42,-41,-40,-39,-38,4,5,133];
                        
                        break;

                    case 21:
                        //史莱姆
                        List<short> slime = [138,121,122,71,59,16,1,-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-25,50];
                        break;
                    case 22:
                        //恶魔
                        List<short> devil = [156, 66, 62];
                        break;
                    case 23:
                        //乌龟 
                        List<short> tortoise = [153, 154];
                        break;
                    case 24:
                        //水晶
                        List<short> god = [657,660,659,658];
                        break;
                    case 25:
                        //骷髅2
                        List<short> hell = [277, 278, 279, 280, 281];
                        break;
                    case 26:
                        //骷髅3
                        List<short> bones = [291, 291, 292, 292, 293, 293];

                        break;
                    case 27:
                        //死神
                        List<short> reaper = [253, 253, 253, 253, 253, 253];
                        break;
                    case 28:
                        //丛林三王
                        List<short> sh = [153, 177, 157];

                        break;
                    case 29:
                        //血月
                        List<short> blood = [618, 619, 620, 621];
                        break;
                    case 30:
                        //心跳

                        break;
                    case 31:
                        break;
                    case 32:
                        break;

                    case 33:
                        break;
                    case 34:
                        break;
                    case 35:
                        break;
                    case 36:
                        break;
                    case 37:
                        break;
                    case 38:
                        break;
                    case 39:
                        break;
                    case 40:
                        break;
                    case 41:
                        break;
                    case 42:
                        break;
                    case 43:
                        break;
                    case 44:
                        break;
                    case 45:
                        break;
                    case 46:

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
                        player.wingTime = 0;
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
                        List<short> shorts = [NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime];
                        QuickSpawnNPC(shorts, player.Center);
                        break;
                    case 16:
                        //反向移动x 2
                        duanWuPlayer.Setmovespeed1 *= -1;
                        break;
                    case 17:
                        //磁力下将 2
                        break;
                    case 18:
                        //防御力下降10 2
                        duanWuPlayer.SetDefense -= 10;
                        break;
                    case 19:
                        //各种宝箱怪
                        List<short> Mimic = [473, 474, 475, 476];

                        break;
                    case 20:
                        //四柱
                        break;

                    case 21:
                        //巨人
                        List<short> Golem = [243, 245, 531, 482];
                        break;

                        case 22:
                        //笨猪鲨
                            break;
                        case 23:
                        //物品混乱
                            break;
                        case 24:
                        break; case 25:
                            break;
                        case 26:break;
                        case 27:break;
                        case 28:break;
                        case 29:
                            break;
                        case 30:
                            break;
                        case 31:

                            break;
                        case 32:
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
                        //易碎10 3
                        break;
                    case 11:
                        //召唤物 3
                        duanWuPlayer.Setmovespeed -= 1;
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
                        List<short> boos = [NPCID.KingSlime, NPCID.EyeofCthulhu, NPCID.EaterofWorldsHead, NPCID.BrainofCthulhu, NPCID.QueenBee, NPCID.Deerclops, NPCID.Skeleton, NPCID.WallofFlesh, NPCID.QueenSlimeBoss, NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime, NPCID.Plantera, NPCID.DukeFishron, NPCID.EmpressButterfly, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerStardust, NPCID.LunarTowerVortex, NPCID.MoonLordCore];
                        QuickSpawnNPC(boos, player.Center);
                        break;
                    case 5:
                        //清空物品
                        foreach (var item in player.inventory)
                        {
                            item.type = 0;
                        }
                        foreach (var item in player.miscEquips)
                        {
                            item.type = 0;
                        }
                        foreach (var item in player.armor)
                        {
                            item.type = 0;
                        }
                        break;
                    case 6:
                        //摄像头模式
                        duanWuPlayer.Screenpos = Main.LocalPlayer.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                        duanWuPlayer.StartScreenpos = true;
                        break;
                    case 7:
                        //时间加速

                        break;
                    case 8:
                        //刷怪提升
                        OtherResults.QuickSetSpwanRate(true);
                        break;
                    case 9:
                        //饰品栏减少 4
                        break;
                    case 10:

                        break;
                    default:


                        break;
                }
            }

        }



        /// <summary>
        /// 以玩家为中心破坏矩形范围内的方块
        /// </summary>
        /// <param name="safeBox"></param>
        public void KillTileRectangle(Rectangle safeBox)
        {

            for (int i = safeBox.X - safeBox.Width; i < safeBox.X + safeBox.Width; i++)
            {
                for (int j = safeBox.Y; j < safeBox.Y + safeBox.Height; j++)
                {
                    WorldGen.KillTile(i, j, false, false, true);
                }
            }
        }

        public static void QuickSpawnNPC(List<short> x, Vector2 pos)
        {
            foreach (int y in x)
            {
                QuickSpawnNPC(y, pos);
            }
        }

        /// <summary>
        /// 生成NPC并同步
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        public static void QuickSpawnNPC(int id, Vector2 pos)
        {
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, id);
            SelectNPCID = id;
            SelectNPCpos = pos;
            ModContent.GetInstance<NetNPC>().NetSeed(-1, -1);
        }
        /// <summary>
        /// 生成射弹并同步
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        public static void QuickSpawnProjectlies(int id, Vector2 pos)
        {
            
            Projectile.NewProjectile(null, pos, Vector2.Zero, id, 99, 1);
            SelectNPCID = id;
            SelectProjectliespos = pos;
            ModContent.GetInstance<NetProjectlies>().NetSeed(-1, -1);
        }

        public static void KillTileRectangle(Rectangle safeBox, bool noItem = false)
        {

            for (int i = safeBox.X; i < safeBox.X + safeBox.Width; i++)
            {
                for (int j = safeBox.Y; j < safeBox.Y + safeBox.Height; j++)
                {
                    WorldGen.KillTile(i, j, false, false, noItem);
                }
            }
        }


    }
}
