// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
/*扫描显示*/
Shader "XM/ScanEffect"
{
	Properties
	{
	_MainTex("Main Tex", 2D) = "white"{}
	_lineColor("Line Color", Color) = (0,0,0,0)
	_lineWidth("Line width", Range(0, 1.0)) = 0.1
	_rangeX("Range X", Range(0,1.0)) = 1.0
	}

		SubShader
	{
	Tags {
	"Queue" = "Transparent"
	}

	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	Cull back

	Pass
	{
	CGPROGRAM

	#pragma vertex vert
	#pragma fragment frag

	#include "Lighting.cginc"

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _lineColor;
	float _lineWidth;
	float _rangeX;

	struct a2v
	{
	float4 vertex : POSITION;
	float4 texcoord : TEXCOORD0;
	};

	struct v2f
	{
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	};

	v2f vert(a2v v)
	{
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
	return o;
	}

	fixed4 frag(v2f i) : SV_TARGET
	{
	fixed4 col = tex2D(_MainTex, i.uv);

	if (i.uv.x > _rangeX)
	{
	 clip(-1);
	}
	else if (i.uv.x > _rangeX - _lineWidth)
	{
	 float offsetX = i.uv.x - _rangeX + _lineWidth;
	 fixed xAlpha = offsetX / _lineWidth;
	 col = col * (1 - xAlpha) + _lineColor * xAlpha;
	}


	return col;
	}

	ENDCG
	}
	}

		FallBack "Diffuse"
}
