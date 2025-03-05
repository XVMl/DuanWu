using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.Graphics.Effects;
namespace DuanWu.Content.System
{
    public class DuanWuShader:ModSystem
    {
        public override void Load()
        {
            Filters.Scene["DuanWuShader:Zhuan"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/Zhuan", AssetRequestMode.ImmediateLoad).Value), "FilterMyShader"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:Zhuan"].Load();

            Filters.Scene["DuanWuShader:Blur"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/GaussianBlur", AssetRequestMode.ImmediateLoad).Value), "Test"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:Blur"].Load();

            Filters.Scene["DuanWuShader:Pixelation"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/Pixelation", AssetRequestMode.ImmediateLoad).Value), "Test"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:Pixelation"].Load();
        }
    }
}
