using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace DuanWu.Content.System
{
    public class DuanWUServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false)]
        public bool Quickresponse;

        [DefaultValue(false)]
        public bool scoreboard;

        public override void OnChanged()
        {
            DuanWuPlayer.Quickresponse = Quickresponse;
        }

        
    }
}
