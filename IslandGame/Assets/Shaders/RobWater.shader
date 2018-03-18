Shader "Custom/RobWater" {
	Properties {
		_MaxColor ("Max Color", Color) = (1,1,1,1)
		_MinDepth ("Min Depth", float) = 1.0
		
		_MinColor ("Min Color", Color) = (1,1,1,1)
		_MaxDepth ("Max Depth", float) = 10.0

		_DepthFactor ("Depth Factor", float) = 1.0
		_FadeAmount ("Fade Amount", float) = 1.0

		//FoamLine
		_FoamLineDepthFactor ("Foam Depth Factor", float) = 1.0
		_FoamColor ("Foam Color", Color) = (1,1,1,1)

		_OceanSize ("Ocean Size", float) = 1.0
		_OceanSizeWave ("Ocean Size Wave", float) = 1.0
		_OceanSpeedWave ("Ocean Speed Wave", float) = 1.0
		_OceanPhaseWave ("Ocean Phase Wave", float) = 1.0
		_OceanEdgeColour ("Color", Color) = (1,1,1,1)

		
		_MaxDepth ("Max Depth", float) = 10.0
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

		fixed4 _MaxColor;
		fixed4 _MinColor;
		fixed4 _FoamColor;

		half _OceanSize;
		fixed4 _OceanEdgeColour;

		half _MinDepth;
		half _MaxDepth;

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
			// FoamLine
			half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
			depth = LinearEyeDepth(depth).r;

			float depthLine = _DepthFactor * (depth - IN.screenPos.w);
			fixed4 col = lerp(_MinColor, _MaxColor, clamp((depthLine - _MinDepth)/(_MaxDepth-_MinDepth),0,1));	

			//Foam
			float foamLine = 1 - saturate(_FoamLineDepthFactor * (depth - IN.screenPos.w));
			col += (foamLine * _FoamColor * 0.5);

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
