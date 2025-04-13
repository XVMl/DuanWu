using Luminance.Core.Balancing;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class DuanWuILHook:ModSystem
    {
        #region 反射
        /// <summary>
        /// 使旅行模式的上帝模式阻止死亡失效
        /// </summary>
        static readonly MethodInfo killme = typeof(Player).GetMethod(nameof(Player.KillMe))!;
        static readonly FieldInfo creactivemod = typeof(Player).GetField("creativeGodMode")!;
        /// <summary>
        /// 使重写了ModPlayer等类的PreKill方法的阻止死亡失效；
        /// </summary>
        static readonly MethodInfo prekill = typeof(PlayerLoader).GetMethod(nameof(PlayerLoader.PreKill))!;
        /// <summary>
        /// 使重写了ModNPC等类的CheckDead方法的阻止死亡失效；
        /// </summary>
        static readonly MethodInfo checkdead = typeof(NPCLoader).GetMethod(nameof(NPCLoader.CheckDead))!;
        //static readonly FieldInfo fieldInfo = typeof(NPC).GetField(nameof(NPC.timeLeft));
        static readonly FieldInfo hookcheckdead = typeof(NPCLoader).GetField("HookCheckDead", BindingFlags.NonPublic | BindingFlags.Static)!;
        #endregion

        public override void OnModLoad()
        {
            if (Main.dedServ)
            {
                return;
            }

            try
            {
                IL_Player.KillMe += IL_Player_KillMe;
                MonoModHooks.Modify(prekill, IL_PlayerLoader_PreKill);
                //MonoModHooks.Modify(checkdead, IL_NPCLoader_CheckDead);
                MonoModHooks.Add(checkdead, ON_NPCLoader_CheckDead);
                Mod.Logger.Info("Add IL ON Hook success");    
            }
            catch (Exception e)
            {
                ModContent.GetInstance<DuanWu>().Logger.Error("Add IL ON HOOK Error!", e);
            }
        }
        /// <summary>
        /// 在特定条件下将creativeGodMode检测跳过
        /// </summary>
        static void IL_Player_KillMe(ILContext iL)
        {
            try
            {
                ILCursor cursor = new(iL);
                cursor.GotoNext(MoveType.AfterLabel,
                    i => i.MatchLdarg0(),
                    i => i.MatchLdfld(creactivemod)
                    );
                cursor.EmitLdarg1();
                cursor.EmitLdcR8(5836721);
                cursor.EmitCeq();
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Brtrue_S, iL.Instrs[cursor.Index + 3]);
                }
            catch (Exception e)
            {
                ModContent.GetInstance<DuanWu>().Logger.Error("Skip godmod error",e);
                throw;
            }
        }

        static void IL_PlayerLoader_PreKill(ILContext iL)
        {
            try
            {
                ILCursor cursor = new(iL);
                cursor.GotoNext(MoveType.AfterLabel,
                    i=>  i.MatchLdloc0(),
                    i => i.MatchRet()
                    );
                cursor.EmitLdarg1(); 
                cursor.EmitLdcR8(5836721);
                cursor.EmitCeq();
                cursor.EmitLdloc0();
                cursor.EmitOr();
                cursor.EmitStloc0();
            }
            catch (Exception e)
            {
                ModContent.GetInstance<DuanWu>().Logger.Error("Skip godmod error", e);
                throw;
            }
        }

        public delegate bool ONCheckDead(NPC npc);

        private static bool ON_NPCLoader_CheckDead(ONCheckDead orig,NPC npc)
        {
            return true;
        }

        static void IL_NPCLoader_CheckDead(ILContext iL)
        {
            try
            {
                ILCursor cursor = new(iL);
                cursor.GotoNext(MoveType.AfterLabel,
                    i => i.MatchRet()
                    );
                cursor.EmitStloc0();
                cursor.EmitLdcI4(1);
            }
            catch (Exception e)
            {
                ModContent.GetInstance<DuanWu>().Logger.Error("Skip godmod error", e);
                throw;
            }
        }

    }
}
