sampler baseTexture : register(s0);

float2 targetposition;
float3 uColor;
float uProgress; 
float uOpacity;
float2 screenscalerevise;
float4 PixelShaderFunction(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float2 targetCoords = targetposition;
    float2 centreCoords = (coords - targetCoords) * float2(screenscalerevise.x / screenscalerevise.y, 1);
    float dotField = dot(centreCoords, centreCoords);
    
    float ripple = dotField * uColor.y * 3.1415f - uProgress * uColor.z;

    if (ripple < 0 && ripple > uColor.x * -2 * 3.1415f)
    {
        ripple = saturate(sin(ripple));
    }
    else
    {
        ripple = 0;
    }

    float2 sampleCoords = coords + ((ripple * uOpacity / screenscalerevise) * centreCoords);

    return tex2D(baseTexture, sampleCoords);
}
technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}