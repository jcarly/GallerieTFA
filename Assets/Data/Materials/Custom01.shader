// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom01"
{
	Properties
	{
		_Objectcolor("Object color", Color) = (0,0,0,0)
		_Metalness("Metalness", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half filler;
		};

		uniform float4 _Objectcolor;
		uniform float _Metalness;
		uniform float _Roughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Objectcolor.rgb;
			o.Metallic = _Metalness;
			o.Smoothness = ( 1.0 - _Roughness );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1794;33;1628;898;1064;309;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-572,131;Inherit;False;Property;_Roughness;Roughness;2;0;Create;True;0;0;False;0;0;0.386;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-431,-208;Inherit;False;Property;_Objectcolor;Object color;0;0;Create;True;0;0;False;0;0,0,0,0;0.1258899,0.3827226,0.6509434,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-573,27;Inherit;False;Property;_Metalness;Metalness;1;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;5;-284,133;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;63,-29;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom01;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;4;0
WireConnection;0;0;1;0
WireConnection;0;3;3;0
WireConnection;0;4;5;0
ASEEND*/
//CHKSM=8AFC0E398AE145039CD02544F3383849220E4EAF