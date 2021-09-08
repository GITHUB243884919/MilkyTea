Shader "UFrame/Water2DShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_DisplacementTex ("DisplacementTex", 2D) = "white" {}
		//_WaterTex ("WaterTex", 2D) = "white" {}
		//_MaskTex ("MaskTex", 2D) = "white" {}
        _WaterColor ("WaterColor", Color) = (1, 1, 1, 1)
		_BaseHeight ("BaseHeight", float) = 0.4
		_Turbulence ("Turbulence", float) = 1
		_ScrollOffset ("ScrollOffset", float) = 0
	}
	SubShader
	{
	    Tags { 
			"RenderType"="Transparent" 
			//"RenderType" = "Opaque"
			"Queue"="Transparent"
		}
		LOD 100

		Pass
		{
            Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _WaterColor;
			float _BaseHeight;
			fixed _Turbulence;
			fixed _ScrollOffset;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float wave(float x) {
				fixed waveOffset = 	cos((x - _Time + _ScrollOffset) * 60) * 0.004
									+ cos((x - 2 * _Time + _ScrollOffset) * 20) * 0.008
									+ sin((x + 2 * _Time + _ScrollOffset) * 35) * 0.01
									+ cos((x + 4 * _Time + _ScrollOffset) * 70) * 0.001;
				return _BaseHeight + waveOffset * _Turbulence;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 waterCol = _WaterColor;
				float waveHeight = wave(i.uv.x);
				fixed isTexelAbove = step(waveHeight, i.uv.y);
				fixed isTexelBelow = 1 - isTexelAbove;
                fixed4 col = waterCol;
                col.a = isTexelBelow;
                return col;
			}
			ENDCG
		}
	}
}
