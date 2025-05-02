using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using DuanWu.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using Luminance;
using Luminance.Core.Graphics;
namespace DuanWu.Content.Items
{
    public class SuperWoodenSword:ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = int.MaxValue-1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        //public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        //{
        //    Main.spriteBatch.End();  
        //    Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
        //    //GameShaders.Misc["SliverBlade"].UseImage1(ModContent.Request<Texture2D>("DuanWu/Content/Images/FireNoiseA"));
        //    //GameShaders.Misc["SliverBlade"].UseImage2(ModContent.Request<Texture2D>("DuanWu/Content/Images/univers"));
        //    //GameShaders.Misc["SliverBlade"].Apply();
        //    ManagedShader managed = ShaderManager.GetShader("DuanWu.ExamplePrimShader");
        //    //managed.TrySetParameter("pixelationFactor", Vector2.One * 0.5f);
        //    //managed.SetTexture(FireNoiseB, 1, SamplerState.LinearWrap);
        //    managed.Apply();
        //    Texture2D texture = ModContent.Request<Texture2D>("DuanWu/Content/Items/Asditems").Value;
        //    spriteBatch.Draw(texture, position, null, Color.White, 0f, texture.Size() / 2, 0.7f, SpriteEffects.None, 0);
        //    Main.spriteBatch.End();
        //    Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
        //}


        //public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //    Main.spriteBatch.End();
        //    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //    GameShaders.Misc["ArmorBasic"].UseImage1(ModContent.Request<Texture2D>("DuanWu/Content/Images/FireNoiseA"));
        //    GameShaders.Misc["ArmorBasic"].Apply();
        //    Texture2D texture = ModContent.Request<Texture2D>("DuanWu/Content/Items/Asditems").Value;
        //    spriteBatch.Draw(texture, Item.position - Main.screenPosition + new Vector2(6f, 24f), null, Color.White, 0f, texture.Size() / 2, 0.5f, SpriteEffects.None, 0);
        //    Main.spriteBatch.End();
        //    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        //    return false;
        //}

    }
}
