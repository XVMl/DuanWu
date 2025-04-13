using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
namespace DuanWu.Content.Items
{
    internal class Logo:ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = int.MaxValue - 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 320;
            Item.height = 320;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Logo>();
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            //target.active = !target.active;
        }

        public override void HoldItemFrame(Player player)
        {
            base.HoldItemFrame(player);
        }


        //public override void HoldItem(Player player)
        //{
        //    foreach (NPC nPC in Main.ActiveNPCs)
        //    {
        //        if (nPC.Hitbox.Intersects(base.Item.Hitbox))
        //        {
        //            Main.NewText("HIT");
        //        }
        //    }
        //    base.HoldItem(player);
        //}

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {

            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }
        public override bool CanHitPvp(Player player, Player target)
        {
            
            return base.CanHitPvp(player, target);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/Items/Logo").Value;
            spriteBatch.Draw(tex, position, null, Color.White, 0f, tex.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/Items/Logo").Value;
            spriteBatch.Draw(tex, Item.Center, null, Color.White, 0f, tex.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
    }
}
