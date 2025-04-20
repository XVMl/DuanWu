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
using DuanWu.Content.System;
using Luminance.Common.Utilities;

namespace DuanWu.Content.UI
{
    public class LisaoQuestion:BaseUIState
    {
        private UIElement area;
        private UIText FirstSentence;
        private UIText DownSentence;
        private UIText conunttime;
        private UIText answer;
        private float visibility = 0.5f;

        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Width.Set(700f, 0f);
            area.Height.Set(150f, 0f);
            area.HAlign = 0.5f; area.VAlign = 0.1f;
            FirstSentence = new UIText("", 0.7f, true);
            conunttime = new UIText("", 0.7f, true);
            DownSentence = new UIText("", 0.7f, true);
            answer = new UIText("", 0.7f, true);
            FirstSentence.Height.Set(1f, 0f);
            FirstSentence.Width.Set(600f, 0f);
            FirstSentence.HAlign = 0.5f;
            //FirstSentence.Left.Set(0f, 0f);
            FirstSentence.Top.Set(75f, 0f);
            FirstSentence.IgnoresMouseInteraction = true;
            conunttime.Height.Set(1f, 0f);
            conunttime.Width.Set(600f, 0f);
            conunttime.Left.Set(300f, 0f);
            //conunttime.HAlign = 0.5f;
            conunttime.Top.Set(25f, 0f);
            conunttime.IgnoresMouseInteraction = true;
            DownSentence.Height.Set(1f, 0f);
            DownSentence.Width.Set(600f, 0f);
            //DownSentence.Left.Set(0f, 0f);
            DownSentence.HAlign = 0.5f;
            DownSentence.Top.Set(25f, 0f);
            DownSentence.IgnoresMouseInteraction = true;
            answer.Height.Set(1f, 0f);
            answer.Width.Set(600f, 0f);
            answer.HAlign = 0.5f;
            answer.Top.Set(25f, 0f);
            answer.TextColor = Color.Black*0;
            answer.IgnoresMouseInteraction = true;
            area.Append(FirstSentence);
            area.Append(DownSentence);
            area.Append(conunttime);
            area.Append(answer);
        }

        public override void Update(GameTime gameTime)
        {
            DuanWuPlayer player = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (!player.LisaoActive)
            {
                visibility = 0;
                area.Remove();
                return;
            }

            Append(area);
            visibility = 0.5f;
            
            if (player.counttime > 0)
            {
                conunttime.SetText((player.counttime / 60 + 1).ToString());
                answer.SetText("");
            }
            else
            {
                answer.ShadowColor= Color.Black * Utilities.InverseLerp(0f, 180, 316 - player.ShowAnswer);
                answer.TextColor = Color.White * Utilities.InverseLerp(0f, 180, 316 - player.ShowAnswer);
                conunttime.SetText("0");
                answer.SetText(player.QuestionAnswer);
            }

            if (player.lisaoquestion == 0)
            {
                DownSentence.SetText("__________");
                FirstSentence.SetText(player.LisaoQuestionText ?? "");
                answer.Top.Set(25f, 0f);
            }
            else
            {
                FirstSentence.SetText("__________");
                DownSentence.SetText(player.LisaoQuestionText ?? "");
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
            Texture2D time = ModContent.Request<Texture2D>("DuanWu/Content/UI/Time").Value;
            Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/LisaoQuestion"), hitbox, Color.White*visibility);
            //spriteBatch.Draw(time, new Rectangle(hitbox.X+100,hitbox.Y+120,time.Width,time.Height), Color.White * visibility);
        }
    }
}
