using Luminance.Common.Utilities;
using Luminance.Core.Cutscenes;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat.Commands;
using Terraria.ModLoader;
using Terraria.UI;
namespace DuanWu.Content.System
{
    //public class PlayVideo : UIState
    //{
    //    private UIElement area;

    //    private UIVideo video;

    //    private float WihteAlpha = 0;

    //    public override void OnInitialize()
    //    {
    //        area = new UIElement();
    //        area.Top.Set(0f, 0f);
    //        area.Left.Set(0f, 0f);
    //        area.Width.Set(Main.screenWidth, 1f);
    //        area.Height.Set(Main.screenHeight, 1f);
    //        area.HAlign = area.VAlign = .5f;
    //        video = new UIVideo(ModContent.Request<Video>("DuanWu/Assets/Videos/NeverGonnaGiveYouUp"))
    //        {
    //            ScaleToFit = true,
    //            //WaitForStart = true,
    //            //DoLoop = true,
    //        };
    //        video.Width.Set(0, 2f);
    //        video.Height.Set(0, 2f);
    //        Append(area);
    //    }

    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        base.Draw(spriteBatch);
    //    }

    //    protected override void DrawSelf(SpriteBatch spriteBatch)
    //    {
    //        //Texture2D tex = ModContent.Request<Texture2D>("ParadiseLost/Content/UI/WhiteScreen").Value;
    //        //if (Main.LocalPlayer.GetModPlayer<ParadiseLostPlayer>().End)
    //        //{
    //        //    float op = Main.LocalPlayer.GetModPlayer<ParadiseLostPlayer>().Cutscene;
    //        //    spriteBatch.Draw(tex, area.GetInnerDimensions().ToRectangle(), Color.White * (op / 45));
    //        //}
    //        //spriteBatch.Draw(tex, area.GetInnerDimensions().ToRectangle(), Color.White * WihteAlpha);
    //        //base.DrawSelf(spriteBatch);
    //    }
    //    public override void Update(GameTime gameTime)
    //    { 
    //        DuanWuPlayer Set = Main.LocalPlayer.GetModPlayer<DuanWuPlayer>();
    //        if (Set.Cutscene >= 0)
    //        {
    //            Set.Cutscene--;
    //        }
    //        else
    //        {
    //            Set.Cutscene = 0;
    //        }
    //        PlaySelectVideo(Set);
    //        base.Update(gameTime);
    //    }

    //    private void PlaySelectVideo(DuanWuPlayer set)
    //    {
    //        UIVideo iVideo = video;
    //        //if (set.Cutscene > 0 && set.Cutscene <= 60)
    //        //{
    //        //    WihteAlpha += 0.01f;
    //        //}
    //        if (set.Cutscene == 1 && !iVideo.StartVideo)
    //        {
    //            iVideo.FinishVideos = false;
    //            iVideo.StartVideo = true;
    //            area.Append(iVideo);
    //        }
    //        if (iVideo.FinishVideos)
    //        {
    //            iVideo.FinishVideos = false;
    //            iVideo.Remove();
    //        }
    //        if (set.Cutscene == 0)
    //        {
    //            WihteAlpha = 0f;
    //        }
    //    }
    //}

    public class VideoCutscene : Cutscene
    {
        public override int CutsceneLength => Utilities.SecondsToFrames(5);

        public override BlockerSystem.BlockCondition GetBlockCondition => new(true, false, () => Timer<CutsceneLength);
        public override void OnBegin()
        {
            Main.NewText("begin");
        }

        public override void OnEnd()
        {
            Main.NewText("end");
        }
    }

}
