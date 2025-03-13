using DuanWu.Content.Buffs;
using DuanWu.Content.Items;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class RewardSystem
    {
        private int RewardLevel;

        public RewardSystem(int rewardlevel)
        {
            RewardLevel = rewardlevel;
            SetReward(RewardLevel);
        }


        private void AverageChance()
        {
            if (DuanWuPlayer.RandResults)
            {
                this.RewardLevel = Main.rand.Next(0, 10);
            }
        }

        public void SetReward(int RewardLevel = 1)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            Player player = Main.LocalPlayer;
            if (duanWuPlayer.Reward != true)
            {
                return;
            }

            AverageChance();

            if (RewardLevel == 1)
            {
                //1级
                int level1 = Main.rand.Next(0, 20);
                switch (level1)
                {
                    case 0:
                        Main.NewText("There is no rewaed!!!");
                        break;

                    case 1:
                        //回满血 1
                        player.statLife = player.statLifeMax2;
                        player.statMana = player.statManaMax2;
                        Main.NewText("回满血 1");
                        break;

                    case 2:
                        //各种任务鱼 1
                        List<short> fish = [2469, 2470, 2471, 2472, 2473, 2474, 2475, 2476, 2477, 2478, 2479, 2480, 2481, 2482, 2483, 2484, 2485, 2486, 2487, 2488, 2450, 2451, 2452, 2453, 2454, 2455, 2456, 2457, 2458, 2459, 2460, 2461, 2463, 2464, 2465, 2466, 2467, 2468];
                        QuickSpawnItemList(fish, player, 1);
                        break;

                    case 3:
                        //各种草药 1
                        List<short> yao = [318, 313, 314, 315, 316, 317, 2358];
                        QuickSpawnItemList(yao, player, 99);
                        break;

                    case 4:
                        //各种药水 1
                        List<short> potion = [ItemID.ArcheryPotion, ItemID.BattlePotion, ItemID.BiomeSightPotion, ItemID.BuilderPotion, ItemID.CalmingPotion, ItemID.CratePotion, ItemID.EndurancePotion, ItemID.FeatherfallPotion, ItemID.FishingPotion, ItemID.FlipperPotion, ItemID.GillsPotion, ItemID.GravitationPotion, ItemID.HeartreachPotion, ItemID.HunterPotion, ItemID.InfernoPotion, ItemID.InvisibilityPotion, ItemID.IronskinPotion, ItemID.LifeforcePotion, ItemID.LovePotion, ItemID.LuckPotion, ItemID.MagicPowerPotion, ItemID.ManaRegenerationPotion, ItemID.MiningPotion, ItemID.NightOwlPotion, ItemID.ObsidianSkinPotion, ItemID.RagePotion, ItemID.RegenerationPotion, ItemID.ShinePotion, ItemID.SonarPotion, ItemID.SpelunkerPotion, ItemID.StinkPotion, ItemID.SummoningPotion, ItemID.SwiftnessPotion, ItemID.ThornsPotion, ItemID.TitanPotion, ItemID.WarmthPotion, ItemID.WaterWalkingPotion, ItemID.WrathPotion];
                        QuickSpawnItemList(potion, player, 5, 99);
                        break;

                    case 5:
                        //肉前锭 1
                        List<short> bar1 = [ItemID.GoldBar, ItemID.CopperBar, ItemID.SilverBar, ItemID.IronBar, ItemID.HallowedBar, ItemID.HellstoneBar, ItemID.MeteoriteBar];
                        QuickSpawnItemList(bar1, player, 99);
                        break;

                    case 6:
                        //渔具 1
                        List<short> fishishing = [ItemID.HighTestFishingLine, ItemID.TackleBox, ItemID.CratePotion, ItemID.SonarPotion, ItemID.AnglerTackleBag, 3120, ItemID.WeatherRadio, ItemID.Sextant, ItemID.FishFinder, 4881, ItemID.LavaproofTackleBag];
                        QuickSpawnItemList(fishishing, player, 1);
                        break;

                    case 7:
                        //染料 1
                        List<short> Dyes = [1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069, 1070];
                        QuickSpawnItemList(Dyes, player, 15, 1);
                        break;

                    case 8:

                        //油漆 1
                        List<short> paint = [1071, 1072, 1073, 1074, 1075, 1076, 1077, 1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 1090, 1091, 1092, 1093, 1094, 1095, 1096, 1097, 1098, 1099, 1100];
                        QuickSpawnItemList(paint, player, 15, 99);

                        break;

                    case 9:
                        //电路 1
                        Main.NewText("电路");
                        break;

                    case 10:
                        //音乐盒 1
                        List<short> Music = [562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 1596, 1597, 1598, 1599, 1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609, 1610, 5362, 5112, 5044, 5006, 4990, 4991, 4992, 4985, 4979, 4606, 4421, 4356, 4357, 4358, 4237, 4077, 4078, 4079, 4080, 4081, 4082, 3796, 3370, 3371, 3235, 3236, 3237, 3044, 2742, 1963, 1964, 1965];
                        QuickSpawnItemList(Music, player, 1);

                        break;
                    case 11:
                        //高尔夫 1
                        Main.NewText("高尔夫");
                        break;

                    case 12:
                        //食物,饮料 1
                        List<short> food = [ItemID.Ale, ItemID.ApplePie, ItemID.Bacon, ItemID.BananaSplit, ItemID.BBQRibs, ItemID.BunnyStew, ItemID.Burger, ItemID.ChickenNugget, ItemID.ChocolateChipCookie, ItemID.ChristmasPudding, ItemID.CookedMarshmallow, ItemID.CreamSoda, ItemID.Escargot, ItemID.FriedEgg, ItemID.Fries, ItemID.FroggleBunwich, ItemID.Apple, ItemID.Apricot, ItemID.Banana, ItemID.BloodOrange, ItemID.Cherry, ItemID.Coconut, ItemID.Elderberry, ItemID.Grapefruit, ItemID.Lemon, ItemID.Mango, ItemID.Peach, ItemID.Pineapple, ItemID.Plum, ItemID.Pomegranate, ItemID.Rambutan, ItemID.SpicyPepper, ItemID.FruitJuice, ItemID.FruitSalad, ItemID.AppleJuice, ItemID.BloodyMoscato, ItemID.Lemonade, ItemID.PeachSangria, ItemID.PrismaticPunch, ItemID.TropicalSmoothie, ItemID.GingerbreadCookie, ItemID.GoldenDelight, ItemID.Grapes, ItemID.GrapeJuice, ItemID.GrilledSquirrel, ItemID.GrubSoup, ItemID.Hotdog, ItemID.IceCream, ItemID.JojaCola, ItemID.Milkshake, ItemID.MonsterLasagna, ItemID.Nachos, ItemID.PadThai, ItemID.Pizza, ItemID.Pho, ItemID.PotatoChips, ItemID.PumpkinPie, ItemID.RoastedBird, ItemID.RoastedDuck, ItemID.Sake, ItemID.SauteedFrogLegs, ItemID.ShrimpPoBoy, ItemID.ShuckedOyster, ItemID.Spaghetti, ItemID.Steak, ItemID.SugarCookie, ItemID.Teacup];
                        QuickSpawnItemList(food, player, 15, 25);

                        break;
                    case 13:
                        //异界八音盒 1
                        List<short> Music1 = [5014, 5015, 5016, 5017, 5018, 5019, 5020, 5021, 5022, 5023, 5024, 5025, 5026, 5027, 5028, 5029, 5030, 5031, 5032, 5033, 5034, 5035, 5036, 5037, 5038, 5039, 5040];
                        QuickSpawnItemList(Music1, player, 1);

                        break;

                    case 14:
                        //钱 1 
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 71, 99);
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 72, 99);

                        break;

                    case 15:
                        //随机物品1 1
                        player.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(null), Main.rand.Next(0, 5455), 1);

                        break;
                    case 16:
                        //鱼饵1 1 
                        List<short> bait = [2674, 2675, 2676, 4334, 2156, 4335, 3194, 3191, 1992, 2007, 2740, 4336, 3192, 4845, 2001, 4361, 4847, 2004, 4363, 4849, 1994, 4337, 1995, 1996, 4338, 2157, 3193, 2006, 5132, 1998, 1999, 1997, 4418, 2002, 4339, 2000];
                        QuickSpawnItemList(bait, player, 999);

                        break;
                    case 17:
                        //种子 1
                        List<short> seeds = [58, 62, 194, 195, 307, 308, 309, 310, 311, 312, 5214, 4241, 4041, 4042, 4043, 4044, 4045, 4046, 4047, 4048, 2357, 2171, 369];
                        QuickSpawnItemList(seeds, player, 990);

                        break;
                    case 18:
                        //宝石 1
                        List<short> jewel = [177, 179, 178, 179, 180, 181, 182, 999];
                        QuickSpawnItemList(jewel, player, 99);

                        break;
                    case 19:
                        //大宝石 1
                        List<short> bigjewel = [3643, 1522, 1527, 1525, 1526, 1524, 1523];
                        QuickSpawnItemList(bigjewel, player, 1);

                        break;
                    case 20:
                        //家具 1
                        Main.NewText("家具");
                        break;
                    case 21:

                        break;
                    case 22:

                        break;
                    case 23:

                        break;
                    case 24:

                        break;
                    case 25:

                        break;
                    case 26:

                        break;
                    case 27:

                        break;
                    case 28:

                        break;
                    case 29:

                        break;
                    case 30:

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

                    default:
                        Main.NewText("未知奖励");
                        break;

                }

            }

            else if (RewardLevel == 2)
            {

                //2级

                int level2 = Main.rand.Next(0, 10);
                switch (level2)
                {
                    case 0:
                        //加生命上限55 2
                        duanWuPlayer.SetLifeMax2 += 55;
                        Main.NewText("加生命上限 2");
                        break;
                    case 1:
                        //钱 2
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 73, 999);

                        break;
                    case 2:
                        //弹药 1
                        List<short> Bullwt = [97, 234, 278, 515, 546, 1179, 1302, 1335, 1342, 1349, 1350, 1351, 1352, 3567, 4915];
                        QuickSpawnItemList(Bullwt, player, 200);

                        break;
                    case 3:
                        //弹药 2
                        List<short> arrow = [40, 41, 47, 51, 265, 516, 545, 988, 1235, 1334, 1341, 3003, 3568, 5348];
                        Main.NewText("弹药");
                        QuickSpawnItemList(arrow, player, 200);

                        break;
                    case 4:
                        //BUFF 2

                        Main.NewText("BUFF2");
                        break;
                    case 5:
                        //运气 2
                        player.luck += 1;
                        Main.NewText(player.luck);
                        break;
                    case 6:
                        //各种灌注 2
                        List<short> list = [ItemID.FlaskofCursedFlames, ItemID.FlaskofFire, ItemID.FlaskofGold, ItemID.FlaskofIchor, ItemID.FlaskofNanites, ItemID.FlaskofParty, ItemID.FlaskofPoison, ItemID.FlaskofVenom];
                        QuickSpawnItemList(list, player, 3, 25);

                        break;
                    case 7:
                        //随机物品5 2
                        for (int i = 0; i < 5; i++)
                        {

                            player.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(null), Main.rand.Next(0, 5455), 10);
                        }

                        break;

                    case 8:
                        //玩家框 2
                        duanWuPlayer.ShowPlayHitBox = true;
                        break;

                    case 9:
                        //旗帜
                        List<short> flag = [1615, 1616, 1617, 1618, 1619, 1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628, 1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641, 1642, 1643, 1644, 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684, 1685, 1686, 1687, 1688, 1689, 1690, 1691, 1692, 1693, 1694, 1695, 1696, 1697, 1698, 1699, 1700, 1701, 2897, 2898, 2899, 2900, 2901, 2902, 2903, 2904, 2905, 2906, 2907, 2908, 2909, 2910, 2911, 2912, 2913, 2914, 2915, 2916, 2917, 2918, 2919, 2920, 2921, 2922, 2923, 2924, 2925, 2926, 2927, 2928, 2929, 2930, 2931, 2932, 2933, 2934, 2935, 2936, 2937, 2938, 2939, 2940, 2941, 2942, 2943, 2944, 2945, 2946, 2947, 2948, 2949, 2950, 2951, 2952, 2953, 2954, 2955, 2956, 2957, 2958, 2959, 2960, 2961, 2962, 2963, 2964, 2965, 2966, 2967, 2968, 2969, 2970, 2971, 2972, 2973, 2974, 2975, 2976, 2977, 2978, 2979, 2980, 2981, 2982, 2983, 2984, 2985, 2986, 2987, 2988, 2989, 2990, 2991, 2992, 2993, 2994];
                        QuickSpawnItemList(flag, player, 20, 1);

                        break;
                    case 10:
                        //环境
                        List<short> environment = [780, 5392, 5393, 5394, 781, 782, 783, 784];
                        QuickSpawnItemList(environment, player, 999);
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 779, 1);

                        break;


                    case 11:

                        //稀有鱼饵 2
                        List<short> bait2 = [2891, 4340, 2893, 4362, 4419, 2895, 2673,];
                        QuickSpawnItemList(bait2, player, 99);

                        break;
                    case 12:
                        //匣子 2
                        List<short> crate = [2334, 2335, 2336, 3203, 3204, 3205, 3206, 3207, 3208, 4405, 4407, 4877, 5002];
                        QuickSpawnItemList(crate, player, 10);

                        break;
                    case 13:
                        //额外召唤栏1 2
                        duanWuPlayer.SetMinions += 1;
                        break;
                    case 14:
                        //加速度10 2
                        duanWuPlayer.Setmovespeed += 2;

                        break;
                    case 15:
                        //磁力
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
                    case 21:

                        break;
                    case 22:

                        break;
                    default:

                        break;

                }

            }

            else if (RewardLevel == 3)
            {


                //3级
                int level3 = Main.rand.Next(0, 10);
                switch (level3)
                {
                    case 0:
                        //各种各样的锭 3
                        Main.NewText("各种各样的锭 3");
                        List<short> bar = [19, 20, 21, 22, 57, 117, 175, 381, 382, 391, 703, 704, 705, 706, 1006, 1184, 1191, 1198, 1225, 1257, 1552, 3261, 3467];
                        QuickSpawnItemList(bar, player, 99);
                        break;
                    case 1:
                        //NPC party 3
                        Main.NewText("NPC");
                        break;
                    case 2:
                        //钱 3
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 71, 999);
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 72, 999);
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 73, 999);
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), 74, 999);

                        break;
                    case 3:
                        //弹幕框 3
                        duanWuPlayer.ShowProjectileBox = true;
                        break;
                    case 4:
                        //怪物框 3
                        duanWuPlayer.ShowNPCHitBox = true;
                        break;
                    case 5:
                        //永久玩家框 3
                        duanWuPlayer.ShowPlayHitBox = true;
                        duanWuPlayer.ForverShowPlayHitBox = true;
                        break;
                    case 6:
                        //四柱武器 3
                        List<short> sizhu = [3469, 3475, 3476, 3542, 3531, 3474, 3473, 3543];
                        QuickSpawnItemList(sizhu, player, 1, 1);

                        break;
                    case 7:
                        //四柱工具 3 
                        List<short> xingxu = [2772, 2773, 2774, 2775, 2776, 3524, 3526, 3527, 3528, 3525, 3462, 3453, 3464, 3465];
                        QuickSpawnItemList(xingxu, player, 3, 1);

                        break;

                    case 8:

                        //额外召唤栏5 2
                        duanWuPlayer.SetMinions += 5;
                        break;

                    case 9:
                        //SuperWoodSword 3
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), ModContent.ItemType<SuperWoodenSword>(), 99);


                        break;
                    case 10:
                        //UlraWoodSword 3
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), ModContent.ItemType<UltraWoodenSword>(), 99);

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

            else if (RewardLevel == 4)
            {



                //4级
                int level4 = Main.rand.Next(0, 10);
                switch (level4)
                {
                    case 0:
                        //天顶剑 4
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(null), ItemID.Zenith, 1);
                        Main.NewText("天顶剑 4");
                        break;
                    case 1:
                        //永久NPC框 4
                        duanWuPlayer.ShowNPCHitBox = true;
                        duanWuPlayer.ForverShowNPCHitBox = true;
                        break;
                    case 2:
                        //永久弹幕框 4
                        duanWuPlayer.ShowProjectileBox = true;
                        duanWuPlayer.ForverShowProjectileBox = true;
                        break;
                    case 3:
                        //击杀敌对NPC 4
                        foreach (NPC nPC in Main.ActiveNPCs)
                        {
                            if (!nPC.friendly)
                            {
                                nPC.life = 0;

                                nPC.checkDead();
                            }
                        }
                        break;
                    case 4:
                        //无线魔力 4
                        duanWuPlayer.SetMana = true;
                        break;
                    case 5:
                        //无限飞行 4
                        duanWuPlayer.Fly = true;
                        break;
                    case 6:
                        //亮度提升

                        break;
                    case 7:
                        //太阳
                        player.AddBuff(ModContent.BuffType<Sun>(), 30000);
                        break;

                    case 8:
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

        public void QuickSpawnItemList(List<short> x, Player player, int num = 99)
        {
            for (int i = 0; i < x.Count; i++)
            {
                player.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(null), x[i], num);
            }
        }


        public void QuickSpawnItemList(List<short> x, Player player, int count = 1, int num = 99)
        {
            for (int i = 0; i < count; i++)
            {
                player.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(null), x[Main.rand.Next(0, x.Count)], num);
            }
        }

    }
}
