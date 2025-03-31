sampler baseTexture : register(s0);
sampler noiseTexture : register(s0);

float2 pixelationFactor;
float globalTime;
float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(baseTexture, coords);
    if (!any(color))
    {
        return color;
    }
    float4 noise = tex2D(noiseTexture, float2(coords.x, coords.y + 0.5f * globalTime));
    noise.rgb -= (1 - frac(coords.y))*0.7f;
    clip(noise.r);
    return noise + color;
}
    
technique Technique1
{
    pass ArmorBasic
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}