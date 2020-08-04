Shader "Custom/Arm"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	_DisolveTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _DisolveTex;
        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed _Pos;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			int posx = ((int)IN.worldPos.x/4 )*4;
			int posy = ((int)IN.worldPos.y/4 )*4;
			int posz = ((int)IN.worldPos.z/4 )*4;
			float radius = 0.3;
			float dist = distance(fixed3(posx, posy, posz), IN.worldPos);
			float dist1 = distance(fixed3(posx+4, posy+4, posz+4), IN.worldPos);

			if (radius < dist&&radius<dist1) {
				o.Albedo = fixed4(110 / 255.0, 87 / 255.0, 139 / 255.0, 1);
			}
			else {
				o.Albedo = fixed4(1, 1, 1, 1);
			}

        }
        ENDCG
    }
    FallBack "Diffuse"
}
