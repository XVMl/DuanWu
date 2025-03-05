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


        public PenaltySystem(int penaltylevel)
        {
            this.Penaltylevel = penaltylevel;
            SetPenalty(penaltylevel);
        }

        public PenaltySystem()
        {
            SetPenalty();
        }



        public void SetPenalty(int penaltylevel = 1)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.Reward != false)
            {
                return;
            }
            int n = Main.rand.Next(0, 12);
            Player player = Main.LocalPlayer;
            switch (n)
            {
                case 0:
                    Main.NewText("These is no penaltly");
                    break;

                case 1:
                    Vector2 v2 = player.position + Main.rand.NextVector2Circular(10, 10) * 100;
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)v2.X, (int)v2.Y, NPCID.DukeFishron);
                    break;

                case 2:
                    player.KillMeForGood();
                    break;

                case 3:
                    Main.dayTime = false;
                    Main.time = 0;
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.DukeFishron);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.KingSlime);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.EyeofCthulhu);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Deerclops);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Skeleton);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.WallofFlesh);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.QueenSlimeBoss);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Retinazer);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Spazmatism);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.TheDestroyer);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Skeleton);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Plantera);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Golem);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.MoonLordCore);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.QueenBee);
                    break;

                case 4:
                    Main.dayTime = false;
                    Main.time = 0;
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Retinazer);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Spazmatism);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.TheDestroyer);
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Skeleton);
                    break;

                case 5:
                    player.TeleportationPotion();
                    break;

                case 6:
                    OtherResults.SetCamera(-0.2f);
                    break;

                case 7:
                    duanWuPlayer.KeepQuestionActive = true;
                    Main.NewText("dadaosi");
                    break;

                case 8:
                    duanWuPlayer.QuestionCount = 10;
                    Main.NewText("da10");
                    break;

                case 9:
                    duanWuPlayer.QuestionCount = 1;
                    Main.NewText("da1");
                    break;

                case 10:
                    duanWuPlayer.QuestionCount = 5;
                    Main.NewText("da5");
                    break;

                case 11:
                    foreach (NPC nPC in Main.ActiveNPCs)
                    {
                        if (!nPC.friendly)
                        {
                            nPC.Center = player.Center;
                        }
                    }
                    Main.NewText("chuanshong");
                    break;

                case 12:
                    player.difficulty = 2;
                    Main.NewText("硬核");
                    break;

                case 13:
                    player.HeldItem.type = ItemID.DirtBlock;
                    break;

                case 14:
                    duanWuPlayer.ScreenShakeUpDown = true;
                    break;
                case 15:
                    //左转90°
                    OtherResults.SetCamera(0.5f);

                    break;
                case 16:
                    //右转90°
                    OtherResults.SetCamera(-0.5f);

                    break;
                case 17:
                    //一直转
                    OtherResults.SetCamera(0);

                    break;
                case 18:
                    //高斯模糊
                    OtherResults.SetBlur();

                    break;
                case 19: 

                    //减低分辨率
                    break;
                case 20:
                    //白天地牢守卫
                    Main.dayTime = true;
                    Main.time = 0;
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y, NPCID.Skeleton);
                    break;
                case 21: 
                    //水牢


                    break;
                case 22: 
                    //杀死玩家
                    

                    break;
                case 23:
                    //
                    break;
                case 24:
                    //生成雷管
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 _v2 = Main.rand.NextVector2Circular(16, 16);
                        Projectile.NewProjectile(null, player.Center + _v2 * 20, new Vector2(0, 0), 29, 1, player.whoAmI, 0, 0, 0);
                    }
                    break;
                case 25:
                    //生成落石


                    break;
                case 26:
                    //玩家爆炸


                    break;
                case 27: 
                    //事件
                    break;
                case 28:
                    //添加DEBUFF
                    break;
                case 29:
                    //减少生命上限
                    break;
                case 30: 
                    //一点血

                    break;
                case 31: 
                    //敌方回满血
                    break;
                case 32: 
                    //失去钱
                    break;
                case 33:
                    //存款-50% 2
                    break;
                case 34:
                    //存款-100% 3
                    break;
                case 35:
                    //白天光女
                    NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)player.Center.X, (int)player.Center.Y,636);
                    break;
                case 36: 
                    //雨 1
                    break;
                case 37: 
                    //减速 1


                    break;

                case 38:
                    //史莱姆NPC死亡 2
                   
                    foreach (NPC nPC in Main.ActiveNPCs)
                    {
                        if (nPC.type==678||nPC.type==670||nPC.type==679||nPC.type==680||nPC.type==681||nPC.type==682||nPC.type==683||nPC.type==684)
                        {
                            nPC.life = 0;
                            nPC.checkDead();
                        }
                    }
                    break;
                case 39:
                    //大量的渔夫
                    break;
                case 40:
                    player.velocity.Y -= 200;
                    //地球上投
                    break;
                case 41: 
                    //不能跳
                    break;
                case 42: 
                    //不能飞
                    break;
                case 43:
                    //马赛克8 2
                    OtherResults.SetPixelation(8);
                    break;
                case 44:
                    //马赛克16 3
                    OtherResults.SetPixelation(16);
                    break;
                case 45:
                    //马赛克64 4
                    OtherResults.SetPixelation(64);
                    break;
                case 46:
                    //NPC全死 2
                    foreach (NPC nPC in Main.ActiveNPCs)
                    {
                        if (nPC.friendly)
                        {
                            nPC.life = 0;

                            nPC.checkDead();
                        }
                    }
                    break;
                case 47:
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
                case 48:
                    
                    break;
                case 49:
                    
                    break;
                case 50:
                    
                    break;
                case 51:
                    
                    break;
                case 52: 
                    
                    break;
                case 53:
                    
                    break;
                case 54:
                    
                    break;
                case 55:
                    
                    break;





                default:
                    Main.NewText("These is no penaltly");
                    break;
            }

        }
    }
}
