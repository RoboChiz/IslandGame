Shader "Unlit/OceanFloor"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_BottomShade ("BottomShade", Color) = (1,1,1,1)
		_TopShade ("TopShade", Color) = (1,1,1,1)

		_BottomLimit ("BottomLimit", Range(-50,50)) = -5
		_TopLimit ("TopLimit", Range(-50,50)) = -1

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _BottomShade;
			fixed4 _TopShade;
			half _TopLimit;
			half _BottomLimit;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				float4 worldPos =  mul (unity_WorldToObject, i.vertex);
				fixed4 lerpCol = lerp(_TopShade, _BottomShade, (worldPos.y - _BottomLimit) / (_TopLimit-_BottomLimit));

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * lerpCol;
			}
			ENDCG
		}
	}
}
