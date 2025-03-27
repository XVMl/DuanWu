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

        public static void SetCamera(float intensity)
        {
            if (!Filters.Scene["DuanWuShader:Zhuan"].IsActive())
            {
                Filters.Scene.Activate("DuanWuShader:Zhuan");        
            }
            if (Filters.Scene["DuanWuShader:Zhuan"].IsActive())
            {
                Filters.Scene["DuanWuShader:Zhuan"].GetShader().Shader.Parameters["Reve"].SetValue(intensity);        
            }
        }
        
        public static void SetBlur()
        {
            if (!Filters.Scene["DuanWuShader:Blur"].IsActive())
            {
                Filters.Scene.Activate("DuanWuShader:Blur");
            }
        }

        public static void SetPixelation(float intensity)
        {
            if (!Filters.Scene["DuanWuShader:Pixelation"].IsActive())
            {
                Filters.Scene.Activate("DuanWuShader:Pixelation");
                Filters.Scene["DuanWuShader:Pixelation"].GetShader().Shader.Parameters["intensity"].SetValue(intensity);
            }
        }


        public static void GiganticTransFormation()
        {

        }

        public static void QuickSetTime(double time, bool daytime)
        {
            SetTime = time;
            Setday = daytime;
            ModContent.GetInstance<NetTime>().NetSeed(-1, -1);
        }

        public static void QuickSetSpwanRate(bool rate)
        {
            DuanWuPlayer.SetSpwanRate= rate;
            ModContent.GetInstance<NetSpawnRate>().NetSeed(-1, -1);
        }
    }
}
