using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DuanWu.Content.UI
{
    internal class DrawHitBox : UIState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, default, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
            Texture2D tex = ModContent.Request<Texture2D>("DuanWu/Content/UI/WhiteScreen").Value;
            DrawPlayHitBox(tex,duanWuPlayer);
            DrawPorjectileHitBox(tex,duanWuPlayer);
            DrawNPCHitBox(tex,duanWuPlayer);
        }

        private void DrawPlayHitBox(Texture2D tex,DuanWuPlayer duanWuPlayer)
        {
            if (!duanWuPlayer.ShowPlayHitBox)
            {
                return;
            }

            List<Rectangle> play= new List<Rectangle>();
            foreach(Player player in Main.player)
            {
                if (player.active)
                {
                    play.Add(player.Hitbox);
                }
            }
            foreach(Rectangle hitbox in play)
            {
                Main.spriteBatch.Draw(tex, new Rectangle(hitbox.X - (int)Main.screenPosition.X, hitbox.Y - (int)Main.screenPosition.Y, hitbox.Width, hitbox.Height), null, Color.Green * 0.7f, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

        }

        private void DrawNPCHitBox(Texture2D tex, DuanWuPlayer duanWuPlayer)
        {
            if (!duanWuPlayer.ShowNPCHitBox)
            {
                return;
            }

            List<Rectangle> firendly = [];
            List<Rectangle> enemy = [];
            foreach (NPC nPC in Main.ActiveNPCs)
            {
                if (nPC.friendly)
                {
                    firendly.Add(nPC.Hitbox);
                }
                else
                {
                    enemy.Add(nPC.Hitbox);
                }
            }

            foreach (Rectangle x in enemy)
            {
                Main.spriteBatch.Draw(tex, new Rectangle(x.X - (int)Main.screenPosition.X, x.Y - (int)Main.screenPosition.Y, x.Width, x.Height), null, Color.Red * 0.7f, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
            foreach (Rectangle x in firendly)
            {
                Main.spriteBatch.Draw(tex, new Rectangle(x.X - (int)Main.screenPosition.X, x.Y - (int)Main.screenPosition.Y, x.Width, x.Height), null, Color.Green * 0.7f, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

        }

        private void DrawPorjectileHitBox(Texture2D tex, DuanWuPlayer duanWuPlayer)
        {
            if (!duanWuPlayer.ShowProjectileBox)
            {
                return; 
            }
            List<Rectangle> enmyprojectile = [];
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active)
                {
                    enmyprojectile.Add(projectile.Hitbox);
                }
            }
            foreach (Rectangle x in enmyprojectile)
            {
                Main.spriteBatch.Draw(tex, new Rectangle(x.X - (int)Main.screenPosition.X, x.Y - (int)Main.screenPosition.Y, x.Width, x.Height), null, Color.Yellow * 0.4f, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

        }



    }
}
