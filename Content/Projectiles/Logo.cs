using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DuanWu.Content.Projectiles
{
    public class Logo:ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = 486;
            Projectile.height = 142;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.Hitbox.Intersects(Projectile.Hitbox))
                {
                    npc.life = 0;
                    npc.checkDead();
                }
            }
            foreach (var item in Main.ActivePlayers)
            {
                if (item.Hitbox.Intersects(Projectile.Hitbox)&&Projectile.owner!=item.whoAmI)
                {
                    item.KillMe(new PlayerDeathReason(), 5836721, 1);
                }
            }
            foreach (var item in Main.ActiveProjectiles)
            {
                if (item.whoAmI!=Projectile.whoAmI&&item.Hitbox.Intersects(Projectile.Hitbox))
                {
                    item.active = false;
                }
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0);
            return false;
        }

    }
}
