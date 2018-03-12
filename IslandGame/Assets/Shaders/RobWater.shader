Shader "Custom/RobWater" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FoamLine ("Color", Color) = (1,1,1,1)
		_DepthFactor ("Depth Factor", float) = 1.0

		_OceanSize ("Ocean Size", float) = 1.0
		_OceanSizeWave ("Ocean Size Wave", float) = 1.0
		_OceanSpeedWave ("Ocean Speed Wave", float) = 1.0
		_OceanPhaseWave ("Ocean Phase Wave", float) = 1.0
		_OceanEdgeColour ("Color", Color) = (1,1,1,1)
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
		fixed4 _Color;
		fixed4 _FoamLine;

		half _OceanSize;
		fixed4 _OceanEdgeColour;

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
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			// FoamLine
			half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
			depth = LinearEyeDepth(depth).r;
 
            float foamLine = 1 - saturate(_DepthFactor * (depth - IN.screenPos.w));
			float4 col = c + (foamLine * _FoamLine);

			//White Edge at Contact
			if(IN.localPos.y > -0.5 && (IN.localPos.x > _OceanSize || IN.localPos.x < -_OceanSize || IN.localPos.z > _OceanSize || IN.localPos.z < -_OceanSize))
			{
				col = _OceanEdgeColour;
			}

			// Final Shader Setup
			o.Albedo = col.rgb;
			o.Alpha = col.a;
		}
		ENDCG
	}
}
