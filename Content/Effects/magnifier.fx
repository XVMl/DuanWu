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
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    float2 pos = uTargetPosition;
    //float2 pos = float2(0.5, 0.5);
    // offset �����ĵ���ǰ�������
    float2 offset = (coords - pos);
    // ��Ϊ����Ȳ�ͬ��������
    float2 rpos = offset * float2(uScreenResolution.x / uScreenResolution.y, 1);
    float dis = length(rpos);
    if (dis >= 0.3f)
    {
        return color;
    }
    // ������������0.8��
    return tex2D(uImage0, pos + dis * offset);
}
technique Technique1
{
    pass test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}