using DuanWu.Content.MyUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuanWu.Content.Items
{
    public class ZongZi : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = 0;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 2;
            Item.consumable = true;
            ItemID.Sets.ItemNoGravity[Item.type] = false;
            Item.ResearchUnlockCount = 0;
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
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive|| DuanWuPlayer.WaitingForQuestionEnd)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {

        }

        public override bool? UseItem(Player player)
        {
            LanguageHelper.SetQuestion();
            if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                }
            }
            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/UI/ZongZi").Value;
            spriteBatch.Draw(tex, position, null, Color.White, 0f, tex.Size() / 2, 0.1f, SpriteEffects.None, 0);
            return false;
        }

    }
}
