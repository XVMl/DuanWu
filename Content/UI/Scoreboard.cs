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
using DuanWu.Content.System;
using Terraria.GameInput;
using Terraria.ID;

namespace DuanWu.Content.UI
{
    public class Scoreboard : BaseUIState
    {
        public static MyGrid UIGrid;
        public static Dictionary<string, bool> Player = [];
        public static Dictionary<string, int> counts = [];

        public override bool IsLoaded() => Main.netMode == NetmodeID.MultiplayerClient;
        
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        public override void OnInitialize()
        {
            UIGrid = new MyGrid();
            UIGrid.Width.Set(200, 0);
            UIGrid.HAlign = 1f;
            UIGrid.VAlign = 0.5f;
            CalcBox();
            Append(UIGrid);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>("DuanWu/Content/UI/CutsceneUI/Scoreboard"), UIGrid.GetDimensions().ToRectangle(), Color.White);
        }

        public static void CaleElement(int count,Dictionary<string, Record> _records)
        {
            if (UIGrid.Count == count) return;
            UIGrid.Clear();
            foreach(var item in _records)
            {
                UIGrid.Add(new ScoreboardElement(item.Key, 0, 0));
            }
        }

        public static void TryUpdata(string playname, float corrects, int numberofquestions, int index)
        {
            foreach (ScoreboardElement item in UIGrid._items)
            {
                if (item.name.Text==playname)
                {
                    item.TryUpdata(playname, corrects, numberofquestions);
                    break;
                }
            }
            //(UIGrid._items[index] as ScoreboardElement).TryUpdata(playname, corrects, numberofquestions);
        }

        public static void CalcBox()
        {
            UIGrid.Height.Set(5 + UIGrid._items.Count * 45, 0);
            UIGrid.Width.Set(200, 0);
        }

    }

    internal class ScoreboardElement : UIElement
    {
        public UIText name;
        private UIText accuracy;
        private UIText count;
        private UIImage imac;
        private UIImage imcn;
        public float AC = 0;
        public ScoreboardElement(string playname, float corrects, int numberofquestions)
        {
            name = new UIText(playname);
            accuracy = new UIText("AC"+corrects.ToString());
            count = new UIText("CN" + numberofquestions.ToString());
            imac = new(ModContent.Request<Texture2D>("DuanWu/Content/UI/Zongzi1"));
            imcn = new(ModContent.Request<Texture2D>("DuanWu/Content/UI/Zongye1"));
            AC = corrects;
            name.Left.Set(50, 0);
            Height.Set(40, 0);
            Width.Set(100, 0);
            count.Top.Set(20, 0);
            imcn.Top.Set(20, 0);
            count.Left.Set(100, 0);
            imcn.Left.Set(100, 0);
            accuracy.Top.Set(20, 0);
            imac.Top.Set(20, 0);
            accuracy.Left.Set(20, 0);
            imac.Left.Set(20, 0);
            Append(name);
            Append(accuracy);
            Append(count);
            Append(imac);
            Append(imcn);   
        }

        public void TryUpdata(string playname, float corrects, int numberofquestions)
        {
            name.SetText(playname);
            accuracy.SetText("AC"+ corrects.ToString());
            count.SetText("CN"+numberofquestions.ToString());
            AC= corrects;
        }

    }

    public class MyGrid : UIElement
    {
        public delegate bool ElementSearchMethod(UIElement element);

        private class UIInnerList : UIElement
        {
            public override bool ContainsPoint(Vector2 point)
            {
                return true;
            }

            protected override void DrawChildren(SpriteBatch spriteBatch)
            {
                Vector2 position = this.Parent.GetDimensions().Position();
                Vector2 dimensions = new Vector2(this.Parent.GetDimensions().Width, this.Parent.GetDimensions().Height);
                foreach (UIElement current in this.Elements)
                {
                    Vector2 position2 = current.GetDimensions().Position();
                    Vector2 dimensions2 = new Vector2(current.GetDimensions().Width, current.GetDimensions().Height);
                    if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
                    {
                        current.Draw(spriteBatch);
                    }
                }
            }
        }

        public List<UIElement> _items = new List<UIElement>();
        protected UIScrollbar _scrollbar;
        internal UIElement _innerList = new UIInnerList();
        private float _innerListHeight;
        public float ListPadding = 5f;

        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        // todo, vertical/horizontal orientation, left to right, etc?
        public MyGrid()
        {
            this._innerList.OverflowHidden = false;
            this._innerList.Width.Set(0f, 1f);
            this._innerList.Height.Set(0f, 1f);
            this.OverflowHidden = true;
            base.Append(this._innerList);
        }

        public float GetTotalHeight()
        {
            return this._innerListHeight;
        }

        public virtual void Add(UIElement item)
        {
            _items.Add(item);
            _innerList.Append(item);
            UpdateOrder();
            _innerList.Recalculate();
        }

        public void UpdateOrder()
        {
            _items.Sort(new Comparison<UIElement>(SortMethod));
        }

        public int SortMethod(UIElement item1, UIElement item2)
        {
            return ((item2 as ScoreboardElement).AC).CompareTo((item1 as ScoreboardElement).AC);
        }
        public override void RecalculateChildren()
        {
            float availableWidth = GetInnerDimensions().Width;
            UpdateOrder();
            base.RecalculateChildren();
            float top = 0f;
            float left = 0f;
            float maxRowHeight = 0f;
            for (int i = 0; i < this._items.Count; i++)
            {
                var item = this._items[i];
                var outerDimensions = item.GetOuterDimensions();
                if (left + outerDimensions.Width > availableWidth && left > 0)
                {
                    top += maxRowHeight + this.ListPadding;
                    left = 0;
                    maxRowHeight = 0;
                }
                maxRowHeight = Math.Max(maxRowHeight, outerDimensions.Height);
                item.Left.Set(left, 0f);
                left += outerDimensions.Width + this.ListPadding;
                item.Top.Set((float)Utils.Lerp(item.Top.Pixels, top, 0.3f), 0f);
            }
            this._innerListHeight = top + maxRowHeight;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsMouseHovering)
                PlayerInput.LockVanillaMouseScroll("ModLoader/UIList");
        }

        public virtual void Clear()
        {
            this._innerList.RemoveAllChildren();
            this._items.Clear();
        }

    }

}
