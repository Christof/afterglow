float4x4 WorldViewProj : WorldViewProjection;
float3 LightPosition = (15, 15, 15);

struct VS_IN
{
	float3 position : POSITION;
	float3 normal : NORMAL;
	float3 color : COLOR0;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float3 col : COLOR;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(float4(input.position.xyz, 1.0), WorldViewProj);
	
	float3 lightDir = normalize(LightPosition - input.position);
	output.col.rgb = input.color * (dot(input.normal, lightDir) * 0.5 + 0.5);
	
	return output;
}

PS_IN VS_Normals( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(float4(input.position.xyz, 1.0), WorldViewProj);
		
	output.col = input.normal * 0.5 + 0.5;
	
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

technique10 RenderNormals
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS_Normals() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}