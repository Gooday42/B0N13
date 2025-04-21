Shader "Unlit/GlitchShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShakePower ("Shake Power", Float) = 0.03
        _ShakeRate ("Shake Rate", Range(0.0, 1.0)) = 0.2
        _ShakeSpeed ("Shake Speed", Float) = 5.0
        _ShakeBlockSize ("Shake Block Size", Float) = 30.5
        _ShakeColorRate ("Shake Color Rate", Range(0.0, 1.0)) = 0.01
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
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
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ShakePower;
            float _ShakeRate;
            float _ShakeSpeed;
            float _ShakeBlockSize;
            float _ShakeColorRate;
            
            float random(float seed)
            {
                return frac(543.2543 * sin(dot(float2(seed, seed), float2(3525.46, -54.3415))));
            }
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                float enable_shift = step(random(trunc(_Time.y * _ShakeSpeed)), _ShakeRate);
                
                float2 fixed_uv = i.uv;
                fixed_uv.x += (random((trunc(i.uv.y * _ShakeBlockSize) / _ShakeBlockSize) + _Time.y) - 0.5) * _ShakePower * enable_shift;
                
                fixed4 pixel_color = tex2D(_MainTex, fixed_uv);
                pixel_color.r = lerp(
                    pixel_color.r,
                    tex2D(_MainTex, fixed_uv + float2(_ShakeColorRate, 0.0)).r,
                    enable_shift
                );
                pixel_color.b = lerp(
                    pixel_color.b,
                    tex2D(_MainTex, fixed_uv + float2(-_ShakeColorRate, 0.0)).b,
                    enable_shift
                );
                
                return pixel_color;
            }
            ENDCG
        }
    }
}
