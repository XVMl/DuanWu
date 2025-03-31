sampler baseTexture : register(s0);

float2 screenscalerevise;
float2 targetposition;
float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(baseTexture, coords);
    if (!any(color))
        return color;
    float2 pos = float2(0.5, 0.5);
    // offset �����ĵ���ǰ�������
    float2 offset = (coords - pos);
    // ��Ϊ����Ȳ�ͬ��������
    float2 rpos = offset * float2(screenscalerevise.x / screenscalerevise.y, 1);
    float dis = length(rpos);
    if (dis >= 0.3f)
    {
        return color;
    }
    // ������������0.8��
    return tex2D(baseTexture, pos + dis * offset);
} 
technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}