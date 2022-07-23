using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace InTerra
{
	public class InTerra_TerrainShaderGUI : ShaderGUI
	{
		bool setMinMax = false;
		bool setNormScale = false;
		bool moveLayer = false;
		bool pomSetting = false;
		bool layersScales = false;
		bool colorTintLayers = false;
		bool colorTintTexture = false;
		int layerToFirst = 0;
		string shaderName = " ";

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			Material targetMat = materialEditor.target as Material;
			bool normInMask = targetMat.IsKeywordEnabled("_TERRAIN_NORMAL_IN_MASK");

			//----------------------------- FONT STYLES ----------------------------------
			var styleButtonBold = new GUIStyle(GUI.skin.button) {fontStyle = FontStyle.Bold};
			var styleBoldCenter = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
			var styleRight = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight };
			var styleMini = new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleRight };
			var styleBigBold = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 13 };
			//----------------------------------------------------------------------------

			Terrain terrain = null;
			if (Selection.activeGameObject != null)
			{
				terrain = Selection.activeGameObject.GetComponent<Terrain>();
			}

			if (terrain == null)
			{
				EditorGUILayout.HelpBox("No Terrain is selected, some settings may not work!", MessageType.Warning);
			}

			if (targetMat.shader.name != shaderName)
			{
				EditorApplication.delayCall += () =>
				{
					if (targetMat.shader.name == InTerra_Data.DiffuseTerrainShaderName)
					{
						SetKeyword(targetMat, targetMat.GetTexture("_TerrainColorTintTexture") && targetMat.GetFloat("_TerrainColorTintStrenght") > 0, "_TERRAIN_TINT_TEXTURE");
					}
					InTerra_Data.UpdateTerrainData();
				};				
				shaderName = targetMat.shader.name;
			}


			if (targetMat.shader.name == InTerra_Data.DiffuseTerrainShaderName)
			{
				using (new GUILayout.VerticalScope(EditorStyles.helpBox))
				{
					HeightmapBlending("Heightmap blending", "Heightmap based texture transition.");					
				}
			}
			else
			{
				//=============================  MASK MAP FEATURES  =============================
				using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
				{
					EditorGUILayout.LabelField("Mask Map Features", styleBoldCenter);
					using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
					{
						//------------------------ HEIGHTMAP BLENDING ------------------------
						HeightmapBlending("Heightmap Blending", "Heightmap based texture transition.");

						//------------------------ PARALLAX OCCLUSION MAPPING ------------------------
						bool parallax = targetMat.IsKeywordEnabled("_TERRAIN_PARALLAX");

						EditorGUI.BeginChangeCheck();
						EditorStyles.label.fontStyle = FontStyle.Bold;

						parallax = EditorGUILayout.ToggleLeft(LabelAndTooltip("Parallax Occlusion Mapping", "An illusion of 3D effect created by offsetting the texture depending on heightmap."), parallax);
						EditorStyles.label.fontStyle = FontStyle.Normal;
						

						if (EditorGUI.EndChangeCheck())
						{
							materialEditor.RegisterPropertyChangeUndo("InTerra Parallax Occlusion Mapping");
							SetKeyword(targetMat, parallax, "_TERRAIN_PARALLAX");
							InTerra_Data.UpdateTerrainData();
						}

						if (parallax)
						{
							using (new GUILayout.VerticalScope(EditorStyles.helpBox))
							{
								using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
								{
									EditorGUI.BeginChangeCheck();
									float affineSteps = targetMat.GetFloat("_ParallaxAffineStepsTerrain");
									GUILayout.Label(LabelAndTooltip("Affine Steps: ", "The higher number the smoother transition between steps, but also the higher number will increase performance heaviness."));
									affineSteps = EditorGUILayout.IntField((int)affineSteps, GUILayout.MaxWidth(30));									
									affineSteps = Mathf.Clamp(affineSteps, 1, 10);
									
									if (EditorGUI.EndChangeCheck())
									{
										materialEditor.RegisterPropertyChangeUndo("InTerra Parallax Values");
										targetMat.SetFloat("_ParallaxAffineStepsTerrain", affineSteps);
									}
								}
								EditorGUI.indentLevel = 1;								
								pomSetting = EditorGUILayout.Foldout(pomSetting, "Layers Setting", true);
								EditorGUI.indentLevel = 0;
								if (pomSetting && terrain != null)
								{
									using (new GUILayout.VerticalScope(EditorStyles.helpBox))
									{

										using (new GUILayout.HorizontalScope())
										{
											GUILayout.Label(LabelAndTooltip("Height", "The value of the height illusion."), styleMini, GUILayout.MinWidth(40));
											GUILayout.Label(LabelAndTooltip("Steps", "Each step is creating a new layer for offsetting. The more steps, the more precise the parallax effect will be, but also the higher number will increase performance heaviness."), styleMini, GUILayout.MaxWidth(30));
										}
										for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
										{
											TerrainLayer tl = terrain.terrainData.terrainLayers[i];
											if (tl)
											{
												Vector4 amplitude = tl.diffuseRemapMax;
												Vector4 steps = tl.diffuseRemapMin;
												EditorGUI.BeginChangeCheck();
												GUILayout.BeginHorizontal();
												amplitude.w = EditorGUILayout.FloatField((i + 1).ToString() + ". " + tl.name + " :", amplitude.w, GUILayout.MinWidth(60));
												amplitude.w = Mathf.Clamp(amplitude.w, 0, 15);
												steps.w = EditorGUILayout.IntField((int)steps.w, GUILayout.MaxWidth(25));
												steps.w = Mathf.Clamp(steps.w, 0, 50);
												GUILayout.EndHorizontal();
												if (EditorGUI.EndChangeCheck())
												{
													Undo.RecordObject(terrain.terrainData.terrainLayers[i], "InTerra Parallax Values");
													tl.diffuseRemapMax = amplitude;
													tl.diffuseRemapMin = steps;

												}
											}
										}
									}
								}
							}
						}

						//------------------------ OCCLUSION, METALLIC, SMOOTHNESS ------------------------
						bool maskMaps = targetMat.IsKeywordEnabled("_TERRAIN_MASK_MAPS") && !normInMask;
						string omLabel = "A.Occlusion, Metallic, Smoothness";
						string omTooltip = "This option applies the Ambient occlusion, Metallic and Smoothness maps from Mask Map.";
						EditorGUI.BeginChangeCheck();

						EditorStyles.label.fontStyle = FontStyle.Bold;	
						maskMaps = normInMask ? false : EditorGUILayout.ToggleLeft(LabelAndTooltip(omLabel, omTooltip), maskMaps);

						EditorStyles.label.fontStyle = FontStyle.Normal;
						if (EditorGUI.EndChangeCheck())
						{
							materialEditor.RegisterPropertyChangeUndo("InTerra Terrain Mask maps");
							SetKeyword(targetMat, maskMaps, "_TERRAIN_MASK_MAPS");
							InTerra_Data.UpdateTerrainData();
						}
				
						//-------------------------------- NORMAL IN MASK -------------------------------- 				
						EditorGUI.BeginChangeCheck();
						EditorStyles.label.fontStyle = FontStyle.Bold;
						normInMask = EditorGUILayout.ToggleLeft(LabelAndTooltip("Normal map in Mask map", "The Normal map will be taken from Mask map Green and Alpha channel, A.Occlusion from Red channel and Heightmap from Blue."), normInMask);
						EditorStyles.label.fontStyle = FontStyle.Normal;

						if (EditorGUI.EndChangeCheck())
						{
							materialEditor.RegisterPropertyChangeUndo("InTerra Normal To Mask");
							SetKeyword(targetMat, normInMask, "_TERRAIN_NORMAL_IN_MASK");
							if (normInMask) SetKeyword(targetMat, false, "_TERRAIN_MASK_MAPS");
							InTerra_Data.UpdateTerrainData();
						}

						if (!normInMask)
						{
							if (GUILayout.Button(LabelAndTooltip("Mask Map Creator", "Open window for creating Mask Map"), styleButtonBold))
							{
								InTerra_MaskCreator.OpenWindow(false);
							}
						}
						else
						{
							using (new GUILayout.VerticalScope(EditorStyles.helpBox))
							{
								if (GUILayout.Button(LabelAndTooltip("Normal-Mask Map Creator", "Open window for creating Mask Map including Normal map."), styleButtonBold))
								{
									InTerra_MaskCreator.OpenWindow(true);
								}

								EditorGUI.indentLevel = 1;
								setNormScale = EditorGUILayout.Foldout(setNormScale, "Normal Scales", true);

								if (setNormScale && terrain != null)
								{
									for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
									{
										TerrainLayer tl = terrain.terrainData.terrainLayers[i];
										if (tl)
										{										
											float nScale = tl.normalScale;
											EditorGUI.BeginChangeCheck();
											nScale = EditorGUILayout.FloatField((i + 1).ToString() + ". " + tl.name + " :", nScale);
											if (EditorGUI.EndChangeCheck())
											{
												Undo.RecordObject(terrain.terrainData.terrainLayers[i], "InTerra TerrainLayer Normal Scale");
												tl.normalScale = nScale;
											}
										}
									}
								}
							}
							EditorGUI.indentLevel = 0;
						}
					}
				}
			}

			//========================= HIDE TILING (DISTANCE BLENDING) ========================
			bool distanceBlending = targetMat.IsKeywordEnabled("_TERRAIN_DISTANCEBLEND");
			Vector4 distance = targetMat.GetVector("_HT_distance");
		
		
			using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
			{
				EditorGUI.BeginChangeCheck();
				EditorStyles.label.fontStyle = FontStyle.Bold;
				distanceBlending = EditorGUILayout.ToggleLeft(LabelAndTooltip("Hide Tiling", "Hides tiling by covering the texture by its scaled up version in the given distance from the camera."), distanceBlending);
				EditorStyles.label.fontStyle = FontStyle.Normal;

				if (EditorGUI.EndChangeCheck())
				{
					materialEditor.RegisterPropertyChangeUndo("InTerra HideTiling Keyword");
					SetKeyword(targetMat, distanceBlending, "_TERRAIN_DISTANCEBLEND");
					InTerra_Data.UpdateTerrainData();
				}

				EditorGUI.BeginChangeCheck();
				if (distanceBlending)
				{
					using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
					{					
						PropertyLine("_HT_distance_scale", "Scale", "This value is multiplying the scale of the Texture of a distant area.");
						EditorGUI.indentLevel = 1;
						layersScales = EditorGUILayout.Foldout(layersScales, "Adjust Layers Scales", true);
						if (layersScales && terrain != null)
						{
							for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
							{
								TerrainLayer tl = terrain.terrainData.terrainLayers[i];
								if (tl)
								{
									Vector4 scale = tl.diffuseRemapMin;
									EditorGUI.BeginChangeCheck();
									scale.x = EditorGUILayout.Slider((i + 1).ToString() + ". " + tl.name + " :", scale.x, -1, 1);
									if (EditorGUI.EndChangeCheck())
									{
										Undo.RecordObject(terrain.terrainData.terrainLayers[i], "InTerra Terrain Layers Scales");
										tl.diffuseRemapMin = scale;
									}
								}
							}
						}
						EditorGUI.indentLevel = 0;

						PropertyLine("_HT_cover", "Cover strenght", "Strength of covering the Terrain textures in the distant area.");

						using (new GUILayout.HorizontalScope())
						{
							EditorGUILayout.LabelField(LabelAndTooltip("Distance", "The distance where the covering will start. The closer the sliders are, the sharper is the transition."), GUILayout.Width(70));
							EditorGUILayout.LabelField(distance.x.ToString("0.0"), GUILayout.Width(30));
							EditorGUILayout.MinMaxSlider(ref distance.x, ref distance.y, distance.z, distance.w);
							EditorGUILayout.LabelField(distance.y.ToString("0.0"), GUILayout.Width(30));
						}

						EditorGUI.indentLevel = 1;
						setMinMax = EditorGUILayout.Foldout(setMinMax, "Adjust Distance range", true);
						EditorGUI.indentLevel = 0;
						if (setMinMax)
						{
							using (new GUILayout.HorizontalScope()) 
							{
								EditorGUILayout.LabelField("Min:", styleRight, GUILayout.Width(45));
								distance.z = EditorGUILayout.DelayedFloatField(distance.z, GUILayout.MinWidth(50));

								EditorGUILayout.LabelField("Max:", styleRight, GUILayout.Width(45));
								distance.w = EditorGUILayout.DelayedFloatField(distance.w, GUILayout.MinWidth(50));
							}
						}

						distance.x = Mathf.Clamp(distance.x, distance.z, distance.w);
						distance.y = Mathf.Clamp(distance.y, distance.z, distance.w);

					}
				
				}

				if (EditorGUI.EndChangeCheck())
				{
					materialEditor.RegisterPropertyChangeUndo("InTerra HideTiling Value");
					targetMat.SetVector("_HT_distance", distance);
				}
			}

			//============================= TRIPLANAR ============================= 
			bool triplanar = targetMat.IsKeywordEnabled("_TERRAIN_TRIPLANAR") || targetMat.IsKeywordEnabled("_TERRAIN_TRIPLANAR_ONE");
			bool triplanarOneLayer = targetMat.IsKeywordEnabled("_TERRAIN_TRIPLANAR_ONE");
			bool applyFirstLayer = targetMat.GetFloat("_TriplanarOneToAllSteep") == 1;

			using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
			{
				EditorGUI.BeginChangeCheck();

				EditorStyles.label.fontStyle = FontStyle.Bold;
				triplanar = EditorGUILayout.ToggleLeft(LabelAndTooltip("Triplanar Mapping", "The Texture on steep slopes of Terrain will not be stretched."), triplanar);
				EditorStyles.label.fontStyle = FontStyle.Normal;
				if (triplanar && terrain != null)				
				{
					targetMat.SetVector("_TerrainSize", terrain.terrainData.size); //needed for triplanar UV
				}

				if (triplanar)
				{					
					using (new GUILayout.VerticalScope(EditorStyles.helpBox))
					{
						PropertyLine("_TriplanarSharpness", "Sharpness", "Sharpness of the textures transitions between planar projections.");
						triplanarOneLayer = EditorGUILayout.ToggleLeft(LabelAndTooltip("First Layer Only", "Only the first Terrain Layer will be triplanared - this option is for performance reasons."), triplanarOneLayer, GUILayout.MaxWidth(115));

						if (triplanarOneLayer)
						{							
							EditorGUI.indentLevel = 1;
							EditorStyles.label.fontSize = 11;
							applyFirstLayer = EditorGUILayout.ToggleLeft(LabelAndTooltip("Apply first Layer to all steep slopes", "The first Terrain Layer will be automaticly applied to all steep slopes."), applyFirstLayer);
							EditorStyles.label.fontSize = 12;

							if (terrain && terrain.terrainData.alphamapLayers > 1)
							{
								moveLayer = EditorGUILayout.Foldout(moveLayer, "Move Layer To First Position", true);
								EditorGUI.indentLevel = 0;
								if (moveLayer)
								{
									List<string> tl = new List<string>();
									for (int i = 1; i < terrain.terrainData.alphamapLayers; i++)
									{
										tl.Add((i + 1).ToString() + ". " + terrain.terrainData.terrainLayers[i].name.ToString()); 
									}
									if ((layerToFirst + 1) >= terrain.terrainData.alphamapLayers) layerToFirst = 0;
									TerrainLayer terainLayer = terrain.terrainData.terrainLayers[layerToFirst + 1];

									using (new GUILayout.HorizontalScope())
									{									
										if (terainLayer && AssetPreview.GetAssetPreview(terainLayer.diffuseTexture))
										{
											GUI.Box(EditorGUILayout.GetControlRect(GUILayout.Width(50),  GUILayout.Height(50)), AssetPreview.GetAssetPreview(terainLayer.diffuseTexture));		
										}
										else
										{
											EditorGUILayout.GetControlRect(GUILayout.Width(50), GUILayout.Height(50));
										}
										using (new GUILayout.VerticalScope())
										{										
											layerToFirst = EditorGUILayout.Popup("", layerToFirst, tl.ToArray(), GUILayout.MinWidth(170));
											if (GUILayout.Button("Move Layer to First Position", GUILayout.MinWidth(170), GUILayout.Height(27)))
											{											
												MoveLayerToFirstPosition(terrain, layerToFirst + 1);
												InTerra_Data.UpdateTerrainData();
											}
										}
										EditorGUILayout.GetControlRect(GUILayout.MinWidth(10));
									}
								}
							}							
						}
					}
				}
				EditorGUI.indentLevel = 0;

				if (EditorGUI.EndChangeCheck())
				{
					materialEditor.RegisterPropertyChangeUndo("InTerra Triplanar Terrain");
					SetKeyword(targetMat, triplanar && triplanarOneLayer, "_TERRAIN_TRIPLANAR_ONE");
					SetKeyword(targetMat, triplanar && !triplanarOneLayer, "_TERRAIN_TRIPLANAR");
					if (applyFirstLayer && triplanar && triplanarOneLayer) targetMat.SetFloat("_TriplanarOneToAllSteep", 1); else targetMat.SetFloat("_TriplanarOneToAllSteep", 0);
				}

				if (targetMat.GetFloat("_NumLayersCount") > 4)
				{
					EditorGUILayout.HelpBox("Triplanar Features will be applied on Terrain Base Map only if \"First Layer only\" is checked and there are not more than four Layers.", MessageType.Info);
				}
			}

			//========================= TWO LAYERS ONLY ===========================
			bool twoLayers = targetMat.IsKeywordEnabled("_LAYERS_TWO");

			using (new GUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUI.BeginChangeCheck();

				EditorStyles.label.fontStyle = FontStyle.Bold;
				twoLayers = EditorGUILayout.ToggleLeft(LabelAndTooltip("First Two Layers Only", "The shader will sample only first twoo layers."), twoLayers);
				EditorStyles.label.fontStyle = FontStyle.Normal;
				if (EditorGUI.EndChangeCheck())
				{
					materialEditor.RegisterPropertyChangeUndo("InTerra Terrain Two Layers");
					SetKeyword(targetMat, twoLayers, "_LAYERS_TWO");
				}
			}

			//========================= COLOR TINT ===========================
			using (new GUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("Color Tint", styleBigBold);
				EditorGUI.indentLevel = 1;
				colorTintTexture = EditorGUILayout.Foldout(colorTintTexture, "Color Tint Texture", true);
				EditorGUI.indentLevel = 0;
			
				if (colorTintTexture && terrain != null)
				{
					using (new GUILayout.HorizontalScope())
					{

						Texture ColorTintTexture = targetMat.GetTexture("_TerrainColorTintTexture");
						float tintStrenght = targetMat.GetFloat("_TerrainColorTintStrenght");

						EditorGUI.BeginChangeCheck();
						ColorTintTexture = (Texture2D)EditorGUILayout.ObjectField(ColorTintTexture, typeof(Texture2D), false, GUILayout.Height(65), GUILayout.Width(65));

						using (new GUILayout.VerticalScope(EditorStyles.helpBox))
						{
							EditorGUILayout.LabelField("Tint Strenght:", GUILayout.MinWidth(35));
							tintStrenght = EditorGUILayout.Slider(tintStrenght, 0, 1, GUILayout.MinWidth(35));
							EditorGUILayout.LabelField("", GUILayout.Width(35));
						}

						if (EditorGUI.EndChangeCheck())
						{
							materialEditor.RegisterPropertyChangeUndo("InTerra Terrain Color Tint Texture");
							targetMat.SetTexture("_TerrainColorTintTexture", ColorTintTexture);
							targetMat.SetFloat("_TerrainColorTintStrenght", tintStrenght);
							if (targetMat.shader.name == InTerra_Data.DiffuseTerrainShaderName)
							{
								SetKeyword(targetMat, targetMat.GetTexture("_TerrainColorTintTexture") && tintStrenght > 0, "_TERRAIN_TINT_TEXTURE");
							}
						}
					}
				}

				EditorGUI.indentLevel = 1;
				colorTintLayers = EditorGUILayout.Foldout(colorTintLayers, "Layers Color Tint", true);

				if (colorTintLayers && terrain != null)
				{
					for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
					{
						TerrainLayer tl = terrain.terrainData.terrainLayers[i];
						if (tl)
						{
							Vector4 color = tl.diffuseRemapMax;
							EditorGUI.BeginChangeCheck();
							color = EditorGUILayout.ColorField(new GUIContent() { text = (i + 1).ToString() + ". " + tl.name} , color, true, false, false);

							if (EditorGUI.EndChangeCheck())
							{
								Undo.RecordObject(terrain.terrainData.terrainLayers[i], "InTerra TerrainLayer Color Tint");
								tl.diffuseRemapMax = color;
							}
						}
					}
				}
				EditorGUI.indentLevel = 0;
			}

			EditorGUILayout.Space();

			using (new GUILayout.VerticalScope(EditorStyles.helpBox)) 
			{
				if (GUILayout.Button(LabelAndTooltip("Update Terrain Data For Objects", "Send updated data from Terrain to Objects integrated to Terrain."), styleButtonBold))
				{
					InTerra_Data.UpdateTerrainData();
				}
			}
			EditorGUILayout.Space();

			if (targetMat.shader.name != InTerra_Data.HDRPTerrainShaderName)
			{

				materialEditor.RenderQueueField();
			}

			else
			{								
				using (new GUILayout.VerticalScope())
				{
					//========================= ENABLE DECALS ===========================
					bool decals = !targetMat.IsKeywordEnabled("_DISABLE_DECALS");
					EditorGUI.BeginChangeCheck();
					decals = EditorGUILayout.Toggle(LabelAndTooltip("Receive Decals", "Enable to allow Materials to receive decals."), decals);
					if (EditorGUI.EndChangeCheck())
					{
						materialEditor.RegisterPropertyChangeUndo("InTerra Enable Receive Decals");
						SetKeyword(targetMat, !decals, "_DISABLE_DECALS");
					}
				
					//========================= PER PIXEL NORMAL ===========================
					bool perPixelNormal = targetMat.IsKeywordEnabled("_TERRAIN_INSTANCED_PERPIXEL_NORMAL");
					EditorGUI.BeginChangeCheck();
					perPixelNormal = EditorGUILayout.Toggle(LabelAndTooltip("Enable Per-pixel Normal", "Enable per-pixel normal when the terrain uses instanced rendering."), perPixelNormal);
					if (EditorGUI.EndChangeCheck())
					{
						materialEditor.RegisterPropertyChangeUndo("InTerra Enable Per-pixel Normal");
						SetKeyword(targetMat, perPixelNormal, "_TERRAIN_INSTANCED_PERPIXEL_NORMAL");
					}
				}
			}

			materialEditor.EnableInstancingField();


			void HeightmapBlending(string name, string tooltip)
			{
				
				bool heightBlending = targetMat.IsKeywordEnabled("_TERRAIN_BLEND_HEIGHT");
				EditorGUI.BeginChangeCheck();
				EditorStyles.label.fontStyle = FontStyle.Bold;
				heightBlending = EditorGUILayout.ToggleLeft(LabelAndTooltip(name, tooltip), heightBlending, GUILayout.MinWidth(120));
				
				EditorStyles.label.fontStyle = FontStyle.Normal;

				if (EditorGUI.EndChangeCheck())
				{
					materialEditor.RegisterPropertyChangeUndo("InTerra HeightBlend");
					SetKeyword(targetMat, heightBlending, "_TERRAIN_BLEND_HEIGHT");
					InTerra_Data.UpdateTerrainData();
				}

				if (heightBlending)
				{
					using (new GUILayout.VerticalScope(EditorStyles.helpBox))
					{
						PropertyLine("_HeightTransition", "Sharpness", "Sharpness of the textures transitions");

						if (targetMat.IsKeywordEnabled("_TERRAIN_DISTANCEBLEND"))
						{
							PropertyLine("_Distance_HeightTransition", "Distant Sharpness", "Sharpness of the textures transitions for distant area setted in Hide Tiling.");
						}
					}
				}
				else
                {
					if (targetMat.shader.name == InTerra_Data.DiffuseTerrainShaderName) EditorGUILayout.LabelField("(Heightmap will be taken from Diffuse Alpha cahnnel)", styleMini);
				}

				if (targetMat.GetFloat("_NumLayersCount") > 4)
				{
					EditorGUILayout.HelpBox("The Heightmap blending will not be applied on Terrain Base Map if there are more than four Layers.", MessageType.Info);
				}
			}

			void PropertyLine(string property, string label, string tooltip = null)
			{
				materialEditor.ShaderProperty(FindProperty(property, properties), new GUIContent() { text = label, tooltip = tooltip });
			}

			GUIContent LabelAndTooltip(string label, string tooltip)
			{
				return new GUIContent() { text = label, tooltip = tooltip };
			}

		
		}

		static void SetKeyword(Material targetMat, bool set, string keyword)
		{
			if (set)
			{
				targetMat.EnableKeyword(keyword);
			}
			else
			{
				targetMat.DisableKeyword(keyword);
			}
		}

		static void MoveLayerToFirstPosition(Terrain terrain, int indexToFirst)
		{
			float[,,] alphaMaps = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);

			for (int y = 0; y < terrain.terrainData.alphamapHeight; y++)
			{
				for (int x = 0; x < terrain.terrainData.alphamapWidth; x++)
				{
					float a0 = alphaMaps[x, y, 0];
					float a1 = alphaMaps[x, y, indexToFirst];

					alphaMaps[x, y, 0] = a1;
					alphaMaps[x, y, indexToFirst] = a0;

				}
			}
			TerrainLayer[] origLayers = terrain.terrainData.terrainLayers;
			TerrainLayer[] movedLayers = terrain.terrainData.terrainLayers;

			TerrainLayer firstLayer = terrain.terrainData.terrainLayers[0];
			TerrainLayer movingLayer = terrain.terrainData.terrainLayers[indexToFirst];

			movedLayers[0] = movingLayer;
			movedLayers[indexToFirst] = firstLayer;

			terrain.terrainData.SetTerrainLayersRegisterUndo(origLayers, "InTerra Move Terrain Layer");			
			terrain.terrainData.terrainLayers = movedLayers;
			
			Undo.RegisterCompleteObjectUndo(terrain.terrainData.alphamapTextures, "InTerra Move Terrain Layer");
			terrain.terrainData.SetAlphamaps(0, 0, alphaMaps);
		}
	}
}
