using DuanWu.Content.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;

namespace DuanWu.Content.UI
{
    internal class WaitingUI : BaseUIState
    {
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        private UIText Wait;

        private UIImage Emoji;

        public override void OnInitialize()
        {
            Wait = new UIText(Language.GetTextValue("Mods.DuanWu.Other.Waiting"), 1, true);
            Wait.VAlign = 0.75f;
            Wait.HAlign = 0.35f;
            Emoji = new(BaseTexture("Emoji0"));
            Emoji.VAlign = 0.85f;
            Emoji.HAlign = 0.1f;
            Append(Wait);
            Append(Emoji);
        }

        public override void Update(GameTime gameTime)
        {
            if (!DuanWuPlayer.WaitingForQuestionEnd)
            {
                Wait.Remove();
                return;
            }
      
            Emoji.SetImage(BaseTexture("Emoji"+Main.rand.Next(0,6).ToString()));
            Wait.SetText(Language.GetTextValue("Mods.DuanWu.Other.Waiting"));
            
            Append(Wait);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

        }
    }
}
