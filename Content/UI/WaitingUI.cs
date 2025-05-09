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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;

namespace DuanWu.Content.UI
{
    internal class WaitingUI : BaseUIState
    {
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        //public override bool IsLoaded() => Main.netMode == NetmodeID.MultiplayerClient;


        private UIText Wait;

        public static UIImage Emoji;

        public override void OnInitialize()
        {
            Wait = new(Language.GetTextValue("Mods.DuanWu.Other.Waiting"), 1, true)
            {
                VAlign = 0.8f,
                HAlign = 0.35f
            };
            Emoji = new(BaseTexture("Emoji0"))
            {
                VAlign = 0.85f,
                HAlign = 0.1f
            };
            Append(Emoji);
        }

        public override void Update(GameTime gameTime)
        {
            if (!DuanWuPlayer.WaitingForQuestionEnd)
            {
                Wait.Remove();
                Emoji.Remove();
                return;
            }
            Wait.SetText(Language.GetTextValue("Mods.DuanWu.Other.Waiting"));
            Append(Emoji);
            Append(Wait);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

        }
    }
}
