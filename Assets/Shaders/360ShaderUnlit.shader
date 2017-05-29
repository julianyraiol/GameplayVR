Shader "Unlit/360ShaderUnlit"
{
	Properties
	{
		_MainTex("Base(RGB)", 2D) = "white"{}
		_Contrast("Contrast", Range(-1,1)) = 0
		_Gama("Gama", Range(0,2)) = 0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 300
		Cull Front
		//This is used to print the texture inside of the sphere
		CGPROGRAM
		#pragma surface surf SimpleLambert
		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten)
		{
			half4 c;
			c.rgb = s.Albedo;
			return c;
		}

		sampler2D _MainTex;
		sampler2D _BumpMap;
		uniform float _Contrast;
		uniform float _Gama;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			IN.uv_MainTex.x = 1 - IN.uv_MainTex.x;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			/*			
			//Claridade de cada pixel (o quao proximo de branco ele é)
			float whitefact = (c.r + c.g + c.b) / 3;
			//Modificar o fator de contraste a partir da claridade do pixel, quanto mais proximo de preto ou branco, menos fator
			float w = 1 - whitefact;
			//Algoritmo de contrast
			
			if (_Contrast > 0.5) {
				if (whitefact > 0.5) {
					whitefact = 0.5 - (whitefact - 0.5);
				}
				c = (1 + whitefact) * (c - 0.5) + 0.5;
			}
			//Gama a partir da claridade (quanto mais claro, menos gama)
			float gama = 1.3 * (1.3 - whitefact);

			//Gama
			//c = 1 * pow((c/1), (1 / gama));
			
			//c = pow(((whitefact * 2) - 1), 2);
			*/
			o.Albedo = c.rgb * 0.5;
			
		}
		ENDCG
	}
	Fallback "Diffuse"
}