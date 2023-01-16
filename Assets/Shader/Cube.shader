Shader "Unlit/Cube"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Top("_Top",  int) = 10
        _H("H", Range(0,360)) = 224
        _S("S", Range(0,1)) = 0.63
        _V("V", Range(0,1)) = 1
        _OutlineWidth("Outline Width", Range(0.01, 1)) = 0.24
        _OutlineLimit("Outline Limit", Range(0.01, 4)) = 0.3
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _Alpha("Alpha", Range(0, 1.0)) = 1.0
        _TexStrength("TexStrength", Range(0, 1.0)) = 1.0
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha
        Tags { "Queue" = "Ransparent" "IgnoreProjector" = "true" "RenderType" = "Transprant" }
        LOD 100

        /*Pass
        {
            ZWrite On
            ColorMask 0
        }*/

        Pass
        {
            ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

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
            float _Alpha;
            float _TexStrength;;
            int _Top;
            float _H;
            float _S;
            float _V;

            half3 RGBToHSV(half3 RGB)
            {
                half R = RGB.x, G = RGB.y, B = RGB.z;
                half3 hsv;
                half Max = max(R, max(G, B));
                half Min = min(R, max(G, B));
                if (R == Max)
                {
                    hsv.x = (G - B) / (Max - Min);
                }
                if (G == Max)
                {
                    hsv.x = 2 + (B - R) / (Max - Min);
                }
                if (B == Max)
                {
                    hsv.x = 4 + (R - G) / (Max - Min);
                }
                hsv.x = hsv.x * 60.0;
                if (hsv.x < 0)
                    hsv.x = hsv.x + 360;
                hsv.z = Max;
                hsv.y = (Max - Min) / Max;
                return hsv;
            }
            half3 HSVToRGB(half3 HSV)
            {
                half R, G, B;
                if (HSV.y == 0)
                {
                    R = G = B = HSV.z;
                }
                else
                {
                    HSV.x = HSV.x / 60.0;
                    int i = (int)HSV.x;
                    half f = HSV.x - (half)i;
                    half a = HSV.z * (1 - HSV.y);
                    half b = HSV.z * (1 - HSV.y * f);
                    half c = HSV.z * (1 - HSV.y * (1 - f));
                    switch (i)
                    {
                    case 0: R = HSV.z; G = c; B = a;
                        break;
                    case 1: R = b; G = HSV.z; B = a;
                        break;
                    case 2: R = a; G = HSV.z; B = c;
                        break;
                    case 3: R = a; G = b; B = HSV.z;
                        break;
                    case 4: R = c; G = a; B = HSV.z;
                        break;
                    default: R = HSV.z; G = a; B = b;
                        break;
                    }
                }
                return half3(R, G, B);
            }

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
                float3 light_dir = normalize(_WorldSpaceLightPos0.xyz);
                float3 normal_dir = normalize(i.normal_dir);
                float lambert = dot(normal_dir, light_dir) * 0.5 + 0.5;
                fixed4 col = fixed4(lerp(HSVToRGB(half3(_H * saturate(floor(i.pos.y) / _Top), _S, _V)), tex2D(_MainTex, i.uv).rgb, _TexStrength) * lerp(0.4, 1, step(0.4, lambert)), _Alpha);
                //fixed4 col = saturate(floor(i.pos.y) / _Top);
                return col;
            }
            ENDCG
        }
        Pass
        {
            Cull front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                return fixed4(_OutlineColor.rgb, 1);
            }
            ENDCG
        }
    }
}
