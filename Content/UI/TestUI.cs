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
    //internal class GameState : BaseUIState
    //{
    //    private static MyGrid iGrid = new();
    //    public override void OnInitialize()
    //    {
    //        iGrid = new MyGrid();
    //        iGrid.Width.Set(300f, 0);
    //        iGrid.Height.Set(300f, 0);
    //        iGrid.HAlign = iGrid.VAlign = 0.5f;
    //        Append(iGrid);
    //    }
    //    public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

    //    public override void Update(GameTime gameTime)
    //    {
    //        base.Update(gameTime);
    //    }

    //    protected override void DrawSelf(SpriteBatch spriteBatch)
    //    {
    //        //Utils.DrawBorderString(spriteBatch, message, Utils.TopLeft(iGrid.GetDimensions().ToRectangle()) + Vector2.One * 0.4f, Color.White);
    //        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/text"), iGrid.GetDimensions().ToRectangle(), Color.White);
    //    }

    //}

    public class TestUI : BaseUIState
    {
        private static MyGrid iGrid = new();
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        public override bool IsLoaded() =>true;
        public override void OnInitialize()
        {
            iGrid = new MyGrid();
            iGrid.Width.Set(200f, 0);
            iGrid.Height.Set(300f, 0);
            iGrid.HAlign = 0.5f;
            iGrid.VAlign = 0.5f;
            Append(iGrid);
        }

        public static void AddElement()
        {
            float x = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy;
            Main.NewText(x);
            iGrid.Add(new ScoreboardElement(x.ToString(), x, Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount));
        }

        public static void Adjust()
        {
            iGrid._items[1].Top.Set(200f, 0);
        }

        //protected override void DrawSelf(SpriteBatch spriteBatch)
        //{
        //    //Utils.DrawBorderString(spriteBatch, message, Utils.TopLeft(iGrid.GetDimensions().ToRectangle()) + Vector2.One * 0.4f, Color.White);
        //    spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/text"), iGrid.GetDimensions().ToRectangle(), Color.White);
        //}

    }

}
