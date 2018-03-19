Shader "Custom/OceanWall" {
	Properties {
		_ColourOne ("Colour One", Color) = (1,1,1,1)
		_ColourTwo ("Colour Two", Color) = (1,1,1,1)

		_OceanSize ("Ocean Size", float) = 1.0
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert
        #pragma target 3.0
 
		sampler2D _MainTex;
		sampler2D _CameraDepthTexture;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
			float3 localPos;
		};

		half _Glossiness;
		half _DepthFactor;
		half _FadeAmount;
		half _FoamLineDepthFactor;

		fixed4 _ColourOne;
		fixed4 _ColourTwo;	

		half _OceanSize;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
 
		 void vert (inout appdata_full v, out Input o) {
		   UNITY_INITIALIZE_OUTPUT(Input,o);
		   o.localPos = v.vertex.xyz;
		 }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			fixed4 col;

			//White Edge at Contact
			if(IN.localPos.x > _OceanSize || IN.localPos.x < -_OceanSize )
			{
				col = _ColourOne;
			}

			if(IN.localPos.z > _OceanSize || IN.localPos.z < -_OceanSize)
			{
				col = _ColourTwo;
			}

			// Final Shader Setup
			o.Albedo = col.rgb;
			o.Alpha = col.a;
		}
		ENDCG
	}
}
