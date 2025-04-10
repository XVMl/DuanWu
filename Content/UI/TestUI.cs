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
    public class TestUI : UIState
    {
        private static MyGrid iGrid = new();
        private static string message = "SNJDON O";
        public override void OnInitialize()
        {
            iGrid = new MyGrid();
            iGrid.Width.Set(300f, 0);
            iGrid.Height.Set(300f, 0);
            iGrid.HAlign = iGrid.VAlign = 0.5f;
            Append(iGrid);
        }

        public static void AddElement()
        {
            iGrid.Add(new TestElement(ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton"), Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().PlayerAccuracy));
            message = "NKA";
        }

        public static void Adjust()
        {
            iGrid._items[1].Top.Set(200f, 0);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Utils.DrawBorderString(spriteBatch, message, Utils.TopLeft(iGrid.GetDimensions().ToRectangle()) + Vector2.One * 0.4f, Color.White);
            //    spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/text"), iGrid.GetDimensions().ToRectangle(), Color.White);
        }

    }


    internal class TestElement : UIImageButton
    {
        private UIText text;
        private Asset<Texture2D> _texture;
        public int num;
        public TestElement(Asset<Texture2D> texture,int n) : base(texture)
        {
            text = new(n.ToString());
            _texture = texture;
            num = n;
            Append(text);
        }

        //protected override void DrawSelf(SpriteBatch spriteBatch)
        //{
        //    CalculatedStyle dimensions = GetDimensions();
        //    if (Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ShowAnswer > 0)
        //    {
        //        spriteBatch.Draw(_texture.Value, dimensions.Position(), Color.Green * 1);
        //    }
        //    spriteBatch.Draw(_texture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? 1 : 0.4f));

        //}
    }
}
