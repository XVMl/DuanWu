sampler baseTexture : register(s0);

float2 screenscalerevise;
float2 _Center = float2(0.5, 0.5); // �������ĵ������������
float _Radius = 0.3; // Ť���İ뾶
float _Turns = 5.0; // һȦ�뾶��Ť����Ȧ��
float _Angle = 18.3;
float4 PixelShaderFunction( float4 pos :SV_Position,float2 coords : TEXCOORD0) : COLOR0
{
    //float4 color = tex2D(baseTexture, coords);
    //if (!any(color))
    //    return color;
    //float2 pos = uTargetPosition;
    // offset �����ĵ���ǰ�������
    float2 offset = (coords - _Center);
    // ��Ϊ����Ȳ�ͬ��������
    float2 rpos = offset * float2(screenscalerevise.x / screenscalerevise.y, 1);
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
    
    return tex2D(baseTexture, newTexCoord);
}
technique Technique1
{
    pass test
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}