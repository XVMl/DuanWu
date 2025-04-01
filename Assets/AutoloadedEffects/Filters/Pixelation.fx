sampler baseTexture : register(s0);

float2 screenscalerevise;
float intensity;
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // �������ؿ��С
    const float PIXEL_SIZE = intensity;
    
    // �������� 
    float2 quantizedCoords = floor(coords * screenscalerevise / PIXEL_SIZE);
    quantizedCoords = (quantizedCoords * PIXEL_SIZE) / screenscalerevise;
    
    // ���������������
    return tex2D(baseTexture, quantizedCoords);
}
technique Technique1
{
    pass AutoloadPass
    { 
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
