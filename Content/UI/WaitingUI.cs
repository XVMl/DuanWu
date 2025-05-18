using DuanWu.Content.System;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class WaitingUI : BaseUIState
    {
        public override string Layers_FindIndex => "Vanilla: Interface Logic 2";

        public override bool IsLoaded() => Main.netMode == NetmodeID.MultiplayerClient;

        private UIText Wait;

        public static Texture2D Emoji0 => BaseTexture("Emoji0").Value;
        public static Texture2D Emoji1 => BaseTexture("Emoji1").Value;
        public static Texture2D Emoji2 => BaseTexture("Emoji2").Value;
        public static Texture2D Emoji3 => BaseTexture("Emoji3").Value;
        public static Texture2D Emoji4 => BaseTexture("Emoji4").Value;
        public static Texture2D Emoji5 => BaseTexture("Emoji5").Value;

        public static int Number = 0;


        public override void OnInitialize()
        {
            Wait = new(Language.GetTextValue("Mods.DuanWu.Other.Waiting"), 1, true)
            {
                VAlign = 0.8f,
                HAlign = 0.35f
            };

        }

        public override void Update(GameTime gameTime)
        {
            if (!DuanWuPlayer.WaitingForQuestionEnd)
            {
                Wait.Remove();
                return;
            }
            Wait.SetText(Language.GetTextValue("Mods.DuanWu.Other.Waiting"));
            Append(Wait);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!DuanWuPlayer.WaitingForQuestionEnd)
            {
                return;
            }
            Texture2D Waiting = ModContent.Request<Texture2D>("DuanWu/Assets/UI/Waiting").Value;
            spriteBatch.Draw(Waiting, Wait.GetDimensions().ToRectangle().TopLeft() + new Vector2(-85, 10),null, Color.White,0,Waiting.Size()/2,0.7f,SpriteEffects.None,0);
        }
    }


}
