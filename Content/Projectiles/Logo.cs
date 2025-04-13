using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
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
        }

    }
}
