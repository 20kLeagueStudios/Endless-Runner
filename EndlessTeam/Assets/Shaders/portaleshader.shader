// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Portal"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+1" } // "Queue"="Geometry+1" (ma forse conviene geometry (2000) o geometry -1, da valutare

      // https://forum.unity.com/threads/stencil-shader-with-depth.452575/
      // https://www.youtube.com/watch?v=-NB2TR8IjE8

        ZWrite Off
        ColorMask 0

        /*
        Pass {

         Stencil 

        {
         Ref 1
         Comp Always
         Pass Replace

        }

        }
        */

        Stencil

        {
         Ref 1
         Comp Always
         Pass Replace

        }

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow

        // Global Shader values
        uniform float2 _BendAmount;
        uniform float3 _BendOrigin;
        uniform float _BendFalloff;

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
              float2 uv_MainTex;
        };

        float4 Curve(float4 v)
        {
            //HACK: Considerably reduce amount of Bend
            _BendAmount *= .0001;

            float4 world = mul(unity_ObjectToWorld, v);

            float dist = length(world.xz - _BendOrigin.xz);

            dist = max(0, dist - _BendFalloff);

            // Distance squared
            dist = dist * dist;

            world.xy += dist * _BendAmount;
            return mul(unity_WorldToObject, world);
      }

      void vert(inout appdata_full v)
      {
            v.vertex = Curve(v.vertex);
      }

      void surf(Input IN, inout SurfaceOutput o)
      {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
      }

      ENDCG
    }

        Fallback "Mobile/Diffuse"
}
