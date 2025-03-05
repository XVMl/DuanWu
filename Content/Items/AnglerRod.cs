using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using DuanWu.Content.Projectiles;
namespace DuanWu.Content.Items
{
    public class AnglerRod : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 42;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = 1;
            Item.UseSound = new SoundStyle?(SoundID.Item1);
            Item.fishingPole = 25;
            Item.shootSpeed = 14.5f;
            Item.shoot = ModContent.ProjectileType<AnglerBobber>();
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = 3;
        }

        public override void ModifyFishingLine(Terraria.Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            base.ModifyFishingLine(bobber, ref lineOriginOffset, ref lineColor);
        }

      

    }
}
