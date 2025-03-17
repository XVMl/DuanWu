using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    internal class MyButton:UIElement
    {
        private UIImageButton UIButton;

        private int Number;

        public MyButton(int num) 
        {
            Number=num;
            Main.NewText("!!");
            Width.Set(300, 0);
            Height.Set(50, 0);
            UIButton = new UIImageButton(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"));
            UIButton.Width.Set(386f, 0);
            UIButton.Height.Set(32f, 0);
            UIButton.Top.Set(3f, 0f);
            UIButton.OnLeftClick += new MouseEvent(QAoptionClick);
        }

        private void QAoptionClick(UIMouseEvent evt, UIElement listeningElement)
        {
            DuanWuPlayer.AnswerQuestionTime = Number;
            Main.NewText(Number);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle hitbox = base.GetInnerDimensions().ToRectangle();
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/WhiteScreen"), hitbox, Color.White * 1f);
        }

    }
    public class Lisao : UIState
    {
        public UIGrid LisaochoiceLisst;

        private float Height;

        public override void OnInitialize()
        {
            LisaochoiceLisst = new UIGrid();   
            LisaochoiceLisst.Add(new MyButton(1));
            LisaochoiceLisst.Width.Set(1000f, 0);
            LisaochoiceLisst.Height.Set(500f, 0);
            LisaochoiceLisst.Top.Set(00f, 0);
            Append(LisaochoiceLisst);
        }

        public override void Update(GameTime gameTime)
        {
            LisaochoiceLisst.Add(new MyButton(1));
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle hitbox = LisaochoiceLisst.GetInnerDimensions().ToRectangle();
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/Question"), hitbox, Color.White * 0.5f);
        }
    }
}
