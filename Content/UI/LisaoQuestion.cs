using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria;

namespace DuanWu.Content.UI
{
    public class LisaoQuestion:UIState
    {
        private UIElement area;
        private UIText questionText;
        private UIText questionText1;
        private UIText conunttime;
        private UIText answer; 
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(0f, 0f);
            area.Top.Set(-250f, 0f);
            area.Width.Set(800f, 0f);
            area.Height.Set(100f, 0f);
            area.HAlign = area.VAlign = 0.5f;
            questionText = new UIText("A Bssdfvdf", 0.7f, true);
            questionText.Height.Set(1f, 0f);
            questionText.Width.Set(600f, 0f);
            questionText.Left.Set(0f, 0f);
            questionText.Top.Set(75f, 0f);
            questionText.IgnoresMouseInteraction = true;
            conunttime = new UIText("23", 0.7f, true);
            conunttime.Height.Set(1f, 0f);
            conunttime.Width.Set(600f, 0f);
            conunttime.Left.Set(300f, 0f);
            conunttime.Top.Set(25f, 0f);
            conunttime.IgnoresMouseInteraction = true;
            questionText1 = new UIText("", 0.7f, true);
            questionText1.Height.Set(1f, 0f);
            questionText1.Width.Set(600f, 0f);
            questionText1.Left.Set(0f, 0f);
            questionText1.Top.Set(25f, 0f);
            questionText1.IgnoresMouseInteraction = true;
            answer = new UIText("asdffg", 0.7f, true);
            answer.Height.Set(1f, 0f);
            answer.Width.Set(600f, 0f);
            answer.Left.Set(0f, 0f);
            answer.Top.Set(25f, 0f);
            answer.IgnoresMouseInteraction = true;
            area.Append(questionText);
            area.Append(questionText1);
            area.Append(conunttime);
            area.Append(answer);
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                Append(area);
            }
            else
            {
                area.Remove();
                return;
            }
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime > 0)
            {
                conunttime.SetText( (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime/60+1).ToString());
                answer.SetText("");
            }
            else
            {
                conunttime.SetText("0");
                answer.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().QuestionAnswer);
            }

            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().lisaoquestion==0)
            {
                questionText.SetText("__________");
                questionText1.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoQuestionText ?? "");
                answer.Top.Set(75f, 0f);
            }
            else
            {
                questionText1.SetText("__________");
                questionText.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoQuestionText ?? "");
                answer.Top.Set(25f, 0f);
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
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/Question"), hitbox, Color.White*0.5f );
        }
    }
}
