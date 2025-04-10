using DuanWu.Content.System;
using DuanWu.Content.MyUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using System;
using DuanWu.Content.Buffs;
using System.Net.Sockets;
using Luminance;
using DuanWu.Content.Projectiles;
using Luminance.Core.Graphics;
using Luminance;
using Luminance.Common.Utilities;
using Luminance.Core.Cutscenes;
using Luminance.Core.Sounds;
using Terraria.Audio;
using DuanWu.Content.UI;
namespace DuanWu.Content.Items
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Asditems : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.asd.hjson' file.
        public override void SetDefaults()
        {
            Item.damage = 666;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            //Item.shoot = ProjectileID.MagicMissile;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
  
        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer==player.whoAmI)
            {
                Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy--;
                TestUI.AddElement();
                if (player.altFunctionUse == 2)
                {

                }
                //NetScoreboard.SubmitPacket();
            }


            //ushort selected = TileID.WoodBlock;
            //Rectangle safeBox;
            //safeBox.X = (int)Main.LocalPlayer.Center.X / 16 + 1;
            //safeBox.Y = (int)Main.LocalPlayer.Center.Y / 16 + 1;

            //safeBox.X = (int)Main.MouseWorld.X / 16;
            //safeBox.Y = (int)Main.MouseWorld.Y / 16;

            //Tile tilePtr = Framing.GetTileSafely(safeBox.X, safeBox.Y);
            //Main.NewText(tilePtr.TileType);

            //if (Main.tileFrameImportant[selected])
            //{
            //    TileObject.CanPlace(safeBox.X, safeBox.Y, selected, 0, 0, out TileObject to);
            //    TileObject.Place(to);
            //}
            //else
            //{
            //    Tile tilePtr = Framing.GetTileSafely(safeBox.X, safeBox.Y);
            //    tilePtr.HasTile = true;
            //    Main.NewText(tilePtr.TileType);
            //    tilePtr.TileType = selected;
            //    tilePtr.Slope = 0;

            //    if (Main.netMode == 2)
            //    {
            //        NetMessage.SendTileSquare(-1, safeBox.X, safeBox.Y, 0);
            //    }
            //    else
            //    {
            //        WorldGen.SquareTileFrame(safeBox.X, safeBox.Y, true);
            //    }

            //}
            return true;
        }

    }


}

