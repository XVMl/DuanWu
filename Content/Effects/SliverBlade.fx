sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);

float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
    
float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
    {
        return color;
    }
    float2 newcoords = float2(coords.x - uTime * 0.5f, coords.y + 0.5f * uTime);
    float4 noise = tex2D(uImage1, newcoords);
    noise.r -= (1 - frac(coords.y))-0.3;
    float4 color1 = tex2D(uImage2, newcoords * float2(0.5,0.5));
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