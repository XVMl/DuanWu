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
        private UIText FirstSentence;
        private UIText DownSentence;
        private UIText conunttime;
        private UIText answer;
        private float visibility = 0.5f;
        public override void OnInitialize()
        {
            area = new UIElement();
            area.Width.Set(800f, 0f);
            area.Height.Set(100f, 0f);
            area.HAlign = 0.5f; area.VAlign = 0.1f;
            FirstSentence = new UIText("", 0.7f, true);
            conunttime = new UIText("", 0.7f, true);
            DownSentence = new UIText("", 0.7f, true);
            answer = new UIText("", 0.7f, true);
            FirstSentence.Height.Set(1f, 0f);
            FirstSentence.Width.Set(600f, 0f);
            FirstSentence.Left.Set(0f, 0f);
            FirstSentence.Top.Set(75f, 0f);
            FirstSentence.IgnoresMouseInteraction = true;
            conunttime.Height.Set(1f, 0f);
            conunttime.Width.Set(600f, 0f);
            conunttime.Left.Set(300f, 0f);
            conunttime.Top.Set(25f, 0f);
            conunttime.IgnoresMouseInteraction = true;
            DownSentence.Height.Set(1f, 0f);
            DownSentence.Width.Set(600f, 0f);
            DownSentence.Left.Set(0f, 0f);
            DownSentence.Top.Set(25f, 0f);
            DownSentence.IgnoresMouseInteraction = true;
            answer.Height.Set(1f, 0f);
            answer.Width.Set(600f, 0f);
            answer.Left.Set(0f, 0f);
            answer.Top.Set(25f, 0f);
            answer.IgnoresMouseInteraction = true;
            area.Append(FirstSentence);
            area.Append(DownSentence);
            area.Append(conunttime);
            area.Append(answer);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                visibility = 0;
                area.Remove();
                return;
            }

            Append(area);
            visibility = 0.5f;
            
            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime > 0)
            {
                conunttime.SetText((Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().counttime / 60 + 1).ToString());
                answer.SetText("");
            }
            else
            {
                conunttime.SetText("0");
                answer.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().QuestionAnswer);
            }

            if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().lisaoquestion == 0)
            {
                DownSentence.SetText("__________");
                FirstSentence.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoQuestionText ?? "");
                answer.Top.Set(25f, 0f);
            }
            else
            {
                FirstSentence.SetText("__________");
                DownSentence.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoQuestionText ?? "");
                answer.Top.Set(75f, 0f);
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
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/Question"), hitbox, Color.White*visibility);
        }
    }
}
