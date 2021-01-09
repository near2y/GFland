
Shader "Custom/Test" {
	Properties{
	_Color("Color", Color) = (1,1,1,1)
	_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
	_Metallic("Metallic", Range(0,1)) = 0.0
	_OcclusionColor("OcclusionColor",COLOR) = (0.5,1,0.5,1)
	}
		SubShader
	{
	Tags{"RenderType" = "Opaque" "Queue" = "Transparent"}
	LOD 200


	pass
	{
	ZTest Greater
	ZWrite off
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	fixed4 _OcclusionColor;
	float4 vert(float4 v:POSITION) :SV_POSITION
	{
	return UnityObjectToClipPos(v);
	}
	fixed4 frag() : SV_Target
	{
	return _OcclusionColor;
	}
	ENDCG
	}

	ZTest LEqual
	CGPROGRAM
	#pragma surface surf Standard fullforwardshadows

	#pragma target 3.0


	sampler2D _MainTex;


	struct Input {
	float2 uv_MainTex;
	};


	half _Glossiness;
	half _Metallic;
	fixed4 _Color;


	void surf(Input IN, inout SurfaceOutputStandard o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = c.rgb;
	o.Metallic = _Metallic;
	o.Smoothness = _Glossiness;
	o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"

}