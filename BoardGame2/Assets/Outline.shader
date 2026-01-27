Shader "Custom/OutlineScaled"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,0,1)
        _Scale ("Scale", Range(1.0, 1.2)) = 1.03
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1" }
        Cull Front
        ZWrite On
        ZTest LEqual

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _Scale;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                float4 scaled = v.vertex;
                scaled.xyz *= _Scale;          // scale in object space
                o.pos = UnityObjectToClipPos(scaled);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
