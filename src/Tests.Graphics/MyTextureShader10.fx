float4x4 WorldViewProj : WorldViewProjection;

Texture2D<float4> MyTexture
<
	string ResourceName = "default_color.dds";
>;

SamplerState TextureSampler
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Clamp;
    AddressV = Wrap;
};

struct VS_IN
{
	float3 pos : POSITION;
	float3 col : COLOR;
	float2 tex : TEXCOORD;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 col : COLOR;
	float2 tex : TEXCOORD;
};


PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(float4(input.pos.xyz, 1.0), WorldViewProj);
	output.col = input.col;
	output.tex = input.tex;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	//return float4(input.col.rgb, 1.0);
	//return float4(1, 0, 0, 0);
	
	float4 texel = MyTexture.Sample(TextureSampler, input.tex);
	
	return texel;
}

technique10 Render
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}
