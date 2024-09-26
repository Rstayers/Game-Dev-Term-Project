Shader "Unlit/one-side"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 0
        _Color("Color Tint", Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull[_Cull]

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                    if (col.a < 0.1)
                    {
                        discard; // Avoid rendering almost fully transparent pixels
                    }
                    return col;
                }
                ENDCG
            }
        }
}
