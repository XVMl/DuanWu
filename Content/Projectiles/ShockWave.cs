using Luminance.Assets;
using Luminance.Common.Utilities;
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
using static tModPorter.ProgressUpdate;

namespace DuanWu.Content.Projectiles
{
    public class ShockWave:ModProjectile
    {

        private int rippleCount = 1;
        private float rippleSize = 1;
        private int rippleSpeed = 5;
        private float distortStrength = 200f;
        public override string Texture => MiscTexturesRegistry.InvisiblePixelPath;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.hide = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Shockwave");
            if (!distortion.IsActive)
            {
                distortion.Activate();
            }
            if (distortion.IsActive)
            {
                float progress = (180f - Projectile.timeLeft) / 60f;
                distortion.TrySetParameter("screenscalerevise", new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom);
                distortion.TrySetParameter("targetposition", Utilities.WorldSpaceToScreenUV(Projectile.Center));
                distortion.TrySetParameter("uColor", new Vector3(rippleCount, rippleSize, rippleSpeed));
                distortion.TrySetParameter("uProgress", progress);
                distortion.TrySetParameter("uOpacity", distortStrength * (1 - progress / 3f));
            }
        }

        //public override void AI()
        //{
        //    ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Shockwave");
        //    if (Projectile.timeLeft==1)
        //    {
        //        if (distortion.IsActive)
        //        {
        //            distortion.Deactivate();
        //            Main.NewText("end");
        //        }
        //    }
        //    else if (Projectile.timeLeft <= 180)
        //    {

        //        if (Projectile.ai[0] == 0)
        //        {
        //            Projectile.ai[0] = 1;
        //            //if (!Filters.Scene["DuanWuShader:Shockwave"].IsActive())
        //            //{
        //            //    Filters.Scene.Activate("DuanWuShader:Shockwave", Projectile.Center);
        //            //}

        //            if (!distortion.IsActive)
        //            {
        //                //distortion.TrySetParameter("uColor",new Vector3( rippleCount, rippleSize, rippleSpeed));
        //                //distortion.TrySetParameter("targetposition", Utilities.WorldSpaceToScreenUV(Projectile.Center));
        //                distortion.Activate();
        //                Main.NewText("stat");
        //            }
        //        }

        //        if (distortion.IsActive)
        //        {
        //            Main.NewText("updata");
        //            //distortion.TrySetParameter("targetposition", Utilities.WorldSpaceToScreenUV(Projectile.Center));
        //            //distortion.TrySetParameter("uColor", new Vector3(rippleCount, rippleSize, rippleSpeed));
        //            //float progress = (180f - Projectile.timeLeft) / 60f;
        //            //distortion.TrySetParameter("uProgress", progress);
        //            //distortion.TrySetParameter("uOpacity", distortStrength * (1 - progress / 3f));
        //            distortion.Activate();
        //        }
        //        //distortion.TrySetParameter("uOpacity", distortStrength * (1 - progress / 3f));
        //        //distortion.TrySetParameter("uProgress", progress);

        //        //if (Filters.Scene["DuanWuShader:Shockwave"].IsActive())
        //        //{
        //        //    Filters.Scene["DuanWuShader:Shockwave"].GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
        //        //    float progress = (180f - Projectile.timeLeft) / 60f;
        //        //    Filters.Scene["DuanWuShader:Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
        //        //}
        //    }
        //}

        public override bool PreDraw(ref Microsoft.Xna.Framework.Color lightColor)
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {

        }


    }
}
