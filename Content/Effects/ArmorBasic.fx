sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
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
    float4 noise = tex2D(uImage1, float2(coords.x, coords.y + 0.5f * uTime));
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