sampler baseTexture : register(s0);

float DisplacementX;
float DisplacementY;

float4 FilterMyShader(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = (coords.x + DisplacementX <= 0 || coords.x + DisplacementX >= 1) ? float4(0, 0, 0, 0) : tex2D(baseTexture, float2(coords.x + DisplacementX, coords.y));
    return color;
}

technique Technique1
{
    pass FilterMyShader
    {
        PixelShader = compile ps_2_0 FilterMyShader();
    }
}