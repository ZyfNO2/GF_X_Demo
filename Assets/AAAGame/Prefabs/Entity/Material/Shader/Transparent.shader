Shader "Custom/GradientColor" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (1,0,0,1)
        _ColorB ("Color B", Color) = (0,0,1,1)
        _GradientDirection ("Gradient Direction", Vector) = (0,1,0,0)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _ColorA;
            float4 _ColorB;
            float4 _GradientDirection;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // 计算渐变颜色
                float t = dot(i.uv * _ScreenParams.xy, _GradientDirection.xyz) / _ScreenParams.x;
                fixed4 col = lerp(_ColorA, _ColorB, saturate(t));
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}