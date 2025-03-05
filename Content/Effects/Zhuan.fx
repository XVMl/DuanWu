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


float DisplacementX;
float DisplacementY;

float4 FilterMyShader(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = (coords.x + DisplacementX <= 0 || coords.x + DisplacementX >= 1) ? float4(0, 0, 0, 0) : tex2D(uImage0, float2(coords.x + DisplacementX, coords.y));
    //if (coords.x + DisplacementX <= 0 || coords.x + DisplacementX>=1 )
    //{
        
    //}
        return color;
}

technique Technique1
{
    pass FilterMyShader
    {
        PixelShader = compile ps_2_0 FilterMyShader();
    }
}