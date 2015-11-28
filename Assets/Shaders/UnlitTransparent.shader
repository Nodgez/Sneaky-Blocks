Shader "Custom/UnlitTransparent" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
	pass
	{
		Tags { "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		struct Input {
			float2 uv_MainTex;
		};
		
		struct appdata
		{
			float4 vertex : POSITION;
		};
		
		struct v2f
		{
			float4 pos : SV_POSITION;
		};

		fixed4 _Color;

		v2f vert(appdata input)
		{
			v2f output;
			output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			return output;
		}
		
		float4 frag(v2f input) : COLOR
		{
			return _Color;
		}
		ENDCG
		} 
	}
}
