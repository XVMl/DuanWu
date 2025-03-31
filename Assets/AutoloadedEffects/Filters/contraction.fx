sampler baseTexture : register(s0);

float2 screenscalerevise;
float2 _Center = float2(0.5, 0.5); // 假设中心点是纹理的中心
float _Radius = 0.3; // 扭曲的半径
float _Turns = 5.0; // 一圈半径内扭曲的圈数
float _Angle = 18.3;
float4 PixelShaderFunction( float4 pos :SV_Position,float2 coords : TEXCOORD0) : COLOR0
{
    //float4 color = tex2D(baseTexture, coords);
    //if (!any(color))
    //    return color;
    //float2 pos = uTargetPosition;
    // offset 是中心到当前点的向量
    float2 offset = (coords - _Center);
    // 因为长宽比不同进行修正
    float2 rpos = offset * float2(screenscalerevise.x / screenscalerevise.y, 1);
    float dis = length(rpos);  
    //if (dis >= 0.3f) 
    //{
    //    return color;
    //}
       
    //动态旋转计算
    float rotation = _Angle * saturate(1 - dis / _Radius);
    float sinrot, cosrot;
    sincos(rotation, sinrot, cosrot);
    
    //变换矩阵
    float2x2 rotMatrix = float2x2(cosrot, -sinrot, sinrot, cosrot);
    
    // 计算新的纹理坐标
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