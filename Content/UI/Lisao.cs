using DuanWu.Content.System;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat.Commands;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace DuanWu.Content.UI
{
    public class Lisao : BaseUIState
    {
        public static UIGrid LisaochoiceLisst;

        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        public override void OnInitialize()
        {
            LisaochoiceLisst = new UIGrid();
            LisaochoiceLisst.Width.Set(1250f, 0);
            LisaochoiceLisst.Height.Set(500f, 0);
            LisaochoiceLisst.VAlign = 0.7f;
            LisaochoiceLisst.HAlign = 0.5f;
            LisaochoiceLisst.ListPadding = 80f;
        }

        private void TryAddEdutor()
        {
            int numberofchoise = DuanWuPlayer.Hardmode ? 8 : 4;
            if (LisaochoiceLisst._items.Count!=numberofchoise)
            {
                LisaochoiceLisst.Clear();
                for (int i = 0; i < numberofchoise; i++)
                {
                    LisaochoiceLisst.Add(new MyButton(i,Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoChoiceText[i], i, ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButton")));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoActive)
            {
                LisaochoiceLisst.Remove();
                return;
            } 
            TryAddEdutor();
            foreach (var item in LisaochoiceLisst._items)
            {
                item.Update(gameTime);
            }
            Append(LisaochoiceLisst);
        }

    }

    internal class MyButton:UIImageButton
    {
        private int Number;

        public UIText uiText;

        private int Choise;

        private Asset<Texture2D> _texture;
        public MyButton(int choise,string text,int num,Asset<Texture2D> texture) : base(texture)
        {
            Number = num;
            Height.Set(32, 0);
            Width.Set(550, 0);
            Choise = choise;
            uiText = new UIText(text);
            _texture=texture;
            uiText.HAlign =uiText.VAlign = 0.5f;
            Append(uiText);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            var player = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            Texture2D active = ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButtonActive").Value;
            Texture2D t = ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButtonTrue").Value;
            Texture2D f = ModContent.Request<Texture2D>("DuanWu/Content/UI/choiceButtonFalse").Value;

            if (IsMouseHovering&& player.ShowAnswer<0)
                spriteBatch.Draw(active, dimensions.Position(), Color.White *  1f);
            else
                spriteBatch.Draw(_texture.Value, dimensions.Position(), Color.White * 0.7f);
            if (Choise == player.ChoiceAnswer && player.ShowAnswer > 0)
                spriteBatch.Draw(f, dimensions.Position(), Color.White  * Utilities.InverseLerp(0f, 180,270 - player.ShowAnswer));
            if (Choise == player.Answer && player.ShowAnswer > 0)
                spriteBatch.Draw(t, dimensions.Position(), Color.White * Utilities.InverseLerp(0f, 180, 270 - player.ShowAnswer));
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            DuanWuPlayer duanWuPlayer = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
            if (duanWuPlayer.counttime<=0)
            {
                return;
            }
            Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().ChoiceAnswer = this.Number;
            duanWuPlayer.counttime = 0;
            base.LeftClick(evt);
        }

        public override void Update(GameTime gameTime)
        {
            uiText.SetText(Main.LocalPlayer.GetModPlayer<DuanWuPlayer>().LisaoChoiceText[this.Number]);
        }

    }

}
