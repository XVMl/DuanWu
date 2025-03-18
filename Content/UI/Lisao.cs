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
    public class MyButton:UIImageButton
    {
        private int Number;

        public UIText uiText;
        private string messsage="23r34";
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

    //public class PlayButton:UIElement
    //{
    //    Color Color = new Color(50, 225, 153);
    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"), new Vector2(Main.screenWidth+20,Main.screenHeight-20)/2, Color.White * 1f);
    //    }

    //}
    public class Lisao : UIState
    {
        public UIGrid LisaochoiceLisst;
        //public PlayButton playButton;
        private float Height;

        public override void OnInitialize()
        {
            LisaochoiceLisst = new UIGrid();
            AddEditor();
            LisaochoiceLisst.Width.Set(1250f, 0);
            LisaochoiceLisst.Height.Set(500f, 0);
            LisaochoiceLisst.VAlign = 0.7f;
            LisaochoiceLisst.HAlign = 0.5f;
            LisaochoiceLisst.ListPadding=100f;
            Append(LisaochoiceLisst);
        }

        private void AddEditor()
        {
            for (int i = 0; i < 8; i++)
            {
                LisaochoiceLisst.Add(new MyButton(i.ToString(),i, ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton")));
            }
        }


        ////public override void Draw(SpriteBatch spriteBatch)
        ////{
        ////    Rectangle hitbox = LisaochoiceLisst.GetInnerDimensions().ToRectangle();
        ////    spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/WhiteScreen"), hitbox, Color.White * 0.5f);
        ////}
    }
}
