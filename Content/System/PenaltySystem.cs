﻿using DuanWu.Content.Projectiles;
using log4net.Core;
using Luminance.Common.Utilities;
using Luminance.Core.Cutscenes;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.RGB;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class PenaltySystem
    {

        private int Penaltylevel;

        public static double SetTime;
        public static bool Setday;
        public static int SelectNPCID = 1;
        public static int SelectProjectliesID;
        public static int SelectProjectilesDamage;
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
                this.Penaltylevel = Main.rand.Next(1, 5);
            }
        }

        public void PenaltyText(string path) => Main.NewText(Language.GetTextValue("Mods.DuanWu.Other.Penalty." + path));

        public void SetPenalty(int penaltylevel = 1)
        {

            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.Reward != false)
            {
                return;
            }

            Player player = Main.LocalPlayer;
            AverageChance();
            Main.NewText(penaltylevel);
            if (penaltylevel == 1)
            {
                int level1 = Main.rand.Next(0, 31);
                Main.NewText(level1);
                switch (level1)
                {
                    case 0:
                        //1
                        PenaltyText("1.0");
                        break;
                    case 1:
                        //猪鲨 1
                        QuickSpawnNPC(NPCID.DukeFishron, player.position);
                        break;
                    case 2:
                        //减少20生命上限 1
                        duanWuPlayer.SetLifeMax2 -= 20;
                        PenaltyText("1.2");
                        break;
                    case 3:
                        //换皮 1
                        player.HeldItem.type = ItemID.DirtBlock;
                        break;
                    case 4:
                        //高斯模糊 1
                        if (duanWuPlayer.GaussBlurActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.GaussBlurActive = true;
                        }
                        break;
                    case 5:
                        //白天地牢守卫
                        QuickSetTime(0, true);
                        QuickSpawnNPC(35, player.Center);
                        break;
                    case 6:
                        //生成雷管 1
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 _v2 = Main.rand.NextVector2Circular(16, 16);
                            QuickSpawnProjectlies(29, player.Center + _v2 * 20, 1);
                        }
                        break;
                    case 7:
                        //反向移动x 2
                        player.AddBuff(31, Utilities.MinutesToFrames(5));
                        break;
                    case 8:
                        //减速 1
                        duanWuPlayer.Setmovespeed *= 0.5f;

                        break;
                    case 9:
                        //地球上投 1
                        player.velocity.Y -= 200;

                        break;
                    case 10:
                        //弹跳雷管
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 _v2 = Main.rand.NextVector2Circular(16, 16);
                            QuickSpawnProjectlies(637, player.Center + _v2 * 20, 1);
                        }
                        break;
                    case 11:
                        SetPenalty(penaltylevel);
                        break;
                    case 12:
                        //随机1
                        QuickSpawnNPC(Main.rand.Next(1, 668), player.Center);
                        break;
                    case 13:
                        //-100
                        player.statLife -= 100;
                        PenaltyText("1.13");
                        break;
                    case 14:
                        //随机传送
                        player.TeleportationPotion();
                        break;
                    case 15:
                        //UFO
                        List<short> UFO = [392, 392, 395, 395];
                        QuickSpawnNPC(UFO, player.Center);
                        break;
                    case 16:
                        //传送(0,0)
                        player.Teleport(Vector2.Zero);
                        break;
                    case 17:
                        //蜂巢
                        for (int i = 0; i < 5; i++)
                        {
                            Vector2 _v2 = Main.rand.NextVector2Circular(16, 16);
                            QuickSpawnProjectlies(655, player.Center + _v2 * 20, 1);
                        }
                        break;
                    case 18:
                        //骷髅
                        List<short> rust = [269, 270, 271, 272, 273, 274, 275, 276];
                        QuickSpawnNPC(rust, player.Center);
                        break;
                    case 19:
                        //物品混乱
                        duanWuPlayer.confusion = true;
                        break;
                    case 20:
                        //眼球
                        List<short> list = [-42, -41, -40, -39, -38, 4, 5, 133];
                        QuickSpawnNPC(list, player.Center);
                        break;

                    case 21:
                        //史莱姆
                        List<short> slime = [138, 121, 122, 71, 59, 16, 1, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -25, 50];
                        QuickSpawnNPC(slime, player.Center);
                        break;
                    case 22:
                        //恶魔
                        List<short> devil = [156, 66, 62];
                        QuickSpawnNPC(devil, player.Center);
                        break;
                    case 23:
                        //乌龟 
                        List<short> tortoise = [153, 154];
                        QuickSpawnNPC(tortoise, player.Center);
                        break;
                    case 24:
                        //水晶
                        List<short> god = [657, 660, 659, 658];
                        QuickSpawnNPC(god, player.Center);
                        break;
                    case 25:
                        //骷髅2
                        List<short> hell = [277, 278, 279, 280, 281];
                        QuickSpawnNPC(hell, player.Center);
                        break;
                    case 26:
                        //骷髅3
                        List<short> bones = [291, 291, 292, 292, 293, 293];
                        QuickSpawnNPC(bones, player.Center);
                        break;
                    case 27:
                        //死神
                        List<short> reaper = [253, 253, 253, 253, 253, 253];
                        QuickSpawnNPC(reaper, player.Center);
                        break;
                    case 28:
                        //丛林三王
                        List<short> sh = [153, 177, 157];
                        QuickSpawnNPC(sh, player.Center);
                        break;
                    case 29:
                        //血月
                        List<short> blood = [618, 619, 620, 621];
                        QuickSpawnNPC(blood, player.Center);
                        break;
                    case 30:
                        //随机1
                        for (int i = 0; i < 5; i++)
                        {
                            QuickSpawnNPC(Main.rand.Next(1, 668), player.Center);
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (penaltylevel == 2)
            {
                int level2 = Main.rand.Next(0, 23);
                Main.NewText(level2);
                switch (level2)
                {
                    case 0:
                        //随机npc10 2
                        for (int i = 0; i < 10; i++)
                        {
                            QuickSpawnNPC(Main.rand.Next(1, 668), player.Center);
                        }
                        break;
                    case 1:
                        //右转90°
                        if (duanWuPlayer.CameraActive && duanWuPlayer.Cameraintensity == -0.5f)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.CameraActive = true;
                            duanWuPlayer.Cameraintensity = -0.5f;
                        }
                        break;
                    case 2:
                        //左转90°
                        if (duanWuPlayer.CameraActive && duanWuPlayer.Cameraintensity == 0.5f)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.CameraActive = true;
                            duanWuPlayer.Cameraintensity = 0.5f;
                        }
                        break;
                    case 3:
                        //玩家爆炸
                        QuickSpawnProjectlies(1002, player.Center, 99999);
                        break;
                    case 4:
                        //随机前缀
                        player.HeldItem.Prefix(Main.rand.Next(0, PrefixID.Count));
                        PenaltyText("2.4");
                        break;
                    case 5:
                        //添加DEBUFF
                        List<short> debuff = [20, 21, 22, 23, 24, 25, 30, 31, 32, 33, 35, 36, 37, 38, 39, 44, 46, 47, 67, 68, 69, 70, 72, 80, 86, 88, 94, 103, 119, 120, 137, 144, 145, 148, 149, 153, 156, 160, 163, 164, 169, 183, 186, 189, 192, 194, 195, 196, 197, 199, 203, 204, 307, 309, 310, 313, 315, 316, 319, 320, 321, 323, 324, 326, 332, 333, 334, 337, 344, 350, 353];
                        for (int i = 0; i < Main.rand.Next(1, 9); i++)
                        {

                            player.AddBuff(debuff[Main.rand.Next(0, debuff.Count)], 18000);
                        }

                        break;
                    case 6:
                        //传送 2
                        player.Teleport(new Vector2(Main.tile.Width, Main.tile.Height));
                        break;
                    case 7:
                        //黑白
                        if (duanWuPlayer.MagnifierActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {

                            duanWuPlayer.MagnifierActive = true;
                            duanWuPlayer.Matrixfilter = new(
                                                            0.299f, 0.587f, 0.114f, 0,
                                                            0.299f, 0.587f, 0.114f, 0,
                                                            0.299f, 0.587f, 0.114f, 0,
                                                            0.299f, 0.587f, 0.114f, 1);
                        }
                        break;
                    case 8:
                        //反色
                        if (duanWuPlayer.MatrixActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {

                            duanWuPlayer.MatrixActive = true;
                            duanWuPlayer.Matrixfilter = new(
                                                             -1, 0, 0, 1,
                                                             0, -1, 0, 1,
                                                             0, 0, -1, 1,
                                                             0, 0, 0, 1);
                        }
                        break;
                    case 9:
                        //白天光女
                        Main.dayTime = true;
                        Main.time = 0;
                        QuickSpawnNPC(636, player.Center);
                        QuickSetTime(0, true);
                        break;
                    case 10:
                        //史莱姆NPC死亡 2
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (nPC.type == 678 || nPC.type == 670 || nPC.type == 679 || nPC.type == 680 || nPC.type == 681 || nPC.type == 682 || nPC.type == 683 || nPC.type == 684)
                            {
                                nPC.life = 0;
                            }
                        }
                        PenaltyText("2.10");
                        break;
                    case 11:
                        //易碎10 3
                        duanWuPlayer.hitdamage += 0.1f;
                        PenaltyText("2.11");
                        break;
                    case 12:
                        //不能飞

                        duanWuPlayer.SetwingTime = 0;
                        PenaltyText("2.12");
                        break;
                    case 13:
                        //马赛克8 2
                        if (duanWuPlayer.PixelationActive && duanWuPlayer.Pixelationintensity == 8)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {

                            duanWuPlayer.PixelationActive = true;
                            duanWuPlayer.Pixelationintensity = 8;
                        }
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
                        PenaltyText("2.14");
                        break;
                    case 15:
                        //机械三王
                        List<short> shorts = [NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime];
                        QuickSpawnNPC(shorts, player.Center);
                        break;
                    case 16:
                        //随机前缀
                        List<int> prefix = [8, 13, 23, 24, 39, 40, 41];
                        player.HeldItem.Prefix(prefix[Main.rand.Next(0, 7)]);
                        PenaltyText("2.16");
                        break;
                    case 17:
                        //随机BOSS 2
                        List<short> boss = [NPCID.KingSlime, NPCID.EyeofCthulhu, NPCID.EaterofWorldsHead, NPCID.BrainofCthulhu, NPCID.QueenBee, NPCID.Deerclops, NPCID.Skeleton, NPCID.WallofFlesh, NPCID.QueenSlimeBoss, NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime, NPCID.Plantera, NPCID.DukeFishron, NPCID.EmpressButterfly, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerStardust, NPCID.LunarTowerVortex, NPCID.MoonLordCore];
                        QuickSpawnNPC(boss[Main.rand.Next(0, boss.Count)], player.Center);
                        break;
                    case 18:
                        //防御力下降10 2
                        duanWuPlayer.SetDefense -= 10;
                        PenaltyText("2.18");
                        break;
                    case 19:
                        //各种宝箱怪
                        List<short> Mimic = [473, 474, 475, 476];
                        QuickSpawnNPC(Mimic, player.Center);
                        break;
                    case 20:
                        //四柱
                        List<short> Pillar = [517, 422, 507, 493];
                        QuickSpawnNPC(Pillar, player.Center);
                        break;
                    case 21:
                        //巨人
                        List<short> Golem = [243, 245, 631, 482];
                        QuickSpawnNPC(Golem, player.Center);
                        break;
                    case 22:
                        //放大镜
                        if (duanWuPlayer.MagnifierActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.MagnifierActive = true;
                        }
                        break;
                    case 23:
                        //镜头颠倒 2 
                        if (duanWuPlayer.CameraActive && duanWuPlayer.Cameraintensity == 1)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.CameraActive = true;
                            duanWuPlayer.Cameraintensity = 1;
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (penaltylevel == 3)
            {
                int level3 = Main.rand.Next(0, 14);
                Main.NewText(level3);
                switch (level3)
                {
                    case 0:
                        //禁锢
                        player.AddBuff(47, Utilities.SecondsToFrames(5));
                        CutsceneManager.QueueCutscene(new VideoCutscene());
                        break;
                    case 1:
                        //硬核 3 
                        if (player.difficulty == 2)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            player.difficulty = 2;
                            PenaltyText("3.1");
                        }
                        break;
                    case 2:
                        //镜头震动 2
                        if (duanWuPlayer.ScreenShakeUpDown)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.ScreenShakeUpDown = true;
                        }
                        break;
                    case 3:
                        //一直转
                        if (duanWuPlayer.CameraActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.CameraActive = true;
                            duanWuPlayer.Cameraintensity = 0;
                        }
                        break;
                    case 4:
                        //半血 3
                        duanWuPlayer.SetLifeMax2 = -(int)player.statLifeMax2 / 2;
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
                        for (int i = 50; i < 54; i++)
                        {
                            player.inventory[i].type = 0;
                        }
                        PenaltyText("3.6");
                        break;
                    case 7:
                        //马赛克16 3
                        if (duanWuPlayer.PixelationActive && duanWuPlayer.Pixelationintensity == 16)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.PixelationActive = true;
                            duanWuPlayer.Pixelationintensity = 16;
                        }
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
                        PenaltyText("3.8");
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
                        //丢物品
                        player.DropItems();
                        break;
                    case 11:
                        //召唤物 3
                        duanWuPlayer.SetMinions -= 1;
                        PenaltyText("3.11");
                        break;
                    case 12:
                        //答题成绩减半
                        duanWuPlayer.PlayerAccuracy /= 2;
                        PenaltyText("3.12");
                        break;
                    case 13:
                        //死亡 3
                        player.KillMe(new PlayerDeathReason(), 5836721, 1);
                        break;

                    default:
                        break;
                }
            }
            else if (penaltylevel == 4)
            {
                int level4 = Main.rand.Next(0, 9);
                Main.NewText(level4);
                switch (level4)
                {
                    case 0:
                        //删档 4
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ModContent.ProjectileType<KillPlayer>(), 0, 0);
                        PenaltyText("4.0");
                        break;
                    case 1:
                        //一点血 4
                        duanWuPlayer.SetLifeMax2 = 1 - player.statLifeMax2;
                        PenaltyText("4.1");
                        break;
                    case 2:
                        //马赛克64 4
                        if (duanWuPlayer.PixelationActive)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.PixelationActive = true;
                            duanWuPlayer.Pixelationintensity = 64;
                        }
                        break;
                    case 3:
                        //游戏崩溃
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ModContent.ProjectileType<Error>(), 0, 0);
                        PenaltyText("4.3");
                        break;
                    case 4:
                        //全BOSS
                        List<short> boss = [NPCID.KingSlime, NPCID.EyeofCthulhu, NPCID.EaterofWorldsHead, NPCID.BrainofCthulhu, NPCID.QueenBee, NPCID.Deerclops, NPCID.Skeleton, NPCID.WallofFlesh, NPCID.QueenSlimeBoss, NPCID.Retinazer, NPCID.Spazmatism, NPCID.TheDestroyer, NPCID.SkeletronPrime, NPCID.Plantera, NPCID.DukeFishron, NPCID.EmpressButterfly, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerStardust, NPCID.LunarTowerVortex, NPCID.MoonLordCore];
                        QuickSpawnNPC(boss, player.Center);
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
                        if (duanWuPlayer.StartScreenpos)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            duanWuPlayer.Screenpos = duanWuPlayer.screenCache = Main.LocalPlayer.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                            duanWuPlayer.StartScreenpos = true;
                        }
                        break;
                    case 7:
                        //答题记录清除
                        duanWuPlayer.PlayerAccuracy = 0;
                        duanWuPlayer.PlayerQuestioncount = 0;
                        PenaltyText("4.7");
                        break;
                    case 8:
                        //刷怪提升
                        if (DuanWuPlayer.SetSpwanRate)
                        {
                            SetPenalty(penaltylevel);
                        }
                        else
                        {
                            QuickSetSpwanRate(true);
                            PenaltyText("4.8");
                        }
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
            SelectNPCpos = pos + Main.rand.NextVector2Circular(10, 10) * 50;
            ModContent.GetInstance<NetNPC>().SendPacket(-1, -1);
        }
        /// <summary>
        /// 生成射弹并同步
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>

        public static void QuickSetSpwanRate(bool rate)
        {
            DuanWuPlayer.SetSpwanRate = rate;
            ModContent.GetInstance<NetSpawnRate>().SendPacket(-1, -1);
        }

        public static void QuickSpawnProjectlies(int id, Vector2 pos, int damage)
        {
            Projectile.NewProjectile(null, pos, Vector2.Zero, id, damage, 1);
            SelectNPCID = id;
            SelectProjectliespos = pos;
            SelectProjectilesDamage = damage;
            ModContent.GetInstance<NetProjectlies>().SendPacket(-1, -1);
        }

        public static void QuickSetTime(double time, bool daytime)
        {
            SetTime = time;
            Setday = daytime;
            ModContent.GetInstance<NetTime>().SendPacket(-1, -1);
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
