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
        public static Dictionary<string, bool> Player = [];
        public static Dictionary<string, int> counts = [];
        public override void OnInitialize()
        {
            UIGrid = new UIGrid();
            UIGrid.Width.Set(200, 0);
            UIGrid.HAlign = 1f;
            UIGrid.VAlign = 0.5f;
            CalcBox();
            Append(UIGrid);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/text"), UIGrid.GetDimensions().ToRectangle(), Color.White);
        }

        public static void CaleElement(int count)
        {
            if (UIGrid.Count == count) return;
            UIGrid.Clear();
            for (int i = 0; i < count; i++) { UIGrid.Add(new ScoreboardElement("", 0, 0)); }
        }

        public static void TryUpdata(string playname, float corrects, int numberofquestions, int index)
        {
            (UIGrid._items[index] as ScoreboardElement).TryUpdata(playname, corrects, numberofquestions);
        }

        public static void CalcBox()
        {
            UIGrid.Height.Set(5 + UIGrid._items.Count * 45, 0);
            UIGrid.Width.Set(200, 0);
        }

    }
    internal class ScoreboardElement : UIElement
    {
        private UIText name;
        private UIText count;
        private UIText accuracy;
        private int Rank = 1;
        public ScoreboardElement(string playname, float corrects, int numberofquestions)
        {
            name = new UIText(playname);
            accuracy = new UIText(corrects.ToString());
            count = new UIText(numberofquestions.ToString());
            name.Left.Set(50, 0);
            Height.Set(40, 0);
            Width.Set(100, 0);
            count.Top.Set(20, 0);
            accuracy.Top.Set(20, 0);
            accuracy.Left.Set(20, 0);
            Append(name);
            Append(accuracy);
            Append(count);
        }

        public void TryUpdata(string playname, float corrects, int numberofquestions)
        {
            name.SetText(playname);
            accuracy.SetText(corrects.ToString());
            count.SetText(numberofquestions.ToString());
        }



    }

}
