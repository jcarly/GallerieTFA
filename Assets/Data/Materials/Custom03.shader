// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom03"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = -1.02
		[Toggle]_Automaticanimation("Automatic animation", Float) = 0
		_Blinktimeinsecond("Blink time in second", Range( 0.1 , 10)) = 0
		_Apparition("Apparition", Range( -1 , 1)) = 0
		_Apparitionmask("Apparition mask", 2D) = "white" {}
		_Internalcolor("Internal color", Color) = (0,0,0,0)
		[HDR]_Emissive("Emissive", Color) = (0,0,0,0)
		_Emissivethickness("Emissive thickness", Range( 0.01 , 0.5)) = 0
		_scrollspeed("scroll speed", Vector) = (0,0,0,0)
		_Scale("Scale", Float) = 0
		_wolvy_baking_low_Wolvy_SHD_BaseColor("wolvy_baking_low_Wolvy_SHD_BaseColor", 2D) = "white" {}
		_wolvy_baking_low_Wolvy_SHD_Normal("wolvy_baking_low_Wolvy_SHD_Normal", 2D) = "bump" {}
		_wolvy_baking_low_Wolvy_SHD_Metalness("wolvy_baking_low_Wolvy_SHD_Metalness", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
			float3 worldPos;
		};

		uniform sampler2D _wolvy_baking_low_Wolvy_SHD_Normal;
		uniform float4 _wolvy_baking_low_Wolvy_SHD_Normal_ST;
		uniform sampler2D _wolvy_baking_low_Wolvy_SHD_BaseColor;
		uniform float4 _wolvy_baking_low_Wolvy_SHD_BaseColor_ST;
		uniform float4 _Internalcolor;
		uniform float4 _Emissive;
		uniform float _Emissivethickness;
		uniform sampler2D _Apparitionmask;
		uniform float2 _scrollspeed;
		uniform float _Automaticanimation;
		uniform float _Apparition;
		uniform float _Blinktimeinsecond;
		uniform float _Scale;
		uniform sampler2D _wolvy_baking_low_Wolvy_SHD_Metalness;
		uniform float4 _wolvy_baking_low_Wolvy_SHD_Metalness_ST;
		uniform float _Cutoff = -1.02;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_wolvy_baking_low_Wolvy_SHD_Normal = i.uv_texcoord * _wolvy_baking_low_Wolvy_SHD_Normal_ST.xy + _wolvy_baking_low_Wolvy_SHD_Normal_ST.zw;
			float3 switchResult13 = (((i.ASEVFace>0)?(UnpackNormal( tex2D( _wolvy_baking_low_Wolvy_SHD_Normal, uv_wolvy_baking_low_Wolvy_SHD_Normal ) )):(float3(0,0,-1))));
			o.Normal = switchResult13;
			float2 uv_wolvy_baking_low_Wolvy_SHD_BaseColor = i.uv_texcoord * _wolvy_baking_low_Wolvy_SHD_BaseColor_ST.xy + _wolvy_baking_low_Wolvy_SHD_BaseColor_ST.zw;
			float4 switchResult17 = (((i.ASEVFace>0)?(tex2D( _wolvy_baking_low_Wolvy_SHD_BaseColor, uv_wolvy_baking_low_Wolvy_SHD_BaseColor )):(_Internalcolor)));
			o.Albedo = switchResult17.rgb;
			float2 panner27 = ( 1.0 * _Time.y * _scrollspeed + i.uv_texcoord);
			float mulTime5 = _Time.y * ( ( 2.0 * UNITY_PI ) / _Blinktimeinsecond );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_3_0 = ( tex2D( _Apparitionmask, panner27 ).g + (( _Automaticanimation )?( sin( mulTime5 ) ):( _Apparition )) + ( ase_vertex3Pos.y * _Scale ) );
			float smoothstepResult22 = smoothstep( ( _Emissivethickness + 0.5 ) , 0.5 , temp_output_3_0);
			o.Emission = ( _Emissive * smoothstepResult22 ).rgb;
			float2 uv_wolvy_baking_low_Wolvy_SHD_Metalness = i.uv_texcoord * _wolvy_baking_low_Wolvy_SHD_Metalness_ST.xy + _wolvy_baking_low_Wolvy_SHD_Metalness_ST.zw;
			o.Metallic = tex2D( _wolvy_baking_low_Wolvy_SHD_Metalness, uv_wolvy_baking_low_Wolvy_SHD_Metalness ).r;
			o.Alpha = 1;
			clip( temp_output_3_0 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
231;190;1230;829;214.9706;1163.54;1.699805;True;True
Node;AmplifyShaderEditor.CommentaryNode;19;-1501.099,425.9717;Inherit;False;857;268;Automatic animation;5;9;7;11;5;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1451.099,578.9716;Inherit;False;Property;_Blinktimeinsecond;Blink time in second;2;0;Create;True;0;0;False;0;0;3.42;0.1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;9;-1399.099,475.9717;Inherit;False;1;0;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;11;-1175.099,509.9717;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1024.099,509.9717;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;29;-790.8972,16.1424;Inherit;False;Property;_scrollspeed;scroll speed;8;0;Create;True;0;0;False;0;0,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-822.8972,-142.8576;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;30;-99.92456,311.4659;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;6;-798.0984,505.9717;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-930.7704,281.2419;Inherit;False;Property;_Apparition;Apparition;3;0;Create;True;0;0;False;0;0;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;27;-443.8972,-73.8576;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-58.92456,468.4659;Inherit;False;Property;_Scale;Scale;9;0;Create;True;0;0;False;0;0;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-127.2486,-103.1653;Inherit;True;Property;_Apparitionmask;Apparition mask;4;0;Create;True;0;0;False;0;-1;None;d01457b88b1c5174ea4235d140b5fab8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;124.0754,369.4659;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-26.89734,-234.8576;Inherit;False;Property;_Emissivethickness;Emissive thickness;7;0;Create;True;0;0;False;0;0;0.5;0.01;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;20;-582.7705,355.242;Inherit;False;Property;_Automaticanimation;Automatic animation;1;0;Create;True;0;0;False;0;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;352.7513,-21.16532;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;338.1027,-190.8576;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;239.894,-840.4884;Inherit;False;Property;_Internalcolor;Internal color;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;467.1027,-379.8576;Inherit;False;Property;_Emissive;Emissive;6;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0.1037736,0.1037736,0.1037736,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;15;-48.10605,-518.4884;Inherit;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;0,0,-1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SmoothstepOpNode;22;539.1027,-197.8576;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.6;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;33;167.6755,-1091.734;Inherit;True;Property;_wolvy_baking_low_Wolvy_SHD_BaseColor;wolvy_baking_low_Wolvy_SHD_BaseColor;10;0;Create;True;0;0;False;0;-1;fc95f489f933fef46af330aba08c1b73;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;-117.0801,-783.1841;Inherit;True;Property;_wolvy_baking_low_Wolvy_SHD_Normal;wolvy_baking_low_Wolvy_SHD_Normal;11;0;Create;True;0;0;False;0;-1;e8777df792b909047b339763b990c562;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwitchByFaceNode;17;485.8939,-842.4884;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;35;119.8021,-381.2611;Inherit;True;Property;_wolvy_baking_low_Wolvy_SHD_Metalness;wolvy_baking_low_Wolvy_SHD_Metalness;12;0;Create;True;0;0;False;0;-1;8b4a39bc129dba24886e3fb1026fd34f;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;749.1027,-277.8576;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwitchByFaceNode;13;293.894,-524.4884;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1026.988,-379.5397;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom03;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;-1.02;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;9;0
WireConnection;11;1;7;0
WireConnection;5;0;11;0
WireConnection;6;0;5;0
WireConnection;27;0;28;0
WireConnection;27;2;29;0
WireConnection;1;1;27;0
WireConnection;32;0;30;2
WireConnection;32;1;31;0
WireConnection;20;0;21;0
WireConnection;20;1;6;0
WireConnection;3;0;1;2
WireConnection;3;1;20;0
WireConnection;3;2;32;0
WireConnection;26;0;25;0
WireConnection;22;0;3;0
WireConnection;22;1;26;0
WireConnection;17;0;33;0
WireConnection;17;1;16;0
WireConnection;24;0;23;0
WireConnection;24;1;22;0
WireConnection;13;0;34;0
WireConnection;13;1;15;0
WireConnection;0;0;17;0
WireConnection;0;1;13;0
WireConnection;0;2;24;0
WireConnection;0;3;35;0
WireConnection;0;10;3;0
ASEEND*/
//CHKSM=1240D201928F1DD13A817904C35D013B0FF5EBBA