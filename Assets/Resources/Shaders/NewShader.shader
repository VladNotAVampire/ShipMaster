Shader "Custom/NewShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Value ("Just a kidding", Range(-1000, 1000)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float  _Value;

		inline float4 LightingBaseDiffuse(SurfaceOutput s, fixed3 lightDir, half3 viewDir)
		{
			float some = dot(s.Normal, viewDir);

			//if (some >= _Value)
			//{
			//	some = dot(s.Normal, lightDir);
			//}
			//else
			//{
				some = 0;
			//}

			float4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * some;
			col.a	= s.Alpha;
			return col;
		}

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o, SurfaceOutput s, fixed3 lightDir, half3 viewDir) {
			float some = dot(s.Normal, viewDir);

			fixed4 c = _Color;

			if (some > _Value)
			o.Albedo = c.rgb;
			else
			e.Albedo = 0;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
