using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Luminance.Core.Graphics;

namespace DuanWu.Content.Items
{
    public class InfiniteUniverseBar:ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
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

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("DuanWu/Content/Items/InfiniteUniverseBar").Value;
            spriteBatch.Draw(texture, position, null, Color.White, 0f, texture.Size() / 2, 0.8f, SpriteEffects.None, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
            //ManagedShader managedShader = ShaderManager.GetShader("DuanWu.ArmorBasic");
            //managedShader.Apply();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, texture.Size() / 2, 0.8f, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
            return false;
        }

        //public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //    Main.spriteBatch.End();
        //    Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
        //    GameShaders.Misc["ArmorBasic"].Apply();
        //    Texture2D texture = ModContent.Request<Texture2D>("DuanWu/Content/Items/InfiniteUniverseBar").Value;
        //    spriteBatch.Draw(texture, position, null, Color.White, 0f, texture.Size() / 2, 1, SpriteEffects.None, 0);
        //    Main.spriteBatch.End();
        //    Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
        //}

    }
}
