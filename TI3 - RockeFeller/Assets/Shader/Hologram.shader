Shader "Custom/Hologram"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Tint ("Tint", Color) = (0,1,1,1)

        _ScanlineSpeed ("Scanline Speed", Float) = 4
        _ScanlineDensity ("Scanline Density", Float) = 250

        _GlitchStrength ("Glitch Strength", Float) = 0.005
        _FlickerSpeed ("Flicker Speed", Float) = 10

        _Brightness ("Brightness", Float) = 2

        _Alpha ("Alpha", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        Lighting Off

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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Tint;

            float _ScanlineSpeed;
            float _ScanlineDensity;

            float _GlitchStrength;
            float _FlickerSpeed;

            float _Brightness;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.color = v.color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float glitch =
                    sin(_Time.y * 40 + uv.y * 80)
                    * _GlitchStrength;

                uv.x += glitch;

                fixed4 tex = tex2D(_MainTex, uv);

                float scan =
                    sin(
                        uv.y * _ScanlineDensity
                        - _Time.y * _ScanlineSpeed * 20
                    );

                scan = scan * 0.15 + 0.85;

                float flicker =
                    sin(_Time.y * _FlickerSpeed)
                    * 0.05 + 0.95;

                tex.rgb *= scan;

                tex.rgb *= flicker;

                tex.rgb *= _Tint.rgb;

                tex.rgb *= _Brightness;

                tex.rgb += glitch * 3;

                tex.a = saturate(tex.a * _Alpha * 2);

                return tex * i.color;
            }

            ENDCG
        }
    }
}