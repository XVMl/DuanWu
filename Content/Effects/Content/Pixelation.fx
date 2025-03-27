sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // 设置像素块大小
    const float PIXEL_SIZE = 10.0;
    
    // 坐标量化
    float2 quantizedCoords = floor(coords * uScreenResolution / PIXEL_SIZE);
    quantizedCoords = (quantizedCoords * PIXEL_SIZE) / uScreenResolution;
    
    // 采样量化后的坐标
    return tex2D(uImage0, quantizedCoords);
}

technique Technique1
{
    pass Test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
