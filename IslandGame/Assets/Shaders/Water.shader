Shader "Render Depth" {
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct vertexInput
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};

			// Unity built-in - NOT required in Properties
			uniform sampler2D _CameraDepthTexture;
			
			vertexOutput vert (vertexInput input)
			{
				vertexOutput output;
				
				//Convert position to world space
				output.pos = UnityObjectToClipPos(input.vertex);

				//Compte Depth
				output.screenPos = ComputeScreenPos(output.pos);

				return output;
			}
			
			fixed4 frag (vertexOutput input) : COLOR
			{
				// Sample the Camera Depth texture
				float4 s = UNITY_PROJ_COORD(input.screenPos);

				float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, s);
				float depth = LinearEyeDepth(depthSample).r;

				// Because the camera depth texture returns a value between 0-1
				// we can use that value to create a grayscale color to test the value output.
				float4 foamline = float4(depth, depth, depth, 1);
				return foamline;
			}
			ENDCG
        }
    }
}