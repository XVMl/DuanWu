using Luminance.Assets;
using Luminance.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace DuanWu.Content.Projectiles
{
    internal class Error:ModProjectile
    {
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
            Projectile.hide = true;
            Projectile.alpha = 255;
        }
        public override void OnKill(int timeLeft)
        {
            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Error");
            if (!distortion.IsActive)
            {
                distortion.Activate();
            }
        }
    }
}
