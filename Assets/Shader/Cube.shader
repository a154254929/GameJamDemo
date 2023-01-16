Shader "Unlit/Cube"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Top ("Top", int) = 10
        _OutlineWidth("Outline Width", Range(0.01, 1)) = 0.24
        _OutlineLimit("Outline Limit", Range(0.01, 4)) = 0.3
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _Alpha("Alpha", Range(0, 1.0)) = 1.0
        _ColorStrength("ColorStrength", Range(0, 1.0)) = 1.0
        [Toggle]_ExplodState1("Explod State1", int) = 0
        _ExplodColor1("Explod Color1", Color) = (1,1,1,1)
        [Toggle]_ExplodState2("Explod State2", int) = 0
        _ExplodColor2("Explod Color2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transprant" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            ZWrite On
            ColorMask 0
        }

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 pos : TEXCOOORD1;
                float3 normal_dir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Top;
            float _Alpha;
            float _ColorStrength;
            fixed4 _StepColor;
            int _ExplodState1;
            fixed4 _ExplodColor1;
            int _ExplodState2;
            fixed4 _ExplodColor2;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                o.normal_dir = normalize(UnityObjectToWorldNormal(v.normal));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed3 light_dir = normalize(_WorldSpaceLightPos0.xyz);
                float3 normal_dir = normalize(i.normal_dir);
                float lambert = dot(normal_dir, light_dir) * 0.5 + 0.5;
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 col = fixed4(lerp(texColor.rgb, _StepColor.rgb, _ColorStrength * texColor.a) * lerp(0.4, 1, step(0.4, lambert)), 1.0);
                //fixed4 col = saturate(floor(i.pos.y) / _Top);
                return fixed4(lerp(lerp(col, _ExplodColor1, _ExplodState1), _ExplodColor2, _ExplodState2).rgb, _Alpha);
            }
            ENDCG
        }
        Pass
        {
            Cull front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            float _OutlineLimit;
            fixed4 _OutlineColor;
            float _Alpha;

            v2f vert(appdata v)
            {
                v2f o;
                float4 pos = UnityObjectToClipPos(v.vertex);
                float3 viewNormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.tangent.xyz);
                float3 ndcNormal = normalize(TransformViewToProjection(viewNormal.xyz)) * min(pos.w, _OutlineLimit);//将法线变换到NDC空间
                float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));//将近裁剪面右上角位置的顶点变换到观察空间
                float aspect = abs(nearUpperRight.y / nearUpperRight.x);//求得屏幕宽高比
                ndcNormal.x *= aspect;
                pos.xy += 0.1 * _OutlineWidth * ndcNormal.xy;
                o.pos = pos;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(_OutlineColor.rgb, _Alpha);
            }
            ENDCG
        }
    }
}
