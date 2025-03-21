using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace DuanWu.Content.UI
{
    public class Lisao : UIState
    {
        public UIGrid LisaochoiceLisst;
        private float Height;
        public override void OnInitialize()
        {
            LisaochoiceLisst = new UIGrid();
            LisaochoiceLisst.Width.Set(1250f, 0);
            LisaochoiceLisst.Height.Set(500f, 0);
            LisaochoiceLisst.VAlign = 0.7f;
            LisaochoiceLisst.HAlign = 0.5f;
            LisaochoiceLisst.ListPadding = 80f;
        }

        public void AddEditor()
        {
            for (int i = 0; i < 8; i++)
            {
                LisaochoiceLisst.Add(new MyButton(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoChoiceText[i], i, ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton")));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                Append(LisaochoiceLisst);
            }
            else
            {
                LisaochoiceLisst.Clear();
                LisaochoiceLisst.Remove();
                return;
            }
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime+1 == DuanWuPlayer.AnswerQuestionTime*60)
            {
                AddEditor();
            }
            
            
        }

    }

    internal class MyButton:UIImageButton
    {
        private int Number;

        public UIText uiText;
        public MyButton(string text,int x,Asset<Texture2D> texture) : base(texture)
        {
            this.Number = x;
            Height.Set(32, 0);
            Width.Set(550, 0);
            uiText = new UIText(text);
            uiText.HAlign =uiText.VAlign = 0.5f;
            Append(uiText);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Main.NewText(Number);
            base.LeftClick(evt);
        }

    }

}
