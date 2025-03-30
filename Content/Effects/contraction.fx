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

float2 _Center = float2(0.5, 0.5); // �������ĵ������������
float _Radius = 0.3; // Ť���İ뾶
float _Turns = 5.0; // һȦ�뾶��Ť����Ȧ��
float _Angle = 18.3;
float4 PixelShaderFunction( float4 pos :SV_Position,float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    //float2 pos = uTargetPosition;
    // offset �����ĵ���ǰ�������
    float2 offset = (coords - _Center);
    // ��Ϊ����Ȳ�ͬ��������
    float2 rpos = offset * float2(uScreenResolution.x / uScreenResolution.y, 1);
    float dis = length(rpos);  
    //if (dis >= 0.3f) 
    //{
    //    return color;
    //}
       
    //��̬��ת����
    float rotation = _Angle * saturate(1 - dis / _Radius);
    float sinrot, cosrot;
    sincos(rotation, sinrot, cosrot);
    
    //�任����
    float2x2 rotMatrix = float2x2(cosrot, -sinrot, sinrot, cosrot);
    
    // �����µ���������
    float2 newTexCoord = mul(rotMatrix, offset)+0.5;
    
    return tex2D(uImage0, newTexCoord);
}
technique Technique1
{
    pass test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}