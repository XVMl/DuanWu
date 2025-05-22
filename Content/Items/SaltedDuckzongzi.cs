using DuanWu.Content.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using DuanWu.Content.System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Luminance.Common.Utilities;
using DuanWu.Content.Buffs;

namespace DuanWu.Content.Items
{
    internal class SaltedDuckzongzi:ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 34;
            Item.maxStack = 9999;
            Item.rare = 0;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ZongYe>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SaltedDuckEgg>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/UI/SuStyleZongzi").Value;
            spriteBatch.Draw(tex, position, null, Color.White, 0f, tex.Size() / 2, 0.1f, SpriteEffects.None, 0);
            return false;
        }
        //public override void OnConsumeItem(Player player)
        //{
        //    player.AddBuff(206, Utilities.SecondsToFrames(5));
        //    if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
        //    {
        //        if (Main.myPlayer == player.whoAmI)
        //        {
        //            LanguageHelper.SetQuestion();
        //        }
        //    }
        //}

        public override bool? UseItem(Player player)
        {
            if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    player.AddBuff(206, Utilities.MinutesToFrames(5));
                    player.AddBuff(ModContent.BuffType<SaltedDuckZongZiBuff>(), Utilities.MinutesToFrames(5));
                    player.itemTime = Item.useTime;
                    LanguageHelper.SetQuestion();
                }
            }
            return true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/UI/BambooZongzi").Value;
            spriteBatch.Draw(tex, Item.position - Main.screenPosition + new Vector2(5f, 10f), null, Color.White, 0f, tex.Size() / 2, 0.1f, SpriteEffects.None, 0);
            return false;
        }
    }
}
