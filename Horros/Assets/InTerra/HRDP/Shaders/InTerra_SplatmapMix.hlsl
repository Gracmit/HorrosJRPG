#include "InTerra_Functions.hlsl"

#ifdef INTERRA_OBJECT
    void SplatmapMix_float(float heightOffset, float3 worldPos, float3 tangentViewDirTerrain, float3 worldViewDir, float4 worldNormal, float3 worldTangent, float3 worldBitangent, float4 mUV, out float3 albedo, out float3 mixedNormal, out float smoothness, out float metallic, out float occlusion)
#else
    void SplatmapMix(float2 splatBaseUV, float3 worldNormal, float3 tangentViewDirTerrain, float3 worldPos, out float3 mixedAlbedo, out float smoothness, out float metallic, out float occlusion, inout float3 mixedNormal)
#endif
{
    float4 mixedDiffuse;
    float3 wTangent;
    float3 wBTangent;

    float2 uvSplat[_LAYER_COUNT];
    float4 mask[_LAYER_COUNT];
    #ifdef TRIPLANAR
        float2 uvSplat_front[_LAYER_COUNT], uvSplat_side[_LAYER_COUNT];
        float4 mask_front[_LAYER_COUNT], mask_side[_LAYER_COUNT];
    #endif
    #ifdef _TERRAIN_DISTANCEBLEND
        float2 distantUV[_LAYER_COUNT];
        float4 dMask[_LAYER_COUNT];
        #ifdef TRIPLANAR
            float2 distantUV_front[_LAYER_COUNT], distantUV_side[_LAYER_COUNT];
            float4 dMask_front[_LAYER_COUNT], dMask_side[_LAYER_COUNT];
        #endif
    #endif

    #ifdef INTERRA_OBJECT 
        _Smoothness0 = _TerrainSmoothness.x;    _Metallic0 = _TerrainMetallic.x;    _NormalScale0 = _TerrainNormalScale.x;
        _Smoothness1 = _TerrainSmoothness.y;    _Metallic1 = _TerrainMetallic.y;    _NormalScale1 = _TerrainNormalScale.y;
        #ifndef _LAYERS_TWO
            _Smoothness2 = _TerrainSmoothness.z;    _Metallic2 = _TerrainMetallic.z;    _NormalScale2 = _TerrainNormalScale.z;
            _Smoothness3 = _TerrainSmoothness.w;    _Metallic3 = _TerrainMetallic.w;    _NormalScale3 = _TerrainNormalScale.w;
            #ifdef _TERRAIN_8_LAYERS
                _Smoothness4 = _TerrainSmoothness1.x;   _Metallic4 = _TerrainMetallic1.x;   _NormalScale4 = _TerrainNormalScale1.x;
                _Smoothness5 = _TerrainSmoothness1.y;   _Metallic5 = _TerrainMetallic1.y;   _NormalScale5 = _TerrainNormalScale1.y;
                _Smoothness6 = _TerrainSmoothness1.z;   _Metallic6 = _TerrainMetallic1.z;   _NormalScale6 = _TerrainNormalScale1.z;
                _Smoothness7 = _TerrainSmoothness1.w;   _Metallic7 = _TerrainMetallic1.w;   _NormalScale7 = _TerrainNormalScale1.w;
            #endif
        #endif          
    #else
        //In Diffuse remap alphas channels there are the values for parallax mapping, in red channel of _DiffuseRemapOffset there are the values of Layers scales adjust
        //Unity is subtracting the _DiffuseRemapOffset from _DiffuseRemapScale for Terrain shaders, but since the values are used for other purpose than originally there is need to add them back
        _DiffuseRemapScale0 += _DiffuseRemapOffset0;
        _DiffuseRemapScale1 += _DiffuseRemapOffset1;
        #ifndef _LAYERS_TWO 
            _DiffuseRemapScale2 += _DiffuseRemapOffset2;
            _DiffuseRemapScale3 += _DiffuseRemapOffset3;
            #ifdef _TERRAIN_8_LAYERS
                _DiffuseRemapScale4 += _DiffuseRemapOffset4;
                _DiffuseRemapScale5 += _DiffuseRemapOffset5;
                _DiffuseRemapScale6 += _DiffuseRemapOffset6;
                _DiffuseRemapScale7 += _DiffuseRemapOffset7;
            #endif
        #endif
    #endif

    //====================================================================================
    //--------------------------------- SPLAT MAP CONTROL --------------------------------
    //====================================================================================
    float4 blendMask[2];
    blendMask[0] = 0;
    blendMask[1] = 0;

    #ifdef INTERRA_OBJECT
        float2 splatBaseUV = (worldPos.xz - _TerrainPosition.xz) * (1 / _TerrainSize.xz);
        float3 tint = SAMPLE_TEXTURE2D(_TerrainColorTintTexture, sampler_TerrainColorTintTexture, splatBaseUV).rgb;

        #ifndef _LAYERS_ONE     
            float2 splatMapUV = (splatBaseUV * (_Control_TexelSize.zw - 1.0f) + 0.5f) * _Control_TexelSize.xy;
            blendMask[0] = SAMPLE_TEXTURE2D(_Control, sampler_Control, splatMapUV);
            #ifdef _TERRAIN_8_LAYERS
                blendMask[1] = SAMPLE_TEXTURE2D(_Control1, sampler_Control, splatMapUV);
            #endif 
        #else
            blendMask[0] = float4(1, 0, 0, 0);
            blendMask[1] = float4(0, 0, 0, 0);
        #endif 
    #else
        float2 blendUV0 = (splatBaseUV.xy * (_Control0_TexelSize.zw - 1.0f) + 0.5f) * _Control0_TexelSize.xy;                      
        blendMask[0] = SAMPLE_TEXTURE2D(_Control0, sampler_Control0, blendUV0);

        #ifdef _TERRAIN_8_LAYERS
            blendMask[1] = SAMPLE_TEXTURE2D(_Control1, sampler_Control0, blendUV0);
        #endif
        float3 tint = SAMPLE_TEXTURE2D(_TerrainColorTintTexture, sampler_TerrainColorTintTexture, splatBaseUV.xy).rgb;
    #endif
 
    #if defined(INTERRA_OBJECT) || defined(TRIPLANAR) || defined(_TERRAIN_TRIPLANAR_ONE)
        float3 flipUV = worldNormal.rgb < 0 ? -1 : 1;
        float3  weights = abs(worldNormal.rgb);
        weights = pow(weights, _TriplanarSharpness);
        weights = weights / (weights.x + weights.y + weights.z);

        #ifdef INTERRA_OBJECT            
            TriplanarOneToAllSteep(blendMask, (1 - worldNormal.w));
        #else
            TriplanarOneToAllSteep(blendMask, (1 - weights.y));
        #endif
    #endif  

    #if defined(_LAYERS_TWO)
            blendMask[0].r = _ControlNumber == 0 ? blendMask[0].r : _ControlNumber == 1 ? blendMask[0].g : _ControlNumber == 2 ? blendMask[0].b : blendMask[0].a;
            blendMask[0].g = 1 - blendMask[0].r;
    #endif

    #if defined(_TERRAIN_BLEND_HEIGHT) && !defined(_TERRAIN_BASEMAP_GEN) && !defined(_LAYERS_ONE) 
       float4 splatControlSum = blendMask[0] + blendMask[1];
       blendMask[0] = (splatControlSum.r + splatControlSum.g + splatControlSum.b + splatControlSum.a == 0.0f ? 1 : blendMask[0]); //this is preventing the black area when more than one pass
    #endif

    #if defined(_TERRAIN_BASEMAP_GEN_TRIPLANAR) || defined(_LAYERS_ONE) 
        blendMask[0] = float4(1, 0, 0, 0);
        blendMask[1] = float4(0, 0, 0, 0);
    #endif

    #if defined(_TERRAIN_DISTANCEBLEND)
        float4 dBlendMask[2];
        dBlendMask[0] = blendMask[0];
        dBlendMask[1] = blendMask[1];
    #endif

    //================================================================================
    //-------------------------------------- UVs -------------------------------------
    //================================================================================
    #ifdef INTERRA_OBJECT
        float2 mainUV = (mUV.xy * _BaseColorMap_ST.xy + _BaseColorMap_ST.zw);
        float2 mainParallaxOffset = 0;
        #ifdef _OBJECT_PARALLAX
            float3x3 objectToTangent = float3x3(worldTangent.xyz, worldBitangent.xyz, worldNormal.xyz);
            mainParallaxOffset = ParallaxOffset(_MaskMap, sampler_MaskMap, _ParallaxSteps, _ParallaxHeight, mainUV, mul(objectToTangent, worldViewDir), _ParallaxAffineSteps);
            mainUV += mainParallaxOffset;
        #endif
        float2 detailUV = (mUV.xy * _DetailMap_ST.xy + _DetailMap_ST.zw);
        float4 objectAlbedo = SAMPLE_TEXTURE2D(_BaseColorMap, sampler_BaseColorMap, mainUV) * _BaseColor;

        #ifndef _OBJECT_TRIPLANAR            
            _SteepDistortion = worldNormal.y > 0.5 ? 0 : (1 - worldNormal.y) * _SteepDistortion;
            _SteepDistortion *= objectAlbedo.r;
        #else            
            _SteepDistortion = 0;
        #endif

        float3 positionOffset = worldPos - _TerrainPosition;

        #ifndef TRIPLANAR
            UvSplat(uvSplat, positionOffset);
        #else
            float offsetZ = _DisableOffsetY == 1 ? -flipUV.z * worldPos.y : heightOffset * -flipUV.z + (worldPos.z);
            float offsetX = _DisableOffsetY == 1 ? -flipUV.x * worldPos.y : heightOffset * -flipUV.x + (worldPos.x);
              
            offsetZ -= _TerrainPosition.z;
            offsetX -= _TerrainPosition.x;

            UvSplat(uvSplat, uvSplat_front, uvSplat_side, positionOffset, offsetZ, offsetX, flipUV);
        #endif
    #else
        #ifndef TRIPLANAR
            UvSplat(uvSplat, splatBaseUV.xy);
        #else
            UvSplat(uvSplat, uvSplat_front, uvSplat_side, worldPos, splatBaseUV.xy);
        #endif
    #endif          

    //-------------------- PARALLAX OFFSET -------------------------                  
    #ifdef _TERRAIN_PARALLAX
        ParallaxUV(uvSplat, (tangentViewDirTerrain));
    #endif

    //--------------------- DISTANCE UV ------------------------
    #ifdef _TERRAIN_DISTANCEBLEND
        DistantUV(distantUV, uvSplat);
        #ifdef TRIPLANAR
            #ifdef _TERRAIN_TRIPLANAR_ONE
                distantUV_front[0] = uvSplat_front[0] * (_DiffuseRemapOffset0.r + 1) * _HT_distance_scale;
                distantUV_side[0] = uvSplat_side[0] * (_DiffuseRemapOffset0.r + 1) * _HT_distance_scale;
            #else
                DistantUV(distantUV_front, uvSplat_front);
                DistantUV(distantUV_side, uvSplat_side);
            #endif  
        #endif
    #endif

    //====================================================================================
    //-----------------------------------  MASK MAPS  ------------------------------------
    //====================================================================================
    SampleMask(mask, uvSplat, blendMask);
    #if defined(TRIPLANAR) && !defined(TERRAIN_SPLAT_ADDPASS)
        #ifdef _TERRAIN_TRIPLANAR_ONE
            SampleMaskTOL(mask_front, mask, uvSplat_front);
            SampleMaskTOL(mask_side, mask, uvSplat_side);
        #else
            SampleMask(mask_front, uvSplat_front, blendMask);
            SampleMask(mask_side, uvSplat_side, blendMask);
        #endif 
        MaskWeight(mask, mask_front, mask_side, blendMask, weights, _HeightTransition);
    #endif
    #ifdef _TERRAIN_DISTANCEBLEND		
        SampleMask(dMask, distantUV, dBlendMask);
        #if defined(TRIPLANAR) && !defined(TERRAIN_SPLAT_ADDPASS)
            #ifdef _TERRAIN_TRIPLANAR_ONE
                SampleMaskTOL(dMask_front, dMask, distantUV_front);
                SampleMaskTOL(dMask_side, dMask, distantUV_side);
            #else
                SampleMask(dMask_front, distantUV_front, dBlendMask);
                SampleMask(dMask_side, distantUV_side, dBlendMask);
            #endif
        MaskWeight(dMask, dMask_front, dMask_side, dBlendMask, weights, _Distance_HeightTransition);
        #endif
    #endif             

    //========================================================================================
    //------------------------------ HEIGHT MAP SPLAT BLENDINGS ------------------------------
    //========================================================================================
    #if defined(_TERRAIN_BLEND_HEIGHT) && !defined(_LAYERS_ONE) && !defined(TERRAIN_SPLAT_ADDPASS)
        HeightBlend(mask, blendMask, _HeightTransition);
        #ifdef _TERRAIN_DISTANCEBLEND
            HeightBlend(dMask, dBlendMask, _Distance_HeightTransition);
        #endif
    #endif 

    //========================================================================================
    //-------------------------------  ALBEDO, SMOOTHNESS & NORMAL ---------------------------
    //========================================================================================
    SampleSplat(uvSplat, blendMask, mask, mixedDiffuse, mixedNormal);
    #ifdef INTERRA_OBJECT
        wTangent = worldTangent;
        wBTangent = worldBitangent;
        mixedNormal = WorldTangent(wTangent, wBTangent, mixedNormal);
    #endif
        
    #if defined(TRIPLANAR) && !defined(TERRAIN_SPLAT_ADDPASS)
        float4 frontDiffuse;
        float3 frontNormal;
        float4 sideDiffuse;
        float3 sideNormal;

        #ifdef _TERRAIN_TRIPLANAR_ONE
            SampleSplatTOL(frontDiffuse, frontNormal, mixedDiffuse, mixedNormal, uvSplat_front, blendMask, mask);
            SampleSplatTOL(sideDiffuse, sideNormal, mixedDiffuse, mixedNormal, uvSplat_side, blendMask, mask);
        #else
            SampleSplat(uvSplat_front, blendMask, mask, frontDiffuse, frontNormal);
            SampleSplat(uvSplat_side, blendMask, mask, sideDiffuse, sideNormal);
        #endif 
        mixedDiffuse = (mixedDiffuse * weights.y) + (frontDiffuse * weights.z) + (sideDiffuse * weights.x);
        mixedNormal = TriplanarNormal(mixedNormal, wTangent, wBTangent, frontNormal, sideNormal, weights, flipUV);
    #endif

    #ifdef _TERRAIN_DISTANCEBLEND  
    
        float4 distantDiffuse;   
        float3 distantNormal;

        SampleSplat(distantUV, dBlendMask, dMask, distantDiffuse, distantNormal);
        #ifdef INTERRA_OBJECT
            distantNormal = WorldTangent(wTangent, wBTangent, distantNormal);
        #endif
        #if defined(TRIPLANAR) && !defined(TERRAIN_SPLAT_ADDPASS)
            float4 dFrontDiffuse;
            float3 dFontNormal;
            float4 dSideDiffuse;
            float3 dSideNormal;

            #ifdef _TERRAIN_TRIPLANAR_ONE
                SampleSplatTOL(dFrontDiffuse, dFontNormal, distantDiffuse, distantNormal, distantUV_front, dBlendMask, dMask);
                SampleSplatTOL(dSideDiffuse, dSideNormal, distantDiffuse, distantNormal, distantUV_side, dBlendMask, dMask);
            #else
                SampleSplat(distantUV_front, dBlendMask, dMask, dFrontDiffuse, dFontNormal);
                SampleSplat(distantUV_side, dBlendMask, dMask, dSideDiffuse, dSideNormal);
            #endif
            distantDiffuse = (distantDiffuse * weights.y) + (dFrontDiffuse * weights.z) + (dSideDiffuse * weights.x);
            distantNormal = TriplanarNormal(distantNormal, wTangent, wBTangent, dFontNormal, dSideNormal, weights, flipUV);
        #endif
                
        float dist = smoothstep(_HT_distance.x, _HT_distance.y, (distance(worldPos, _WorldSpaceCameraPos)));
        distantDiffuse = lerp(mixedDiffuse, distantDiffuse, _HT_cover);
        distantNormal = lerp(mixedNormal, distantNormal, _HT_cover);
        #ifdef _TERRAIN_BASEMAP_GEN            
            mixedDiffuse = distantDiffuse;
        #else
            mixedDiffuse = lerp(mixedDiffuse, distantDiffuse, dist); 
            mixedNormal = lerp(mixedNormal, distantNormal, dist);
        #endif        
    #endif

    #if !defined(TRIPLANAR_TINT) 
        mixedDiffuse.rgb = lerp(mixedDiffuse.rgb, (mixedDiffuse.rgb * tint), _TerrainColorTintStrenght).rgb;
    #endif


    //========================================================================================
    //--------------------------------   AMBIENT OCCLUSION   ---------------------------------
    //========================================================================================
    occlusion = 1;
    #if defined(_TERRAIN_MASK_MAPS) || defined(_TERRAIN_NORMAL_IN_MASK)
        occlusion = AmbientOcclusion(mask, blendMask);
        #if defined (_TERRAIN_DISTANCEBLEND)
            float dAo = AmbientOcclusion(dMask, dBlendMask);
            dAo = lerp(occlusion, dAo, _HT_cover);
            occlusion = lerp(occlusion, dAo, dist);
        #endif
    #endif

    //========================================================================================
    //--------------------------------------   METALLIC   ------------------------------------
    //========================================================================================

        metallic = MetallicMask(mask, blendMask);
        #if defined (_TERRAIN_DISTANCEBLEND)
            float dMetallic = MetallicMask(dMask, dBlendMask);
            dMetallic = lerp(metallic, dMetallic, _HT_cover);
            metallic = lerp(metallic, dMetallic, dist);
        #endif


    //=======================================================================================
    //==============================|   OBJECT INTEGRATION   |===============================
    //=======================================================================================
    #ifdef INTERRA_OBJECT	
        float steepWeights = _SteepIntersection == 1 ? saturate(worldNormal.y + _Steepness) : 1;
        float intersect1 = smoothstep(_Intersection.y, _Intersection.x, heightOffset) * steepWeights;
        float intersect2 = smoothstep(_Intersection2.y, _Intersection2.x, heightOffset) * (1 - steepWeights);
        float intersection = intersect1 + intersect2;
         
        float4 objectMask = SAMPLE_TEXTURE2D(_MaskMap, sampler_MaskMap, mainUV);
        objectMask.rgba = objectMask.rgba * _MaskMapRemapScale.rgba + _MaskMapRemapOffset.rgba;
        objectAlbedo.a = _HasMask == 1 ? objectMask.a : _Smoothness;
        float objectMetallic = _HasMask == 1 ? objectMask.r : _Metallic;
        float objectAo = _HasMask == 1 ? objectMask.g : _Ao;
        float height = objectMask.b;

        float sSum;
        #ifdef _TERRAIN_BLEND_HEIGHT 
            sSum = lerp(HeightSum(mask, blendMask), 1, intersection);
        #else 	
            sSum = 0.5;
        #endif 

        float2 heightIntersect = (1 / (1 * pow(2, float2(((1 - intersection) * height), (intersection * sSum)) * (-(_Sharpness)))) + 1) * 0.5;
        heightIntersect /= (heightIntersect.r + heightIntersect.g);

        #ifdef _OBJECT_DETAIL 
            #ifdef _OBJECT_PARALLAX
                detailUV += mainParallaxOffset * (_DetailMap_ST.xy / _BaseColorMap_ST.xy);
            #endif
            float3 dt = SAMPLE_TEXTURE2D(_DetailMap, sampler_DetailMap, detailUV).rgb;
            objectAlbedo.rgb = lerp(objectAlbedo.rgb, half(2.0) * dt, _DetailStrenght).rgb;
        #endif
                    
        float3 mainNorm = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, mainUV), _NormalScale);

        // avoid risk of NaN when normalizing.
        #if HAS_HALF
            mainNorm.z += half(0.01);
        #else
            mainNorm.z += 1e-5f;
        #endif
        
        #ifdef _OBJECT_DETAIL            
            float3 mainNormD = UnpackNormalScale(SAMPLE_TEXTURE2D(_DetailNormalMap, sampler_DetailNormalMap, detailUV), _DetailNormalMapScale);                  
            mainNorm = (lerp(mainNorm, BlendNormalRNM(mainNorm, mainNormD), _DetailStrenght));
        #endif
        mixedDiffuse = lerp(mixedDiffuse, objectAlbedo, heightIntersect.r);
        mixedNormal = lerp(mixedNormal, mainNorm, heightIntersect.r);
        metallic = lerp(metallic, objectMetallic, heightIntersect.r);
        occlusion = lerp(occlusion, objectAo, heightIntersect.r);
        albedo = mixedDiffuse.rgb;
    #else
        mixedAlbedo = mixedDiffuse.rgb;
    #endif

    smoothness = mixedDiffuse.a; 
    //=========================================================================================             
}
