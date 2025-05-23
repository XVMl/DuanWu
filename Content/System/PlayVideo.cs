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
    
    public class VideoCutscene : Cutscene
    {
        public override int CutsceneLength => Utilities.SecondsToFrames(5);

        public override BlockerSystem.BlockCondition GetBlockCondition => new(true, false, () => Timer<CutsceneLength);
    }

}
