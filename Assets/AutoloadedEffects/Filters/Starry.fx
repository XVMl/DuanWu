sampler baseTexture : register(s0);

float2 screenscalerevise;
float Strength = 1;
float MaxAngle = 28.3;
float _Radius = 0.2;
float _Mass = 2;
float _Distortion = 1;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{ 
    //float4 color = tex2D(baseTexture, coords);
    float2 Starcentern = float2(0.5, 0.5);
    float2 offset = (coords - Starcentern) ;
    float2 adj = float2(screenscalerevise.x / screenscalerevise.y, 1);
    float2 rpos = offset * adj;
    float dis = length(rpos);
    if (dis < 0.15)    
        return float4(0, 0, 0, 0);
                
    // 归一化处理
    float normDistance = saturate(dis / _Radius);
                 
    // 引力透镜偏移公式（简化相对论模型）
    float deflection = _Mass * (1 - normDistance) / (dis + 0.001);
    deflection *= _Distortion * 0.1;
                
    // 极坐标转笛卡尔偏移
    float2 os= rpos * deflection;
    float2 newpos = coords - os;
    float4 color = tex2D(baseTexture, newpos);
    return color ;
}

technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}