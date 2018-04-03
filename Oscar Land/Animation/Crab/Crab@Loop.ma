//Maya ASCII 2016 scene
//Name: Crab@Loop.ma
//Last modified: Tue, Apr 03, 2018 10:02:28 PM
//Codeset: 1252
file -rdi 1 -ns "Master_Crab" -rfn "Master_CrabRN" -op "v=0;p=17;f=0" -typ "mayaAscii"
		 "E:/Desktop/IslandGame/trunk/Oscar Land/Character/Master_Crab.ma";
file -r -ns "Master_Crab" -dr 1 -rfn "Master_CrabRN" -op "v=0;p=17;f=0" -typ "mayaAscii"
		 "E:/Desktop/IslandGame/trunk/Oscar Land/Character/Master_Crab.ma";
requires maya "2016";
requires "stereoCamera" "10.0";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2016";
fileInfo "version" "2016";
fileInfo "cutIdentifier" "201603180400-990260";
fileInfo "osv" "Microsoft Windows 8 , 64-bit  (Build 9200)\n";
createNode transform -s -n "persp";
	rename -uid "BE44FC7B-4C25-1CA2-84FD-AEBC3BA6F32F";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 93.573308010910651 166.06991052554534 223.40865549847126 ;
	setAttr ".r" -type "double3" -32.705266384271468 -1779.3999999999658 0 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "A486286D-4900-3227-D9E2-B4979E1D4594";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 299.99642827688251;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	rename -uid "ACFF0886-4447-2CA7-BDFE-2B8D64A8DF8A";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 100.10000000000001 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "A08284C9-4CDE-148A-E999-F2B30AFA7851";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.10000000000001;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	rename -uid "25B9AC88-4190-63BF-165C-9E988DAEF7A7";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 100.10000000000001 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "E23ACF22-40ED-C58B-4F0B-21A05D9B8FDD";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.10000000000001;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	rename -uid "52764132-47AF-03E5-CDA3-4B979869993B";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 100.10000000000001 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "E4D4D84A-47A4-8FFD-EF61-71923BB4D759";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.10000000000001;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "6D1E7C77-424A-BE56-7756-73BE5B0E6B26";
	setAttr -s 3 ".lnk";
	setAttr -s 3 ".slnk";
createNode displayLayerManager -n "layerManager";
	rename -uid "2874D323-4166-C927-E1A0-10BF139D4CAE";
createNode displayLayer -n "defaultLayer";
	rename -uid "E727DE18-451E-F4C0-8591-F3B163E25E34";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "6FF0BF5B-4116-09BB-B72C-37B425D906FD";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "5470C1A5-48B3-7DE1-C8A4-B4B213D26D14";
	setAttr ".g" yes;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "6088E5CF-464C-CCF0-66B6-6EBA4297DFBC";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n"
		+ "                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n"
		+ "                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n"
		+ "                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n"
		+ "            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n"
		+ "            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n"
		+ "        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n"
		+ "                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n"
		+ "                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n"
		+ "                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n"
		+ "            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n"
		+ "            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n"
		+ "            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n"
		+ "                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n"
		+ "                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n"
		+ "                -width 1\n                -height 1\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n"
		+ "            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n"
		+ "            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n"
		+ "            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n"
		+ "                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 1\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n"
		+ "                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n"
		+ "                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 2536\n                -height 1644\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n"
		+ "            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n"
		+ "            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 2536\n            -height 1644\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n"
		+ "            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -docTag \"isolOutln_fromSeln\" \n                -showShapes 0\n                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n"
		+ "                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n"
		+ "                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -docTag \"isolOutln_fromSeln\" \n            -showShapes 0\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n"
		+ "            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n"
		+ "            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n"
		+ "                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n"
		+ "                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -clipTime \"on\" \n                -stackedCurves 0\n"
		+ "                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -clipTime \"on\" \n                -stackedCurves 0\n                -stackedCurvesMin -1\n"
		+ "                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n"
		+ "                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n"
		+ "                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n"
		+ "                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n"
		+ "                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n"
		+ "                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n"
		+ "                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n"
		+ "                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n"
		+ "                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"profilerPanel\" -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"Stereo\" (localizedPanelLabel(\"Stereo\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"Stereo\" -l (localizedPanelLabel(\"Stereo\")) -mbv $menusOkayInPanels `;\nstring $editorName = ($panelName+\"Editor\");\n            stereoCameraView -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n"
		+ "                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n"
		+ "                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n"
		+ "                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 0\n                -height 0\n                -sceneRenderFilter 0\n                -displayMode \"centerEye\" \n                -viewColor 0 0 0 1 \n                -useCustomBackground 1\n                $editorName;\n            stereoCameraView -e -viewSelected 0 $editorName;\n            stereoCameraView -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Stereo\")) -mbv $menusOkayInPanels  $panelName;\nstring $editorName = ($panelName+\"Editor\");\n            stereoCameraView -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n"
		+ "                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n"
		+ "                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n"
		+ "                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 0\n                -height 0\n                -sceneRenderFilter 0\n                -displayMode \"centerEye\" \n                -viewColor 0 0 0 1 \n                -useCustomBackground 1\n"
		+ "                $editorName;\n            stereoCameraView -e -viewSelected 0 $editorName;\n            stereoCameraView -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 1\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 1\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n"
		+ "                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"vertical2\\\" -ps 1 20 100 -ps 2 80 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Outliner\")) \n\t\t\t\t\t\"outlinerPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -docTag \\\"isolOutln_fromSeln\\\" \\n    -showShapes 0\\n    -showReferenceNodes 1\\n    -showReferenceMembers 1\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    -mapMotionTrails 0\\n    -ignoreHiddenAttribute 0\\n    -ignoreOutlinerColor 0\\n    $editorName\"\n"
		+ "\t\t\t\t\t\"outlinerPanel -edit -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -docTag \\\"isolOutln_fromSeln\\\" \\n    -showShapes 0\\n    -showReferenceNodes 1\\n    -showReferenceMembers 1\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    -mapMotionTrails 0\\n    -ignoreHiddenAttribute 0\\n    -ignoreOutlinerColor 0\\n    $editorName\"\n"
		+ "\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 2536\\n    -height 1644\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 2536\\n    -height 1644\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 25 -size 100 -divisions 1 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "39325D23-4C98-5E06-488A-A8B0FE666029";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 240 ";
	setAttr ".st" 6;
createNode reference -n "Master_CrabRN";
	rename -uid "78099301-443C-C2E9-46C3-7A824A67767D";
	setAttr -s 48 ".phl";
	setAttr ".phl[1]" 0;
	setAttr ".phl[2]" 0;
	setAttr ".phl[3]" 0;
	setAttr ".phl[4]" 0;
	setAttr ".phl[5]" 0;
	setAttr ".phl[6]" 0;
	setAttr ".phl[7]" 0;
	setAttr ".phl[8]" 0;
	setAttr ".phl[9]" 0;
	setAttr ".phl[10]" 0;
	setAttr ".phl[11]" 0;
	setAttr ".phl[12]" 0;
	setAttr ".phl[13]" 0;
	setAttr ".phl[14]" 0;
	setAttr ".phl[15]" 0;
	setAttr ".phl[16]" 0;
	setAttr ".phl[17]" 0;
	setAttr ".phl[18]" 0;
	setAttr ".phl[19]" 0;
	setAttr ".phl[20]" 0;
	setAttr ".phl[21]" 0;
	setAttr ".phl[22]" 0;
	setAttr ".phl[23]" 0;
	setAttr ".phl[24]" 0;
	setAttr ".phl[25]" 0;
	setAttr ".phl[26]" 0;
	setAttr ".phl[27]" 0;
	setAttr ".phl[28]" 0;
	setAttr ".phl[29]" 0;
	setAttr ".phl[30]" 0;
	setAttr ".phl[31]" 0;
	setAttr ".phl[32]" 0;
	setAttr ".phl[33]" 0;
	setAttr ".phl[34]" 0;
	setAttr ".phl[35]" 0;
	setAttr ".phl[36]" 0;
	setAttr ".phl[37]" 0;
	setAttr ".phl[38]" 0;
	setAttr ".phl[39]" 0;
	setAttr ".phl[40]" 0;
	setAttr ".phl[41]" 0;
	setAttr ".phl[42]" 0;
	setAttr ".phl[43]" 0;
	setAttr ".phl[44]" 0;
	setAttr ".phl[45]" 0;
	setAttr ".phl[46]" 0;
	setAttr ".phl[47]" 0;
	setAttr ".phl[48]" 0;
	setAttr ".ed" -type "dataReferenceEdits" 
		"Master_CrabRN"
		"Master_CrabRN" 0
		"Master_CrabRN" 92
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"translate" " -type \"double3\" 0 -2.2781789888116544 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"translateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"rotate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"rotateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"rotateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"rotateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim" 
		"segmentScaleCompensate" " 1"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim" 
		"rotate" " -type \"double3\" 0 0 13.774849105579866"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim" 
		"rotateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim" 
		"rotateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim" 
		"rotateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim" 
		"segmentScaleCompensate" " 1"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim" 
		"rotate" " -type \"double3\" 0 0 13.774849105579866"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim" 
		"rotateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim" 
		"rotateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim" 
		"rotateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim" 
		"segmentScaleCompensate" " 1"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim" 
		"translateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim" 
		"translateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"translateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"translateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"translateZ" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"rotate" " -type \"double3\" 0 0 0"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"rotateX" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"rotateY" " -av"
		2 "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim" 
		"rotateZ" " -av"
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.translateX" 
		"Master_CrabRN.placeHolderList[1]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.translateY" 
		"Master_CrabRN.placeHolderList[2]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.translateZ" 
		"Master_CrabRN.placeHolderList[3]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.rotateX" 
		"Master_CrabRN.placeHolderList[4]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.rotateY" 
		"Master_CrabRN.placeHolderList[5]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[6]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim.rotateX" 
		"Master_CrabRN.placeHolderList[7]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim.rotateY" 
		"Master_CrabRN.placeHolderList[8]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_l_pincerOffset_grp|Master_Crab:fk_l_pincer_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[9]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim.rotateX" 
		"Master_CrabRN.placeHolderList[10]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim.rotateY" 
		"Master_CrabRN.placeHolderList[11]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:fk_c_bodyOffset_grp|Master_Crab:fk_c_body_anim|Master_Crab:fk_r_pincerOffset_grp|Master_Crab:fk_r_pincer_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[12]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.translateX" 
		"Master_CrabRN.placeHolderList[13]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.translateZ" 
		"Master_CrabRN.placeHolderList[14]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.translateY" 
		"Master_CrabRN.placeHolderList[15]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.rotateX" 
		"Master_CrabRN.placeHolderList[16]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.rotateY" 
		"Master_CrabRN.placeHolderList[17]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legFrontOffset_grp|Master_Crab:ik_l_legFront_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[18]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.translateX" 
		"Master_CrabRN.placeHolderList[19]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.translateY" 
		"Master_CrabRN.placeHolderList[20]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.translateZ" 
		"Master_CrabRN.placeHolderList[21]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.rotateX" 
		"Master_CrabRN.placeHolderList[22]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.rotateY" 
		"Master_CrabRN.placeHolderList[23]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legMidOffset_grp|Master_Crab:ik_l_legMid_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[24]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.translateX" 
		"Master_CrabRN.placeHolderList[25]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.translateY" 
		"Master_CrabRN.placeHolderList[26]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.translateZ" 
		"Master_CrabRN.placeHolderList[27]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.rotateX" 
		"Master_CrabRN.placeHolderList[28]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.rotateY" 
		"Master_CrabRN.placeHolderList[29]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_l_legRearOffset_grp|Master_Crab:ik_l_legRear_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[30]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.translateX" 
		"Master_CrabRN.placeHolderList[31]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.translateY" 
		"Master_CrabRN.placeHolderList[32]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.translateZ" 
		"Master_CrabRN.placeHolderList[33]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.rotateX" 
		"Master_CrabRN.placeHolderList[34]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.rotateY" 
		"Master_CrabRN.placeHolderList[35]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legFrontOffset_grp|Master_Crab:ik_r_legFront_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[36]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.translateX" 
		"Master_CrabRN.placeHolderList[37]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.translateY" 
		"Master_CrabRN.placeHolderList[38]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.translateZ" 
		"Master_CrabRN.placeHolderList[39]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.rotateX" 
		"Master_CrabRN.placeHolderList[40]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.rotateY" 
		"Master_CrabRN.placeHolderList[41]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legMidOffset_grp|Master_Crab:ik_r_legMid_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[42]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.translateX" 
		"Master_CrabRN.placeHolderList[43]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.translateY" 
		"Master_CrabRN.placeHolderList[44]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.translateZ" 
		"Master_CrabRN.placeHolderList[45]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.rotateX" 
		"Master_CrabRN.placeHolderList[46]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.rotateY" 
		"Master_CrabRN.placeHolderList[47]" ""
		5 4 "Master_CrabRN" "|Master_Crab:Crab|Master_Crab:Controls|Master_Crab:fk_c_moveAll_anim|Master_Crab:ik_r_legRearOffset_grp|Master_Crab:ik_r_legRear_anim.rotateZ" 
		"Master_CrabRN.placeHolderList[48]" "";
	setAttr ".ptag" -type "string" "";
lockNode -l 1 ;
createNode animCurveTL -n "fk_c_body_anim_translateX";
	rename -uid "3447593B-4115-8F78-0783-C7838EED2076";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 10 ".ktv[0:9]"  1 0 12 9.8296100884004627 20 15.920022049891273
		 32 15.920022049891273 48 15.920022049891273 60 15.920022049891273 80 0 92 0 102 0
		 120 0;
createNode animCurveTL -n "fk_c_body_anim_translateY";
	rename -uid "7AB05C0D-4F63-2D53-B2ED-92A886A1AECE";
	setAttr ".tan" 3;
	setAttr ".wgt" no;
	setAttr -s 11 ".ktv[0:10]"  1 -2.2781789888116544 12 1.133031723746134
		 24 -2.2150465544423685 36 1.2015529390593089 48 -2.2150465544423685 60 1.2154491851130294
		 69 -2.2150465544423685 80 1.2831592641602354 92 -2.2150465544423685 105 1.2909113909034202
		 120 -2.2511222312248176;
createNode animCurveTL -n "fk_c_body_anim_translateZ";
	rename -uid "0E4BF09B-4A26-3C58-0852-63A91B46F92C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 10 ".ktv[0:9]"  1 0 12 0 20 0 32 0 48 0 60 0 80 0 92 0 102 0
		 120 0;
createNode animCurveTA -n "fk_c_body_anim_rotateX";
	rename -uid "17780B35-4507-FB9D-7986-9AB9D87CCA80";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 12 ".ktv[0:11]"  1 0 12 0.1766356817833418 20 0 24 -0.48305657940854629
		 32 -1.7408399891388675 46 -2.8769030854324842 48 -2.7175409835272624 60 0 80 0 92 0
		 102 2.1783460775202772 120 0;
createNode animCurveTA -n "fk_c_body_anim_rotateY";
	rename -uid "2EB1D88F-4E8E-E7B5-E9B2-4FA8414797DE";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 11 ".ktv[0:10]"  1 0 12 1.6693349060115406 20 2.7187412631492904
		 24 2.6928342360536583 32 2.7187412631492904 48 1.0700585729641592 60 0 80 0 92 0
		 102 1.1128929040093474 120 0;
createNode animCurveTA -n "fk_c_body_anim_rotateZ";
	rename -uid "D8E814D1-46F8-FEE2-8446-559E12C2362A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 13 ".ktv[0:12]"  1 0 12 6.0409737706138689 20 0 24 -3.7697859632060839
		 32 0 48 0 60 0 68 -6.1469118440707433 80 0 86 4.8896172133542564 92 0 102 1.3027478427774914
		 120 0;
createNode animCurveTL -n "ik_r_legFront_anim_translateX";
	rename -uid "665B2B00-4029-FA26-D2B2-9BB1C84BDF76";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 8 16.767864362676931 16 16.767864362676931
		 63 16.767864362676931 72 16.767864362676931 79 0 120 0;
createNode animCurveTL -n "ik_r_legFront_anim_translateY";
	rename -uid "23545B0B-4ED8-EF4A-6827-848B538DD161";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 8 0 63 0 75 0 79 0 120 0;
createNode animCurveTL -n "ik_r_legFront_anim_translateZ";
	rename -uid "2D686A3D-4BC0-2CE1-0517-90AC5806FB1C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 8 1.4761836614653987 16 1.4761836614653987
		 63 1.4761836614653987 72 1.4761836614653987 79 0 120 0;
createNode animCurveTL -n "ik_r_legMid_anim_translateX";
	rename -uid "2992DEDA-4B61-AF3B-6DEA-80BF28DFA090";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  3 0 10 16.767864362676931 18 16.767864362676931
		 61 16.767864362676931 70 16.767864362676931 77 0 120 0;
createNode animCurveTL -n "ik_r_legMid_anim_translateY";
	rename -uid "1551B755-4B63-F4BB-B337-44A881A9F54E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  3 0 10 0 61 0 73 0 77 0 120 0;
createNode animCurveTL -n "ik_r_legMid_anim_translateZ";
	rename -uid "A2034807-4317-7D72-19BB-DD98B3C6AE64";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  3 0 10 1.4761836614653987 18 1.4761836614653987
		 61 1.4761836614653987 70 1.4761836614653987 77 0 120 0;
createNode animCurveTL -n "ik_r_legRear_anim_translateX";
	rename -uid "09377A6F-4B95-DCCF-1347-ADB124A9B8C3";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  5 0 12 16.767864362676931 20 16.767864362676931
		 60 16.767864362676931 69 16.767864362676931 76 0 120 0;
createNode animCurveTL -n "ik_r_legRear_anim_translateY";
	rename -uid "EB4BB499-47BD-3066-71FB-FE8ECD6C0E3E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  5 0 12 0 60 0 72 0 76 0 120 0;
createNode animCurveTL -n "ik_r_legRear_anim_translateZ";
	rename -uid "B0C55129-4DA6-2DD2-7EB0-CBAE347E8474";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  5 0 12 1.4761836614653987 20 1.4761836614653987
		 60 1.4761836614653987 69 1.4761836614653987 76 0 120 0;
createNode animCurveTA -n "ik_r_legRear_anim_rotateX";
	rename -uid "94B87142-463F-6174-AD4D-0B838D0CEC04";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  5 0 12 0 60 0 72 0 76 0 120 0;
createNode animCurveTA -n "ik_r_legRear_anim_rotateY";
	rename -uid "6766E822-411B-668F-CA07-B0BCD5AA1E81";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  5 0 12 0 60 0 72 0 76 0 120 0;
createNode animCurveTA -n "ik_r_legRear_anim_rotateZ";
	rename -uid "84F174CD-47DD-F477-6D94-2BA93C6D7C9E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  5 0 12 0 60 0 72 0 76 0 120 0;
createNode animCurveTA -n "ik_r_legMid_anim_rotateX";
	rename -uid "0467C19F-4A02-368E-C0AD-2685751CA135";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  3 0 10 0 61 0 73 0 77 0 120 0;
createNode animCurveTA -n "ik_r_legMid_anim_rotateY";
	rename -uid "2461C6E7-48FC-BE12-F472-5D9C584877D4";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  3 0 10 0 61 0 73 0 77 0 120 0;
createNode animCurveTA -n "ik_r_legMid_anim_rotateZ";
	rename -uid "B347748F-49D9-33AF-25AE-D4A62CB2211F";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  3 0 10 0 61 0 73 0 77 0 120 0;
createNode animCurveTA -n "ik_r_legFront_anim_rotateX";
	rename -uid "9C5BA366-4A11-036E-1C4E-1A9682DBCEAA";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 8 0 63 0 75 0 79 0 120 0;
createNode animCurveTA -n "ik_r_legFront_anim_rotateY";
	rename -uid "8C4F434D-4725-5723-9881-16A318666307";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 8 0 63 0 75 0 79 0 120 0;
createNode animCurveTA -n "ik_r_legFront_anim_rotateZ";
	rename -uid "6A72D509-44F8-2053-7176-77833B553D99";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 8 0 63 0 75 0 79 0 120 0;
createNode animCurveTL -n "ik_l_legFront_anim_translateX";
	rename -uid "2FE1E539-41BA-FC84-F39C-A38C7DB9A2DF";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  2 0 13 0 17 13.530670375280323 60 13.530670375280323
		 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legFront_anim_translateY";
	rename -uid "21A9595C-4464-69C8-B9A0-F88539229901";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  2 0 13 0 60 0 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legFront_anim_translateZ";
	rename -uid "C52BB8EF-44B0-C116-4EB4-99AC082A705A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  2 0 13 0 17 0.8550895790909685 60 0.8550895790909685
		 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legMid_anim_translateX";
	rename -uid "52DA49B2-43DA-D95E-3E74-7FBAF84FBCB0";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 12 0 16 13.530670375280323 60 13.530670375280323
		 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legMid_anim_translateY";
	rename -uid "FAF3D2D0-4BF0-FFCB-98E2-D2A62118928D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legMid_anim_translateZ";
	rename -uid "3939A6FE-4598-B76A-90A9-F9B753C7838D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 12 0 16 0.8550895790909685 60 0.8550895790909685
		 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legRear_anim_translateX";
	rename -uid "51122431-4EB9-2627-F5EC-BCAD7E7C4A6A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 12 0 16 13.530670375280323 60 13.530670375280323
		 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legRear_anim_translateY";
	rename -uid "39230DFE-46AC-10EC-D9CC-6DB289869924";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTL -n "ik_l_legRear_anim_translateZ";
	rename -uid "F21F1F59-4128-DF36-7B0D-6695EAA2B52E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 12 0 16 0.8550895790909685 60 0.8550895790909685
		 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legFront_anim_rotateX";
	rename -uid "72FA18E1-4A03-35CC-6EE3-F894455815FF";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  2 0 13 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legFront_anim_rotateY";
	rename -uid "655C3FC8-41EE-F67C-9BE8-ECB91DCED146";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  2 0 13 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legFront_anim_rotateZ";
	rename -uid "18546ADB-4246-332D-FE81-E8909553C8F9";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  2 0 13 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legRear_anim_rotateX";
	rename -uid "A76F1BAF-4B10-D321-C906-12888F1AB627";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legRear_anim_rotateY";
	rename -uid "F4E393F7-4EDF-CF05-D124-1C9BC2F6A8AD";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legRear_anim_rotateZ";
	rename -uid "97D0F892-4F4F-E27A-CC7F-199AED0D37DC";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legMid_anim_rotateX";
	rename -uid "E516A0CF-4770-9894-0C95-60B18F6CB93B";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legMid_anim_rotateY";
	rename -uid "4BB4AE32-49BE-EB86-9DF7-E6BF8F56FD3F";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "ik_l_legMid_anim_rotateZ";
	rename -uid "9F36AE9D-4B2E-957B-A580-46AABE895D0E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 0 12 0 60 0 64 0 76 0 120 0;
createNode animCurveTA -n "fk_r_pincer_anim_rotateZ";
	rename -uid "82C87DCE-422B-0998-693B-1E906FEA5B53";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  26 13.774849105579866 91.68 13.774849105579866
		 97.16 12.090741372042942 100.84 13.774849105579866 104.48 12.090741372042942 108.16 12.090741372042942
		 120 13.774849105579866;
createNode animCurveTA -n "fk_r_pincer_anim_rotateX";
	rename -uid "0546F202-421D-AB12-EABA-0585684D7C3C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  91.68 0 97.16 -0.94774165760737872 100.84 0
		 104.48 -0.94774165760737872 108.16 -0.94774165760737872 120 0;
createNode animCurveTA -n "fk_r_pincer_anim_rotateY";
	rename -uid "7110605A-4B4A-213B-2B1C-3FAE71244BFF";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  91.68 0 97.16 7.9349829384646533 100.84 0
		 104.48 7.9349829384646533 108.16 7.9349829384646533 120 0;
createNode animCurveTA -n "fk_l_pincer_anim_rotateX";
	rename -uid "94D4AE97-4B37-D16D-15D9-5F98773EE7C0";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  91.68 0 100.84 0 104.48 0 108.16 0 120 0;
createNode animCurveTA -n "fk_l_pincer_anim_rotateY";
	rename -uid "B14FB753-48BF-3205-F8FA-51A095C459D8";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  91.68 0 97.16 -4.2383329088241792 100.84 0
		 104.48 -4.2383329088241792 108.16 -4.2383329088241792 120 0;
createNode animCurveTA -n "fk_l_pincer_anim_rotateZ";
	rename -uid "66C11D79-43E2-8DFD-DBC1-EEAA0BD7798B";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 5 ".ktv[0:4]"  91.68 13.774849105579866 100.84 13.774849105579866
		 104.48 13.774849105579866 108.16 13.774849105579866 120 13.774849105579866;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 3 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
	setAttr -s 3 ".u";
select -ne :defaultRenderingList1;
	setAttr -s 2 ".r";
select -ne :defaultTextureList1;
	setAttr -s 2 ".tx";
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
connectAttr "fk_c_body_anim_translateX.o" "Master_CrabRN.phl[1]";
connectAttr "fk_c_body_anim_translateY.o" "Master_CrabRN.phl[2]";
connectAttr "fk_c_body_anim_translateZ.o" "Master_CrabRN.phl[3]";
connectAttr "fk_c_body_anim_rotateX.o" "Master_CrabRN.phl[4]";
connectAttr "fk_c_body_anim_rotateY.o" "Master_CrabRN.phl[5]";
connectAttr "fk_c_body_anim_rotateZ.o" "Master_CrabRN.phl[6]";
connectAttr "fk_l_pincer_anim_rotateX.o" "Master_CrabRN.phl[7]";
connectAttr "fk_l_pincer_anim_rotateY.o" "Master_CrabRN.phl[8]";
connectAttr "fk_l_pincer_anim_rotateZ.o" "Master_CrabRN.phl[9]";
connectAttr "fk_r_pincer_anim_rotateX.o" "Master_CrabRN.phl[10]";
connectAttr "fk_r_pincer_anim_rotateY.o" "Master_CrabRN.phl[11]";
connectAttr "fk_r_pincer_anim_rotateZ.o" "Master_CrabRN.phl[12]";
connectAttr "ik_l_legFront_anim_translateX.o" "Master_CrabRN.phl[13]";
connectAttr "ik_l_legFront_anim_translateZ.o" "Master_CrabRN.phl[14]";
connectAttr "ik_l_legFront_anim_translateY.o" "Master_CrabRN.phl[15]";
connectAttr "ik_l_legFront_anim_rotateX.o" "Master_CrabRN.phl[16]";
connectAttr "ik_l_legFront_anim_rotateY.o" "Master_CrabRN.phl[17]";
connectAttr "ik_l_legFront_anim_rotateZ.o" "Master_CrabRN.phl[18]";
connectAttr "ik_l_legMid_anim_translateX.o" "Master_CrabRN.phl[19]";
connectAttr "ik_l_legMid_anim_translateY.o" "Master_CrabRN.phl[20]";
connectAttr "ik_l_legMid_anim_translateZ.o" "Master_CrabRN.phl[21]";
connectAttr "ik_l_legMid_anim_rotateX.o" "Master_CrabRN.phl[22]";
connectAttr "ik_l_legMid_anim_rotateY.o" "Master_CrabRN.phl[23]";
connectAttr "ik_l_legMid_anim_rotateZ.o" "Master_CrabRN.phl[24]";
connectAttr "ik_l_legRear_anim_translateX.o" "Master_CrabRN.phl[25]";
connectAttr "ik_l_legRear_anim_translateY.o" "Master_CrabRN.phl[26]";
connectAttr "ik_l_legRear_anim_translateZ.o" "Master_CrabRN.phl[27]";
connectAttr "ik_l_legRear_anim_rotateX.o" "Master_CrabRN.phl[28]";
connectAttr "ik_l_legRear_anim_rotateY.o" "Master_CrabRN.phl[29]";
connectAttr "ik_l_legRear_anim_rotateZ.o" "Master_CrabRN.phl[30]";
connectAttr "ik_r_legFront_anim_translateX.o" "Master_CrabRN.phl[31]";
connectAttr "ik_r_legFront_anim_translateY.o" "Master_CrabRN.phl[32]";
connectAttr "ik_r_legFront_anim_translateZ.o" "Master_CrabRN.phl[33]";
connectAttr "ik_r_legFront_anim_rotateX.o" "Master_CrabRN.phl[34]";
connectAttr "ik_r_legFront_anim_rotateY.o" "Master_CrabRN.phl[35]";
connectAttr "ik_r_legFront_anim_rotateZ.o" "Master_CrabRN.phl[36]";
connectAttr "ik_r_legMid_anim_translateX.o" "Master_CrabRN.phl[37]";
connectAttr "ik_r_legMid_anim_translateY.o" "Master_CrabRN.phl[38]";
connectAttr "ik_r_legMid_anim_translateZ.o" "Master_CrabRN.phl[39]";
connectAttr "ik_r_legMid_anim_rotateX.o" "Master_CrabRN.phl[40]";
connectAttr "ik_r_legMid_anim_rotateY.o" "Master_CrabRN.phl[41]";
connectAttr "ik_r_legMid_anim_rotateZ.o" "Master_CrabRN.phl[42]";
connectAttr "ik_r_legRear_anim_translateX.o" "Master_CrabRN.phl[43]";
connectAttr "ik_r_legRear_anim_translateY.o" "Master_CrabRN.phl[44]";
connectAttr "ik_r_legRear_anim_translateZ.o" "Master_CrabRN.phl[45]";
connectAttr "ik_r_legRear_anim_rotateX.o" "Master_CrabRN.phl[46]";
connectAttr "ik_r_legRear_anim_rotateY.o" "Master_CrabRN.phl[47]";
connectAttr "ik_r_legRear_anim_rotateZ.o" "Master_CrabRN.phl[48]";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
// End of Crab@Loop.ma
