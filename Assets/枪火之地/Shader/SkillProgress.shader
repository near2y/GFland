// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7113-OUT,alpha-5136-A,clip-5438-OUT;n:type:ShaderForge.SFN_If,id:5438,x:32415,y:33196,varname:node_5438,prsc:2|A-6909-OUT,B-1104-OUT,GT-327-OUT,EQ-327-OUT,LT-2463-OUT;n:type:ShaderForge.SFN_Slider,id:852,x:31504,y:33222,ptovrint:False,ptlb:skill progress,ptin:_skillprogress,varname:node_852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:100;n:type:ShaderForge.SFN_Vector1,id:2463,x:31991,y:33371,varname:node_2463,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:327,x:31960,y:33291,varname:node_327,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5136,x:31916,y:32501,ptovrint:False,ptlb:SampleTexture,ptin:_SampleTexture,varname:node_5136,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Dot,id:6909,x:31392,y:32993,varname:node_6909,prsc:2,dt:0|A-6430-OUT,B-4456-OUT;n:type:ShaderForge.SFN_Normalize,id:6430,x:31083,y:32856,varname:node_6430,prsc:2|IN-285-OUT;n:type:ShaderForge.SFN_Normalize,id:4456,x:31083,y:33084,varname:node_4456,prsc:2|IN-3949-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3949,x:30855,y:33096,varname:node_3949,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-5023-OUT;n:type:ShaderForge.SFN_ComponentMask,id:285,x:30828,y:32814,varname:node_285,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-3497-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:7665,x:30240,y:32738,varname:node_7665,prsc:2;n:type:ShaderForge.SFN_Subtract,id:2567,x:30500,y:32924,varname:node_2567,prsc:2|A-7665-XYZ,B-8122-XYZ;n:type:ShaderForge.SFN_ObjectPosition,id:8122,x:30240,y:32946,varname:node_8122,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:3216,x:30149,y:33356,varname:node_3216,prsc:2;n:type:ShaderForge.SFN_Transform,id:3497,x:30659,y:32978,varname:node_3497,prsc:2,tffrom:0,tfto:1|IN-2567-OUT;n:type:ShaderForge.SFN_Vector3,id:9743,x:30127,y:33188,varname:node_9743,prsc:2,v1:0,v2:0,v3:1000;n:type:ShaderForge.SFN_Multiply,id:5023,x:30380,y:33219,varname:node_5023,prsc:2|A-9743-OUT,B-3216-XYZ;n:type:ShaderForge.SFN_Color,id:3363,x:31868,y:32724,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3363,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:7113,x:32285,y:32731,varname:node_7113,prsc:2|A-5136-RGB,B-3363-RGB;n:type:ShaderForge.SFN_RemapRange,id:7322,x:31845,y:33163,varname:node_7322,prsc:2,frmn:0,frmx:100,tomn:-1,tomx:1|IN-852-OUT;n:type:ShaderForge.SFN_Negate,id:1104,x:32020,y:33163,varname:node_1104,prsc:2|IN-7322-OUT;proporder:852-5136-3363;pass:END;sub:END;*/

Shader "GFLand/SkillProgress" {
    Properties {
        _skillprogress ("skill progress", Range(0, 100)) = 0
        _SampleTexture ("SampleTexture", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _skillprogress;
            uniform sampler2D _SampleTexture; uniform float4 _SampleTexture_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_5438_if_leA = step(dot(normalize(mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb.rb),normalize((float3(0,0,1000)*objPos.rgb).rb)),(-1*(_skillprogress*0.02+-1.0)));
                float node_5438_if_leB = step((-1*(_skillprogress*0.02+-1.0)),dot(normalize(mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb.rb),normalize((float3(0,0,1000)*objPos.rgb).rb)));
                float node_327 = 1.0;
                clip(lerp((node_5438_if_leA*0.0)+(node_5438_if_leB*node_327),node_327,node_5438_if_leA*node_5438_if_leB) - 0.5);
////// Lighting:
////// Emissive:
                float4 _SampleTexture_var = tex2D(_SampleTexture,TRANSFORM_TEX(i.uv0, _SampleTexture));
                float3 emissive = (_SampleTexture_var.rgb*_Color.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_SampleTexture_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _skillprogress;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_5438_if_leA = step(dot(normalize(mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb.rb),normalize((float3(0,0,1000)*objPos.rgb).rb)),(-1*(_skillprogress*0.02+-1.0)));
                float node_5438_if_leB = step((-1*(_skillprogress*0.02+-1.0)),dot(normalize(mul( unity_WorldToObject, float4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb.rb),normalize((float3(0,0,1000)*objPos.rgb).rb)));
                float node_327 = 1.0;
                clip(lerp((node_5438_if_leA*0.0)+(node_5438_if_leB*node_327),node_327,node_5438_if_leA*node_5438_if_leB) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
