/*

% Description of my shader.
% Second line of description for my shader.

keywords: material classic

date: YYMMDD

*/

float4x4 WorldViewProj : WorldViewProjection;

struct VS_PARAMETER
{
	float4 Position : POSITION;
	float3 Color    : COLOR;
};

VS_PARAMETER mainVS(float3 pos : POSITION, float3 color : COLOR)
{
	VS_PARAMETER output;
	output.Position = mul(float4(pos.xyz, 1.0), WorldViewProj);
	output.Color = color;
	
	return output;
}

float4 mainPS(float3 color : COLOR) : COLOR
{
	return float4(color.rgb, 1.0);
}

Technique DefaultTechnique
{
	Pass FirstPass
	{		
		VertexShader = compile vs_2_0 mainVS();
		PixelShader = compile ps_2_0 mainPS();
	}	
}
