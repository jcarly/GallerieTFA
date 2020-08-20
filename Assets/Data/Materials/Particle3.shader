// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Particle3"
{
	Properties
	{
		_Spritesheet("Spritesheet", 2D) = "white" {}
		_ColorLUT("ColorLUT", 2D) = "white" {}
		_Desaturate("Desaturate", Range( 0 , 1)) = 0
		_Intensityboost("Intensity boost", Range( 1 , 100)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform sampler2D _ColorLUT;
			uniform sampler2D _Spritesheet;
			uniform float4 _Spritesheet_ST;
			uniform float _Desaturate;
			uniform float _Intensityboost;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float2 uv_Spritesheet = i.ase_texcoord.xy * _Spritesheet_ST.xy + _Spritesheet_ST.zw;
				float4 tex2DNode1 = tex2D( _Spritesheet, uv_Spritesheet );
				float2 appendResult12 = (float2(tex2DNode1.g , _Time.y));
				float3 desaturateInitialColor7 = tex2D( _ColorLUT, appendResult12 ).rgb;
				float desaturateDot7 = dot( desaturateInitialColor7, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar7 = lerp( desaturateInitialColor7, desaturateDot7.xxx, _Desaturate );
				float4 screenPos = i.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth14 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
				float distanceDepth14 = abs( ( screenDepth14 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( 2.0 ) );
				float4 appendResult3 = (float4((( float4( desaturateVar7 , 0.0 ) * i.ase_color * _Intensityboost )).rgb , ( i.ase_color.a * tex2DNode1.g * saturate( distanceDepth14 ) )));
				
				
				finalColor = appendResult3;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17700
186;492;1906;927;1550.301;712.465;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;13;-1332.329,-494.1062;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1492.309,12.16917;Inherit;True;Property;_Spritesheet;Spritesheet;0;0;Create;True;0;0;False;0;-1;c48055209a5fdbf46977989506e1391b;c48055209a5fdbf46977989506e1391b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;12;-1044.329,-522.1062;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-434.5619,-468.8336;Inherit;False;Property;_Desaturate;Desaturate;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-778.0016,-555.5993;Inherit;True;Property;_ColorLUT;ColorLUT;1;0;Create;True;0;0;False;0;-1;12a5b4a34ba815b4b83a85211600d9fb;12a5b4a34ba815b4b83a85211600d9fb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;4;-554.5619,-241.8336;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;7;-102.5619,-545.8336;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-231.5619,-171.8336;Inherit;False;Property;_Intensityboost;Intensity boost;3;0;Create;True;0;0;False;0;0;75;1;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;14;-467.6663,198.1637;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;112.4381,-260.8336;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;15;-181.6663,224.1637;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;10;254.4381,-262.8336;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;62.4381,57.16638;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;3;490,-122;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;671,-123;Float;False;True;-1;2;ASEMaterialInspector;100;1;Particle3;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;12;0;1;2
WireConnection;12;1;13;0
WireConnection;2;1;12;0
WireConnection;7;0;2;0
WireConnection;7;1;8;0
WireConnection;6;0;7;0
WireConnection;6;1;4;0
WireConnection;6;2;9;0
WireConnection;15;0;14;0
WireConnection;10;0;6;0
WireConnection;5;0;4;4
WireConnection;5;1;1;2
WireConnection;5;2;15;0
WireConnection;3;0;10;0
WireConnection;3;3;5;0
WireConnection;0;0;3;0
ASEEND*/
//CHKSM=F9FF8A62E02333E8CC3FEA2C81F604414FC55296