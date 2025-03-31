sampler baseTexture : register(s0);

float2 screenscalerevise;
float2 targetposition;
float3 uColor;
float uProgress;
float uOpacity;

float4 Shockwave(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float2 targetCoords = (targetposition) / screenscalerevise;
    float2 centreCoords = (coords - targetCoords) * (screenscalerevise / screenscalerevise.y);
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
    pass test
    {
        PixelShader = compile ps_2_0 Shockwave();
    }
}