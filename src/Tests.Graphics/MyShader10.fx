float4x4 WorldViewProj : WorldViewProjection;

struct VS_IN
{
	float3 pos : POSITION;
	float3 col : COLOR;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 col : COLOR;
};


PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(float4(input.pos.xyz, 1.0), WorldViewProj);
	output.col = input.col;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return float4(input.col.rgb, 1.0);
	//return float4(1, 0, 0, 0);
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
