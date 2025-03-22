using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using System.Collections;
using Terraria.Graphics.Effects;

namespace DuanWu.Content.UI
{
    public class Scoreboard : UIState
    {
        public static UIGrid UIGrid;
        public static Dictionary <string,bool> Player = [];
        public static Dictionary <string, int> counts = [];
        public override void OnInitialize()
        {
            UIGrid = new UIGrid();
            UIGrid.Width.Set(100, 0);
            UIGrid.Height.Set(200, 0);
            UIGrid.HAlign = 1f;
            UIGrid.VAlign = 0.5f;
            Append(UIGrid);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/text"), UIGrid.GetDimensions().ToRectangle(), Color.White);
        }

    }
    internal class ScoreboardElement : UIElement
    {
        private UIText name;
        private UIText count;
        private UIText accuracy;
        public ScoreboardElement(string name,float corrects,int numberofquestions)
        {
            this.name = new UIText(name);
            accuracy = new UIText(corrects.ToString());
            count = new UIText(numberofquestions.ToString());
            Height.Set(50, 0);
            Width.Set(100, 0);
            count.Top.Set(25, 0);
            accuracy.Top.Set(40, 0);
            Append(this.name);
            Append(accuracy);
            Append(count);
        }

    }

}
