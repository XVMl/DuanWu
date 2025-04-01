sampler baseTexture : register(s0);

float globalTime;
float intensity;
float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    //float4 color = (coords.x + DisplacementX <= 0 || coords.x + DisplacementX >= 1) ? float4(0, 0, 0, 0) : tex2D(baseTexture, float2(coords.x + DisplacementX, coords.y));
    float2 center = float2(0.5, 0.5);
    float time = intensity == 0 ?globalTime : intensity;
    
    float angle = 3.141*time; 
    float sinRot, conRot;
    sincos(angle, sinRot, conRot);
    float2x2 rotationMatrix =
    {
        conRot, -sinRot,
        sinRot, conRot
    };
    float2 rotatedUV = coords - center;
    rotatedUV = mul(rotationMatrix, rotatedUV) + center;
    rotatedUV = saturate(rotatedUV);
     
    float4 color = tex2D(baseTexture, rotatedUV);
    return color;
}

technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}