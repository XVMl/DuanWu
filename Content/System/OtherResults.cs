using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace DuanWu.Content.System
{
    public class OtherResults
    {
        public OtherResults() { }

        public static double SetTime;
        public static bool Setday;
        Matrix groytrans = new(
                    0.299f, 0.587f, 0.114f, 0,
                    0.299f, 0.587f, 0.114f, 0,
                    0.299f, 0.587f, 0.114f, 0,
                    0.299f, 0.587f, 0.114f, 1);
        Matrix protanopia = new(
            0.567f, 0.443f, 0, 0,
            0.558f, 0.442f, 0, 0,
            0, 0.242f, 0, 0.758f,
            0, 0, 0, 1
            );
        Matrix deuteranopia = new(
            0.625f, 0.375f, 0, 0,
            0.720f, 0.280f, 0, 0,
            0, 0.163f, 0.837f, 0,
            0, 0, 0, 1);
        Matrix reversd = new(
            -1, 0, 0, 1,
            0, -1, 0, 1,
            0, 0, -1, 1,
            0, 0, 0, 1);

        public static void QuickSetTime(double time, bool daytime)
        {
            SetTime = time;
            Setday = daytime;
            ModContent.GetInstance<NetTime>().SendPacket(-1, -1);
        }

        public static void QuickSetSpwanRate(bool rate)
        {
            DuanWuPlayer.SetSpwanRate= rate;
            ModContent.GetInstance<NetSpawnRate>().SendPacket(-1, -1);
        }
    }
}
