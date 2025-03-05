using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;

namespace DuanWu.Content.System
{
    public class ESScreenShaderData : ScreenShaderData
    {
        public ESScreenShaderData(string passName)
            : base(passName)
        {
        }

        public ESScreenShaderData(Ref<Effect> shader, string passName)
            : base(shader, passName)
        {
        }

        public override void Apply()
        {
            base.Apply();
        }
    }
}
