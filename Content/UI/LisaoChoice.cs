using DuanWu.Content.System;
using DuanWu.Content.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace DuanWu.Content.UI
{
    public class LisaoChoice:UIState
    {
        private UIElement area;
        private UIImageButton choice1;
        private UIImageButton choice2;
        private UIImageButton choice3;
        private UIImageButton choice4;
        private UIImageButton choice5;
        private UIImageButton choice6;
        private UIImageButton choice7;
        private UIImageButton choice8;
        private UIImageButton imageButton;
        private UIText choice1Text;
        private UIText choice2Text;
        private UIText choice3Text;
        private UIText choice4Text;
        private UIText choice5Text;
        private UIText choice6Text;
        private UIText choice7Text;
        private UIText choice8Text;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(0f, 0f);
            area.Top.Set(100f, 0f);
            area.Width.Set(800f, 0f);
            area.Height.Set(550f, 0f);
            area.HAlign = area.VAlign = 0.5f;
            choice1 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice1.Left.Set(144f, 0f);
            choice1.Top.Set(32f, 0f);
            choice1.Width.Set(386f, 0f);
            choice1.Height.Set(32f, 0f);
            choice1.OnLeftClick += new UIElement.MouseEvent(QAoption1Click);
            choice5 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice5.Left.Set(334f, 0f);
            choice5.Top.Set(32f, 0f);
            choice5.Width.Set(386f, 0f);
            choice5.Height.Set(32f, 0f);
            choice5.OnLeftClick += new UIElement.MouseEvent(QAoption5Click);
            choice2 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice2.Left.Set(144f, 0f);
            choice2.Top.Set(164f, 0f);
            choice2.Width.Set(386f, 0f);
            choice2.Height.Set(32f, 0f);
            choice2.OnLeftClick += new UIElement.MouseEvent(QAoption2Click);
            choice6 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice6.Left.Set(334f, 0f);
            choice6.Top.Set(164f, 0f);
            choice6.Width.Set(386f, 0f);
            choice6.Height.Set(32f, 0f);
            choice6.OnLeftClick += new UIElement.MouseEvent(QAoption6Click);
            choice3 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice3.Left.Set(144f, 0f);
            choice3.Top.Set(296f, 0f);
            choice3.Width.Set(386f, 0f);
            choice3.Height.Set(32f, 0f);
            choice3.OnLeftClick += new UIElement.MouseEvent(QAoption3Click);
            choice7 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice7.Left.Set(334f, 0f);
            choice7.Top.Set(296f, 0f);
            choice7.Width.Set(386f, 0f);
            choice7.Height.Set(32f, 0f);
            choice7.OnLeftClick += new UIElement.MouseEvent(QAoption7Click);
            choice4 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice4.Left.Set(144f, 0f);
            choice4.Top.Set(428f, 0f);
            choice4.Width.Set(386f, 0f);
            choice4.Height.Set(32f, 0f);
            choice4.OnLeftClick += new UIElement.MouseEvent(QAoption4Click);
            choice8 = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            choice8.Left.Set(344f, 0f);
            choice8.Top.Set(428f, 0f);
            choice8.Width.Set(386f, 0f);
            choice8.Height.Set(32f, 0f);
            choice8.OnLeftClick += new UIElement.MouseEvent(QAoption8Click);
            choice1Text = new UIText("", 1f, false);
            choice1Text.Width.Set(456f, 0f);
            choice1Text.Height.Set(1f, 0f);
            choice1Text.Top.Set(40f, 0f);
            choice1Text.Left.Set(174f, 0f);
            choice1Text.IgnoresMouseInteraction = true;
            choice2Text = new UIText("", 1f, false);
            choice2Text.Width.Set(456f, 0f);
            choice2Text.Height.Set(1f, 0f);
            choice2Text.Top.Set(172f, 0f);
            choice2Text.Left.Set(174f, 0f);
            choice2Text.IgnoresMouseInteraction = true;
            choice3Text = new UIText("", 1f, false);
            choice3Text.Width.Set(456f, 0f);
            choice3Text.Height.Set(1f, 0f);
            choice3Text.Top.Set(304f, 0f);
            choice3Text.Left.Set(174f, 0f);
            choice3Text.IgnoresMouseInteraction = true;
            choice4Text = new UIText("", 1f, false);
            choice4Text.Width.Set(456f, 0f);
            choice4Text.Height.Set(1f, 0f);
            choice4Text.Top.Set(436f, 0f);
            choice4Text.Left.Set(174f, 0f);
            choice4Text.IgnoresMouseInteraction = true;
            choice5Text = new UIText("", 1f, false);
            choice5Text.Width.Set(456f, 0f);
            choice5Text.Height.Set(1f, 0f);
            choice5Text.Top.Set(40f, 0f);
            choice5Text.Left.Set(274f, 0f);
            choice5Text.IgnoresMouseInteraction = true;
            choice6Text = new UIText("", 1f, false);
            choice6Text.Width.Set(456f, 0f);
            choice6Text.Height.Set(1f, 0f);
            choice6Text.Top.Set(172f, 0f);
            choice6Text.Left.Set(274f, 0f);
            choice6Text.IgnoresMouseInteraction = true;
            choice7Text = new UIText("", 1f, false);
            choice7Text.Width.Set(456f, 0f);
            choice7Text.Height.Set(1f, 0f);
            choice7Text.Top.Set(304f, 0f);
            choice7Text.Left.Set(274f, 0f);
            choice7Text.IgnoresMouseInteraction = true;
            choice8Text = new UIText("", 1f, false);
            choice8Text.Width.Set(456f, 0f);
            choice8Text.Height.Set(1f, 0f);
            choice8Text.Top.Set(436f, 0f);
            choice8Text.Left.Set(274f, 0f);
            choice8Text.IgnoresMouseInteraction = true;
            imageButton = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/Button"));
            imageButton.Left.Set(600f, 0f);
            imageButton.Top.Set(0f, 0f);
            imageButton.Height.Set(32f, 0f);
            imageButton.Width.Set(80f, 0f);
            imageButton.OnLeftClick += new MouseEvent(OnMouseClike);
            area.Append(choice1);
            area.Append(choice2);
            area.Append(choice3);
            area.Append(choice4);
            area.Append(choice5);
            area.Append(choice6);
            area.Append(choice7);
            area.Append(choice8);
            area.Append(choice1Text);
            area.Append(choice2Text);
            area.Append(choice3Text);
            area.Append(choice4Text);
            area.Append(choice5Text);
            area.Append(choice6Text);
            area.Append(choice7Text);
            area.Append(choice8Text);
            Append(area);
        }


        private void QAoption1Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 0;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption2Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 1;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption3Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 2;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption4Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 3;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption5Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 4;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption6Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 5;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption7Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 6;
            duanWuPlayer.counttime = 0;
        }

        private void QAoption8Click(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime <= 0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = 7;
            duanWuPlayer.counttime = 0;
        }

        private void OnMouseClike(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            duanWuPlayer.LisaoChoiceActive = true;
            duanWuPlayer.LisaoActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (DuanWuPlayer.Hardmode)
            {
                area.Append(choice5);
                area.Append(choice6);
                area.Append(choice7);
                area.Append(choice8);
                area.Append(choice5Text);
                area.Append(choice6Text);
                area.Append(choice7Text);
                area.Append(choice8Text);
                choice1.Left.Set(-244f, 0f);
                choice2.Left.Set(-244f, 0f);
                choice3.Left.Set(-244f, 0f);
                choice4.Left.Set(-244f, 0f);
                choice1Text.Left.Set(-220f, 0f);
                choice2Text.Left.Set(-220f, 0f);
                choice3Text.Left.Set(-220f, 0f);
                choice4Text.Left.Set(-220f, 0f);
                choice5Text.SetText(duanWuPlayer.LisaoChoiceText[4] ?? "");
                choice6Text.SetText(duanWuPlayer.LisaoChoiceText[5] ?? "");
                choice7Text.SetText(duanWuPlayer.LisaoChoiceText[6] ?? "");
                choice8Text.SetText(duanWuPlayer.LisaoChoiceText[7] ?? "");
            }
            else
            {
                choice5.Remove();
                choice6.Remove();
                choice7.Remove();
                choice8.Remove();
                choice5Text.Remove();
                choice6Text.Remove();
                choice7Text.Remove();
                choice8Text.Remove();
                choice1.Left.Set(144f, 0f);
                choice2.Left.Set(144f, 0f);
                choice3.Left.Set(144f, 0f);
                choice4.Left.Set(144f, 0f);
                choice1Text.Left.Set(174f, 0f);
                choice2Text.Left.Set(174f, 0f);
                choice3Text.Left.Set(174f, 0f);
                choice4Text.Left.Set(174f, 0f);
            }
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                Append(area);
                if (duanWuPlayer.LisaoChoiceActive)
                {
                    area.Append(choice1);
                    area.Append(choice2);
                    area.Append(choice3);
                    area.Append(choice4);
                    area.Append(choice1Text);
                    area.Append(choice2Text);
                    area.Append(choice3Text);
                    area.Append(choice4Text);
                    imageButton.Remove();

                    choice1Text.SetText(duanWuPlayer.LisaoChoiceText[0] ?? "");
                    choice2Text.SetText(duanWuPlayer.LisaoChoiceText[1] ?? "");
                    choice3Text.SetText(duanWuPlayer.LisaoChoiceText[2] ?? "");
                    choice4Text.SetText(duanWuPlayer.LisaoChoiceText[3] ?? "");

                }
                else
                {
                    choice1.Remove();
                    choice2.Remove();
                    choice3.Remove();
                    choice4.Remove();
                    choice1Text.Remove();
                    choice2Text.Remove();
                    choice3Text.Remove();
                    choice4Text.Remove();
                    area.Append(imageButton);
                }
            }
            else
            {
                area.Remove();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/WhiteScreen"), hitbox, Color.White*0f);
        }

    }
}
