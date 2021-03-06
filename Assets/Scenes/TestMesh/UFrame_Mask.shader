Shader "UFrame/UFrame_Mask"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_MaskTex("Mask", 2D) = "white" {}
	}
	SubShader
	{
		Tags { 
			"RenderType"="Transparent" 
			//"RenderType" = "Opaque"
			"Queue"="Transparent"
		}
		LOD 100
		Cull Off
		Lighting Off
		//ZWrite Off
		Fog {Mode Off}
		//Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha
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
			float4 _Color;

			sampler2D _MaskTex;
			float4 _MaskTex_ST;

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
				fixed4 mask = tex2D(_MaskTex, i.uv);
				float4 col = _Color;
				col.a = mask.a;

				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				//col.a = 255; 
				return col;
			}
			ENDCG
		}
	}
}
