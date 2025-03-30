using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
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

            Filters.Scene["DuanWuShader:Shockwave"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/Shockwave", AssetRequestMode.ImmediateLoad).Value), "test"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:Shockwave"].Load();

            Filters.Scene["DuanWuShader:magnifiter"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/magnifier", AssetRequestMode.ImmediateLoad).Value), "test"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:magnifiter"].Load();

            Filters.Scene["DuanWuShader:contraction"] = new Filter(new ESScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/contraction", AssetRequestMode.ImmediateLoad).Value), "test"), (EffectPriority)2);
            Filters.Scene["DuanWuShader:contraction"].Load();

            Ref<Effect> MiscEffect1 = new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/ArmorBasic",AssetRequestMode.ImmediateLoad ).Value);
            GameShaders.Misc["ArmorBasic"] = new MiscShaderData(MiscEffect1, "ArmorBasic");

            Ref<Effect> MiscEffect2 = new Ref<Effect>(ModContent.Request<Effect>("DuanWu/Content/Effects/Content/SliverBlade", AssetRequestMode.ImmediateLoad).Value);
            GameShaders.Misc["SliverBlade"] = new MiscShaderData(MiscEffect2, "ArmorBasic");
        }
    }
}
