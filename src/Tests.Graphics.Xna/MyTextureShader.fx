/*

% Description of my shader.
% Second line of description for my shader.

keywords: material classic

date: YYMMDD

*/

float4x4 WorldViewProj : WorldViewProjection;

texture Texture : Texture
<
	string ResourceName = "default_color.dds";
>;

sampler TextureSampler = sampler_state 
{
    texture = <Texture>;
};

struct VS_PARAMETER
{
	float4 Position : POSITION;
	float2 Texcoord : TEXCOORD;
};

VS_PARAMETER mainVS(float3 pos : POSITION, float2 texCoord : TEXCOORD)
{
	VS_PARAMETER output;
	output.Position = mul(float4(pos.xyz, 1.0), WorldViewProj);
	output.Texcoord = texCoord;
	
	return output;
}

float4 mainPS(float2 texCoord : TEXCOORD) : COLOR
{
	return tex2D(TextureSampler, texCoord);
}

Technique DefaultTechnique
{
	Pass FirstPass
	{		
		VertexShader = compile vs_2_0 mainVS();
		PixelShader = compile ps_2_0 mainPS();
	}	
}
