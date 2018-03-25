﻿Shader "Custom/Caustics" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Sand #1", 2D) = "white" {}
		_AltTex ("Sand #2", 2D) = "white" {}
		_MaskTex ("Sand Mask", 2D) = "white" {}
		_CausticMap ("Caustic Map", 2D) = "white" {}
		_CausticColour ("Caustic Colour", Color) = (1,1,1,1)

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_Intensity ("Intensity", Float) = 0.5
		_XSpeed ("XSpeed", Float) = 0.0
		_YSpeed ("YSpeed", Float) = 0.0

		_GroundRamp ("Ground Ramp", 2D) = "white" {}
		_minGroundHeight ("MinGroundHeight", Float) = 0.0
		_maxGroundHeight ("MaxGroundHeight", Float) = 0.0

		_minOceanHeight ("_minOceanHeight", Float) = 0.0
		_maxOceanHeight ("_maxOceanHeight", Float) = 0.0

		_topOffset ("Top Offset", Float) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AltTex;
		sampler2D _MaskTex;
		sampler2D _CausticMap;
		sampler2D _GroundRamp;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AltTex;
			float2 uv_MaskTex;
			float2 uv_CausticMap;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		half _Intensity;
		half _XSpeed;
		half _YSpeed;
		fixed4 _Color;
		fixed4 _CausticColour;

		half _minGroundHeight;
		half _maxGroundHeight;

		half _minOceanHeight;
		half _maxOceanHeight;
		half _topOffset;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color			
			fixed4 one = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 two = tex2D (_AltTex, IN.uv_AltTex) * _Color;
			fixed4 mask = tex2D (_MaskTex, IN.uv_MaskTex);

			fixed4 c = lerp(one, two, mask.x);

			fixed4 originalC = c;
			//Ground Colour Ramp

			//Percentage(x,a,b)=x−a/b−a
			float yPos = IN.worldPos.y;
			float percent = (yPos - _minGroundHeight)/(_maxGroundHeight - _minGroundHeight);
			float cutOffPercent = clamp((yPos - _minOceanHeight)/(_maxOceanHeight - _minOceanHeight), 0.0, 1.0);

			float4 white = float4(1,1,1,1);
			float4 colorRamp = tex2D (_GroundRamp, float2(percent, 0.5));
			c *= colorRamp;

			if(yPos >= _minGroundHeight)
			{
				c = lerp(c, originalC, cutOffPercent);
			}

			//Caustics
			float midPoint = _minGroundHeight + ((_maxGroundHeight - _minGroundHeight) / 2.0);
			float dist = 1;

			if(midPoint - yPos > 0)
			{
				//dist -= clamp(midPoint - yPos, 0, 1);
			}
			else
			{
				dist -= (yPos - midPoint) + _topOffset;
			}

			c += tex2D (_CausticMap, IN.uv_CausticMap + (float2(_XSpeed * _Time.x, _YSpeed * _Time.x) )) * _Intensity * clamp(dist,0,1) * _CausticColour;

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
