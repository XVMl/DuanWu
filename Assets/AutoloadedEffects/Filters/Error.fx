sampler baseTexture : register(s0);

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    return sampleColor;
} 
technique Technique1
{
    pass AutoloadPas
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}