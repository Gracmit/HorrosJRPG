void NormalAndHeight_float(float3 worldPos, float3 normal, float3 wDir, out float3 mixedNormal, out float4 originalWorldNormal, out float heightOffset, out float3 oWorldPos, out float3 tangentViewDir, out float3 worldViewDir)
{
    #define INTERRA_OBJECT

    originalWorldNormal.xyz = TransformObjectToWorldNormal(normal);
    float2 hmUV = float2 ((worldPos.x - _TerrainPosition.x) * (1 / _TerrainSize.x), (worldPos.z - _TerrainPosition.z) * (1 / _TerrainSize.z));
    float hm = UnpackHeightmap(SAMPLE_TEXTURE2D_LOD(_TerrainHeightmapTexture, sampler_TerrainHeightmapTexture, hmUV, 0));
    heightOffset = worldPos.y - _TerrainPosition.y + (hm * -_TerrainHeightmapScale.y);
    float2 ts = float2(_TerrainHeightmapTexture_TexelSize.x, _TerrainHeightmapTexture_TexelSize.y);
    float hsX = _TerrainHeightmapScale.w / _TerrainHeightmapScale.x;
    float hsZ = _TerrainHeightmapScale.w / _TerrainHeightmapScale.z;

    float height[4];
    float3 terrainNormal;

    height[0] = UnpackHeightmap(SAMPLE_TEXTURE2D_LOD(_TerrainHeightmapTexture, sampler_TerrainHeightmapTexture, hmUV + float2(ts * float2(0, -1)), 0)).r * hsZ;
    height[1] = UnpackHeightmap(SAMPLE_TEXTURE2D_LOD(_TerrainHeightmapTexture, sampler_TerrainHeightmapTexture, hmUV + float2(ts * float2(-1, 0)), 0)).r * hsX;
    height[2] = UnpackHeightmap(SAMPLE_TEXTURE2D_LOD(_TerrainHeightmapTexture, sampler_TerrainHeightmapTexture, hmUV + float2(ts * float2(1, 0)), 0)).r * hsX;
    height[3] = UnpackHeightmap(SAMPLE_TEXTURE2D_LOD(_TerrainHeightmapTexture, sampler_TerrainHeightmapTexture, hmUV + float2(ts * float2(0, 1)), 0)).r * hsZ;

    terrainNormal.x = height[1] - height[2];
    terrainNormal.z = height[0] - height[3];
    terrainNormal.y = 1;

    float intersection = smoothstep(_NormIntersect.y, _NormIntersect.x, heightOffset);
    mixedNormal = lerp(normal, mul(unity_WorldToObject, float4(normalize(terrainNormal.xyz), 0.0)).xyz, intersection);

    float3 terrainWeights = pow(abs(terrainNormal), _TriplanarSharpness);
    terrainWeights = terrainWeights / (terrainWeights.x + terrainWeights.y + terrainWeights.z);
    originalWorldNormal.w = terrainWeights.y;

    oWorldPos = worldPos;
    worldViewDir = wDir;
    tangentViewDir = float3(0, 0, 0);

    #if defined (_TERRAIN_PARALLAX)
        float3 wNormal = normalize(TransformObjectToWorldNormal(mixedNormal));
        half3 axisSign = sign(wNormal);
        half3 tangentY = normalize(cross(wNormal.xyz, half3(0, 0, axisSign.y)));
        half3 bitangentY = normalize(cross(tangentY.xyz, wNormal.xyz)) * axisSign.y;
        half3x3 tbnY = half3x3(tangentY, bitangentY, wNormal);
        tangentViewDir = mul(tbnY, wDir);
    #endif
}