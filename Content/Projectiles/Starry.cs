using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Luminance.Assets;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace DuanWu.Content.Projectiles
{
    public class Starry:ModProjectile
    {
        public override string Texture => MiscTexturesRegistry.InvisiblePixelPath;

        
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, 1).
                WithOffset(new Vector2(-30f, 10f));

        }

        public override void SetDefaults()
        {
            Projectile.width = 76;
            Projectile.height = 76;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            //Texture2D texture1 = ModContent.Request<Texture2D>("DuanWu/Content/Images/FireNoiseA").Value;

            //Main.spriteBatch.End();
            //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, default, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

            ManagedScreenFilter distortion = ShaderManager.GetFilter("DuanWu.Starry");
            if (!distortion.IsActive)
            {
                distortion.Activate();
            }

            Texture2D texture = ModContent.Request<Texture2D>("DuanWu/Content/Projectiles/InvisiblePixel").Value;
            Main.spriteBatch.Draw(texture,Projectile.Center-Main.screenPosition,null, Color.Transparent, 0f, texture.Size() * 0.5f, 400f, 0, 0f);
            return true;
        }
    }
}
