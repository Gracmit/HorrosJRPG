TEXTURE2D(_Control0);

#define DECLARE_TERRAIN_LAYER_TEXS(n)   \
    TEXTURE2D(_Splat##n);               \
    TEXTURE2D(_Normal##n);              \
    TEXTURE2D(_Mask##n)

DECLARE_TERRAIN_LAYER_TEXS(0);
DECLARE_TERRAIN_LAYER_TEXS(1);
DECLARE_TERRAIN_LAYER_TEXS(2);
DECLARE_TERRAIN_LAYER_TEXS(3);
#ifdef _TERRAIN_8_LAYERS
    DECLARE_TERRAIN_LAYER_TEXS(4);
    DECLARE_TERRAIN_LAYER_TEXS(5);
    DECLARE_TERRAIN_LAYER_TEXS(6);
    DECLARE_TERRAIN_LAYER_TEXS(7);
    TEXTURE2D(_Control1);
#endif

#undef DECLARE_TERRAIN_LAYER_TEXS

SAMPLER(sampler_Splat0);
SAMPLER(sampler_Control0);

#ifdef OVERRIDE_SPLAT_SAMPLER_NAME
    #define sampler_Splat0 OVERRIDE_SPLAT_SAMPLER_NAME
    SAMPLER(OVERRIDE_SPLAT_SAMPLER_NAME);
#endif


//=======================================================================
//----------------------------- InTerra SPLAT BLEND ---------------------
//=======================================================================
#if defined(_TERRAIN_MASK_MAPS) || defined(_TERRAIN_NORMAL_IN_MASK) || defined(_TERRAIN_BLEND_HEIGHT) || defined(_TERRAIN_PARALLAX)
    #define TERRAIN_MASK
#endif

#if (defined(_TERRAIN_TRIPLANAR) || defined(_OBJECT_TRIPLANAR) || defined(_TERRAIN_TRIPLANAR_ONE)) && !defined(_TERRAIN_BASEMAP_GEN)
    #define TRIPLANAR
#endif

#if defined(_TERRAIN_NORMAL_IN_MASK)
    #undef _NORMALMAP
    #define _NORMALMAP
#endif

#if defined(_TERRAIN_PARALLAX)
    #define PARALLAX
#endif
  
half4 _HT_distance;
float _HT_distance_scale, _HT_cover;
half _Distance_Height_blending, _Distance_HeightTransition, _TriplanarOneToAllSteep, _TriplanarSharpness;
half _ControlNumber;
half _ParallaxAffineStepsTerrain;
float _TerrainColorTintStrenght;
float3 _TerrainSizeXZPosY;
TEXTURE2D(_TerrainColorTintTexture);    SAMPLER(sampler_TerrainColorTintTexture);

#include "InTerra_SplatmapMix.hlsl" 

void InTerraTerrainLitShade(float2 uv, inout TerrainLitSurfaceData surfaceData, float3 positionWS, float3 normalWS, float3 tangentView)
{
    SplatmapMix(uv,
                normalWS,
                tangentView, 
                positionWS,
                surfaceData.albedo,
                surfaceData.smoothness, 
                surfaceData.metallic, 
                surfaceData.ao, 
                surfaceData.normalData);
}
//=======================================================================

void TerrainLitDebug(float2 uv, inout float3 baseColor)
{
#ifdef DEBUG_DISPLAY
    if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_CONTROL)
        baseColor = GetTextureDataDebug(_DebugMipMapMode, uv, _Control0, _Control0_TexelSize, _Control0_MipInfo, baseColor);
    else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER0)
        baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat0_ST.xy + _Splat0_ST.zw, _Splat0, _Splat0_TexelSize, _Splat0_MipInfo, baseColor);
    else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER1)
        baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat1_ST.xy + _Splat1_ST.zw, _Splat1, _Splat1_TexelSize, _Splat1_MipInfo, baseColor);
    else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER2)
        baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat2_ST.xy + _Splat2_ST.zw, _Splat2, _Splat2_TexelSize, _Splat2_MipInfo, baseColor);
    else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER3)
        baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat3_ST.xy + _Splat3_ST.zw, _Splat3, _Splat3_TexelSize, _Splat3_MipInfo, baseColor);
    #ifdef _TERRAIN_8_LAYERS
        else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER4)
            baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat4_ST.xy + _Splat4_ST.zw, _Splat4, _Splat4_TexelSize, _Splat4_MipInfo, baseColor);
        else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER5)
            baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat5_ST.xy + _Splat5_ST.zw, _Splat5, _Splat5_TexelSize, _Splat5_MipInfo, baseColor);
        else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER6)
            baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat6_ST.xy + _Splat6_ST.zw, _Splat6, _Splat6_TexelSize, _Splat6_MipInfo, baseColor);
        else if (_DebugMipMapModeTerrainTexture == DEBUGMIPMAPMODETERRAINTEXTURE_LAYER7)
            baseColor = GetTextureDataDebug(_DebugMipMapMode, uv * _Splat7_ST.xy + _Splat7_ST.zw, _Splat7, _Splat7_TexelSize, _Splat7_MipInfo, baseColor);
    #endif
#endif
}
