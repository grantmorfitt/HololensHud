Shader "Custom/TerrainAltShader" {
	Properties {
		_ColorTop ("Top Color", Color) = (1,1,1,1)
        _ColorMid ("Mid Color", Color) = (1,1,1,1)
        _ColorBot ("Bot Color", Color) = (1,1,1,1)
		_Middle("Middle", Range(0.001, 0.999)) = 1
		_Min("Minimum Height", Float) = 0.0
		_Max("Max Height", Float) = 10.0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		#pragma target 3.0

		sampler2D _MainTex;

		 fixed4 _ColorTop;
         fixed4 _ColorMid;
         fixed4 _ColorBot;
		 float _Middle;
		 float _Min;
		 float _Max;

		struct Input
		{
			float2 uv_MainTex;
			float4 objectSpacePos;
		};

		fixed4 _Color;

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.objectSpacePos = v.vertex;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			
			float difference = (_Max - _Min);
			float heightGradient = (IN.objectSpacePos.y)/ difference;
			/*if(heightGradient > 1)
			{
				heightGradient = 1;
			}
			if(heightGradient < 0)
			{
				heightGradient = 0;
			}*/
			//float _Middle = heightGradient / 2;
			fixed4 c = lerp(_ColorBot, _ColorMid, saturate(heightGradient/_Middle))*step(heightGradient, _Middle);
			c += lerp(_ColorMid, _ColorTop, saturate((heightGradient - _Middle) / (1 - _Middle))) * step(_Middle, heightGradient);
			//fixed4 c = lerp(_ColorBot, _ColorTop, heightGradient);
			//If some texture is given
			//fixed4 c = lerp(_ColorBot, _ColorTop, /*tex2D(_MainTex, IN.uv_MainTex) */ heightGradient);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//FallBack "Diffuse"
}