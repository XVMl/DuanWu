sampler baseTexture : register(s0);

float2 screenscalerevise;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    // 设置像素块大小
    const float PIXEL_SIZE = 10.0;
    
    // 坐标量化 
    float2 quantizedCoords = floor(coords * screenscalerevise / PIXEL_SIZE);
    quantizedCoords = (quantizedCoords * PIXEL_SIZE) / screenscalerevise;
    
    // 采样量化后的坐标
    return tex2D(baseTexture, quantizedCoords);
}
technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
