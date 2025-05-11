using DuanWu.Content.System;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace DuanWu.Content.UI
{

    public class TestUI : BaseUIState
    {
        public static MyGrid iGrid = new();
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";
        public override bool IsLoaded() =>false;
        public override void OnInitialize()
        {
            iGrid = new MyGrid();
            iGrid.Width.Set(200f, 0);
            iGrid.Height.Set(350f, 0);
            iGrid.HAlign = 0f;
            iGrid.VAlign = 0.5f;
            Append(iGrid);
        }

        public static void ADD()
        {
            iGrid.Add(new ScoreboardElement("12", 0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            //if (!DuanWuPlayer.WaitingForQuestionEnd)
            //{
            //    return;
            //}
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Rectangle rectangle2 = new((int)iGrid.GetDimensions().X, (int)iGrid.GetDimensions().Y, (int)iGrid.GetDimensions().Width, (int)iGrid.GetDimensions().Height - 31);
            Rectangle rectangle = new((int)iGrid.GetDimensions().X, (int)iGrid.GetDimensions().Y - 31, (int)iGrid.GetDimensions().Width, 31);
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/Scoreboard"), rectangle, new Rectangle(0, 0, 104, 31), Color.White);
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/Scoreboard"), rectangle2, new Rectangle(0, 31, 104, 225), Color.White);
            Utils.DrawBorderString(spriteBatch, DuanWuPlayer.ScoreboardText, iGrid.GetDimensions().ToRectangle().TopLeft() + new Vector2(70, -25), Color.White);
        }
    }

}
