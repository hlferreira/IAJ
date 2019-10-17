// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

<<<<<<< HEAD
Shader "RAIN/NavMeshContourShader"
{
    SubShader
    {
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
	
			float _height;
			float4 _colorSolid;
			float4 _colorOutline;
			sampler2D _outlineTexture;
			
			struct vert_out
			{
				float4 position : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			vert_out vert(appdata_base v)
			{
				vert_out tOut;
				tOut.position = UnityObjectToClipPos(v.vertex + float4(0, _height, 0, 0));
				tOut.texcoord = v.texcoord;
				
				return tOut;
			}
	
			float4 frag(vert_out f) : COLOR
			{
				return lerp(_colorOutline, _colorSolid, tex2D(_outlineTexture, f.texcoord.xy).x);
			}
				
			ENDCG
		}
    }
    FallBack "Diffuse"
}
=======
Shader "RAIN/NavMeshContourShader"
{
    SubShader
    {
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
	
			float _height;
			float4 _colorSolid;
			float4 _colorOutline;
			sampler2D _outlineTexture;
			
			struct vert_out
			{
				float4 position : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			vert_out vert(appdata_base v)
			{
				vert_out tOut;
				tOut.position = UnityObjectToClipPos(v.vertex + float4(0, _height, 0, 0));
				tOut.texcoord = v.texcoord;
				
				return tOut;
			}
	
			float4 frag(vert_out f) : COLOR
			{
				return lerp(_colorOutline, _colorSolid, tex2D(_outlineTexture, f.texcoord.xy).x);
			}
				
			ENDCG
		}
    }
    FallBack "Diffuse"
}
>>>>>>> 32342d1e6b1bad9cac424f01f79a5163fc7d6324
