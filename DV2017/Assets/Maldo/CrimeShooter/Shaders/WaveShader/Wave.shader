// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "VascoGames/Mobile/Wave" {
	Properties {
	
		_MainTex ("Base (RGB),Alpha (A)", 2D) = "white" {}
		_Color ("Ambiant color", Color) = (0,0,0,0)
		SpeedValue ("Texture panning speed", float) = 0.2
		WorldPosIntensity ("Wave power", float) = 0.2
		WorldPosFrequence ("Wave frequence", float) = 0.2
		DeformSpeed ("Wave speed", float) = 0.2
		//NormalCorrectionIntensity ("Normal Correction", float) = 0.2
		offsetIntensity ("Vertexcolor influence", float) = 2
		DistanceFade("fade Distance", float) = 1
		_DistanceColor ("Distance color", Color) = (0,0,0,0)
		
	//	Powerfade("fade contrast", float) = 1
	}
	SubShader { 
		Pass{
			Tags { "Queue" = "Geometry" }
			LOD 200
				
			CGPROGRAM
				#pragma target 3.0
				#pragma vertex Vshader
				#pragma fragment Fshader

				#include "UnityCG.cginc"
				
				sampler2D _MainTex;
				uniform float4 _LightColor0;
				uniform float4 _MainTex_ST;
								
				float SpeedValue; 
				float WorldPosIntensity;
				float WorldPosFrequence;
				float DeformSpeed;
				float NormalCorrectionIntensity;
				float offsetIntensity;
				float3 _Color;
				float3 _DistanceColor;
				float DistanceFade;
				
				struct MyAppdata {
					float3 normal : NORMAL;
					float4 vertex : POSITION;
					float4 Tangent: TANGENT;
				  	float2 texcoord :TEXCOORD0;
					fixed4 color : COLOR;
				};
						
				struct V2Pix {
					float4 pos : SV_POSITION;
					half3 UVs	:	TEXCOORD1; 
					half4 pack : TEXCOORD2;
				};

				V2Pix Vshader (MyAppdata source)
				{
				// fetch ressources
					V2Pix transfert;
					float UvSpeed= _Time.x*SpeedValue;
					float2 Uvs = source.texcoord.xy ;
					transfert.UVs.xy = float2(Uvs.x-UvSpeed,Uvs.y)* _MainTex_ST.xy + _MainTex_ST.zw;
					transfert.pack.w = source.color.x;
					float3 Vcolor = source.color.xyz;
					float3 N = normalize(mul(unity_ObjectToWorld, float4(source.normal,0)).xyz);
					float3 WorldPosition = mul(unity_ObjectToWorld,source.vertex).xyz;
					
					// sine
					float alpha = (_Time.w*DeformSpeed+(Vcolor.x*offsetIntensity)+(WorldPosition*WorldPosFrequence));
					float sinAngle = sin(alpha);
					float4 Position = source.vertex+half4(0,sinAngle*WorldPosIntensity,0,0);
					transfert.pos = UnityObjectToClipPos(Position);
					// lighting
					float3 light = normalize(abs(_WorldSpaceLightPos0));
					half diffuseLight = saturate(dot(N,light.xyz));
					transfert.pack.xyz = (_Color+_LightColor0)*diffuseLight;
					// fade controle
					float fade = saturate((1-length(WorldPosition-_WorldSpaceCameraPos)*DistanceFade));
					transfert.UVs.z =fade;
					return transfert;
				}

				
				float4 Fshader (V2Pix recu) :COLOR
				{
					half2 Uvs =recu.UVs.xy;
					half3 MainTex = tex2D(_MainTex,half2(Uvs)).rgb;
					half4 Colorfinale;
					Colorfinale.xyz = lerp(_DistanceColor,MainTex*recu.pack.xyz,recu.UVs.z);
					Colorfinale.w =0.5;
					return float4(Colorfinale);
					
				}
			ENDCG
		}
	}
	FallBack "VertexLit"
}