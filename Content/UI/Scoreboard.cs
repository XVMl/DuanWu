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
        private UIGrid UIGrid;
        private Dictionary <Player,bool> Player = [];
        public override void OnInitialize()
        {
            UIGrid = new UIGrid();
            UIGrid.Width.Set(100, 0);
            UIGrid.Height.Set(200, 0);
            UIGrid.HAlign = 1f;
            UIGrid.VAlign = 0.5f;
            Append(UIGrid);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Player play in Main.ActivePlayers)
            {
                Player.TryAdd(play, false);
                if (!Player[play])
                {
                    UIGrid.Add(new ScoreboardElement(play));
                    Player[play] = true;
                }

            }
            foreach(UIElement uI in  UIGrid._items)
            {
                uI.Update(gameTime);
            }
             
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
        private Player player;
        public ScoreboardElement(Player player)
        {
            name = new UIText(player.name);
            count = new UIText(player.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount.ToString());
            this.player = player;
            Height.Set(50, 0);
            Width.Set(100, 0);
            count.Top.Set(25, 0);
            Append(name);
            Append(count);
        }

        public override void Update(GameTime gameTime)
        {
            count.SetText(player.GetModPlayer<DuanWuPlayer>().PlayerQuestioncount.ToString());
        }
    }

}
