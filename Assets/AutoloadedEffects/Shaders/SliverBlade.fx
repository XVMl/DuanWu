sampler baseTexture : register(s0);
sampler noiseTexture : register(s0);
sampler imageTexture : register(s0);
float2 pixelationFactor;
float globalTime;
float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(baseTexture, coords);
    if (!any(color))
    {
        return color;
    }
    float2 newcoords = float2(coords.x - globalTime * 0.5f, coords.y + 0.5f * globalTime);
    float4 noise = tex2D(noiseTexture, newcoords);
    noise.r -= (1 - frac(coords.y))-0.3;
    float4 color1 = tex2D(imageTexture, newcoords * float2(0.5,0.5));
    color1.a *=noise.r+0.4;
    clip(noise.r);
    return color1;
}
    
technique Technique1
{
    pass ArmorBasic
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}