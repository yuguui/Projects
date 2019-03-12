
Shader "Texture Splatting 3 Detail" {
	Properties
	{
		_MainTex("Splat Map", 2D) = "white" {}
	[NoScaleOffset] _Texture1("Texture 1", 2D) = "white" {}
	[NoScaleOffset] _Texture2("Texture 2", 2D) = "white" {}
	[NoScaleOffset] _Texture3("Texture 3", 2D) = "white" {}
	[NoScaleOffset] _Texture4("Texture 4", 2D) = "white" {}
	_DetailTex("Detail Texture", 2D) = "gray" {}
	}

		SubShader
	{
		Pass
	{
		CGPROGRAM

#pragma vertex MyVertexProgram
#pragma fragment MyFragmentProgram	

#include "UnityCG.cginc"

		sampler2D _MainTex, _DetailTex;
	float4 _MainTex_ST, _DetailTex_ST;

	sampler2D _Texture1, _Texture2, _Texture3, _Texture4;

	struct Interpolators {
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uvDetail : TEXCOORD1;
		float2 uvSplat : TEXCOORD2;
	};

	struct VertexData {
		float4 position : POSITION;
		float2 uv : TEXCOORD0;
	};

	Interpolators MyVertexProgram(VertexData v) {
		Interpolators i;
		i.position = UnityObjectToClipPos(v.position);
		i.uv = TRANSFORM_TEX(v.uv, _MainTex);
		i.uvDetail = TRANSFORM_TEX(v.uv, _DetailTex);
		i.uvSplat = v.uv;
		return i;
	}

	float4 MyFragmentProgram(Interpolators i) : SV_TARGET{
		float4 splat = tex2D(_MainTex, i.uvSplat);
		splat *= tex2D(_DetailTex, i.uvDetail) * unity_ColorSpaceDouble;
		return
			tex2D(_Texture1, i.uv) * splat.r +
			tex2D(_Texture2, i.uv) * splat.g +
			tex2D(_Texture3, i.uv) * splat.b +
			tex2D(_Texture4, i.uv) * (1 - splat.r - splat.g - splat.b);
	}

		ENDCG
	}
	}
}
