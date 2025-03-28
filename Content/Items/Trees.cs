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

namespace DuanWu.Content.Items
{
    public class Trees:ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = int.MaxValue - 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 320;
            Item.height = 320;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/Items/Trees").Value;
            spriteBatch.Draw(tex, position, null, Color.White, 0f, tex.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }

    }
}
