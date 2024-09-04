#define NUM_TEX_COORD_INTERPOLATORS 1
#define NUM_MATERIAL_TEXCOORDS_VERTEX 1
#define NUM_CUSTOM_VERTEX_INTERPOLATORS 0

struct Input
{
	//float3 Normal;
	float2 uv_MainTex : TEXCOORD0;
	float2 uv2_Material_Texture2D_0 : TEXCOORD1;
	float4 color : COLOR;
	float4 tangent;
	//float4 normal;
	float3 viewDir;
	float4 screenPos;
	float3 worldPos;
	//float3 worldNormal;
	float3 normal2;
};
struct SurfaceOutputStandard
{
	float3 Albedo;		// base (diffuse or specular) color
	float3 Normal;		// tangent space normal, if written
	half3 Emission;
	half Metallic;		// 0=non-metal, 1=metal
	// Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
	// Everywhere in the code you meet smoothness it is perceptual smoothness
	half Smoothness;	// 0=rough, 1=smooth
	half Occlusion;		// occlusion (default 1)
	float Alpha;		// alpha for transparencies
};

//#define HDRP 1
#define URP 1
#define UE5
//#define HAS_CUSTOMIZED_UVS 1
#define MATERIAL_TANGENTSPACENORMAL 1
//struct Material
//{
	//samplers start
SAMPLER( SamplerState_Linear_Repeat );
SAMPLER( SamplerState_Linear_Clamp );
TEXTURE2D(       Material_Texture2D_0 );
SAMPLER(  samplerMaterial_Texture2D_0 );
float4 Material_Texture2D_0_TexelSize;
float4 Material_Texture2D_0_ST;
TEXTURE2D(       Material_Texture2D_1 );
SAMPLER(  samplerMaterial_Texture2D_1 );
float4 Material_Texture2D_1_TexelSize;
float4 Material_Texture2D_1_ST;
TEXTURE2D(       Material_Texture2D_2 );
SAMPLER(  samplerMaterial_Texture2D_2 );
float4 Material_Texture2D_2_TexelSize;
float4 Material_Texture2D_2_ST;
TEXTURE2D(       Material_Texture2D_3 );
SAMPLER(  samplerMaterial_Texture2D_3 );
float4 Material_Texture2D_3_TexelSize;
float4 Material_Texture2D_3_ST;
TEXTURE2D(       Material_Texture2D_4 );
SAMPLER(  samplerMaterial_Texture2D_4 );
float4 Material_Texture2D_4_TexelSize;
float4 Material_Texture2D_4_ST;
TEXTURE2D(       Material_Texture2D_5 );
SAMPLER(  samplerMaterial_Texture2D_5 );
float4 Material_Texture2D_5_TexelSize;
float4 Material_Texture2D_5_ST;
TEXTURE2D(       Material_Texture2D_6 );
SAMPLER(  samplerMaterial_Texture2D_6 );
float4 Material_Texture2D_6_TexelSize;
float4 Material_Texture2D_6_ST;
TEXTURE2D(       Material_Texture2D_7 );
SAMPLER(  samplerMaterial_Texture2D_7 );
float4 Material_Texture2D_7_TexelSize;
float4 Material_Texture2D_7_ST;

//};

#ifdef UE5
	#define UE_LWC_RENDER_TILE_SIZE			2097152.0
	#define UE_LWC_RENDER_TILE_SIZE_SQRT	1448.15466
	#define UE_LWC_RENDER_TILE_SIZE_RSQRT	0.000690533954
	#define UE_LWC_RENDER_TILE_SIZE_RCP		4.76837158e-07
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_PI		0.673652053
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_2PI	0.673652053
	#define INVARIANT(X) X
	#define PI 					(3.1415926535897932)

	#include "LargeWorldCoordinates.hlsl"
#endif
struct MaterialStruct
{
	float4 PreshaderBuffer[7];
	float4 ScalarExpressions[1];
	float VTPackedPageTableUniform[2];
	float VTPackedUniform[1];
};
static SamplerState View_MaterialTextureBilinearWrapedSampler;
static SamplerState View_MaterialTextureBilinearClampedSampler;
struct ViewStruct
{
	float GameTime;
	float RealTime;
	float DeltaTime;
	float PrevFrameGameTime;
	float PrevFrameRealTime;
	float MaterialTextureMipBias;	
	float4 PrimitiveSceneData[ 40 ];
	float4 TemporalAAParams;
	float2 ViewRectMin;
	float4 ViewSizeAndInvSize;
	float MaterialTextureDerivativeMultiply;
	uint StateFrameIndexMod8;
	float FrameNumber;
	float2 FieldOfViewWideAngles;
	float4 RuntimeVirtualTextureMipLevel;
	float PreExposure;
	float4 BufferBilinearUVMinMax;
};
struct ResolvedViewStruct
{
	#ifdef UE5
		FLWCVector3 WorldCameraOrigin;
		FLWCVector3 PrevWorldCameraOrigin;
		FLWCVector3 PreViewTranslation;
		FLWCVector3 WorldViewOrigin;
	#else
		float3 WorldCameraOrigin;
		float3 PrevWorldCameraOrigin;
		float3 PreViewTranslation;
		float3 WorldViewOrigin;
	#endif
	float4 ScreenPositionScaleBias;
	float4x4 TranslatedWorldToView;
	float4x4 TranslatedWorldToCameraView;
	float4x4 TranslatedWorldToClip;
	float4x4 ViewToTranslatedWorld;
	float4x4 PrevViewToTranslatedWorld;
	float4x4 CameraViewToTranslatedWorld;
	float4 BufferBilinearUVMinMax;
	float4 XRPassthroughCameraUVs[ 2 ];
};
struct PrimitiveStruct
{
	float4x4 WorldToLocal;
	float4x4 LocalToWorld;
};

static ViewStruct View;
static ResolvedViewStruct ResolvedView;
static PrimitiveStruct Primitive;
uniform float4 View_BufferSizeAndInvSize;
uniform float4 LocalObjectBoundsMin;
uniform float4 LocalObjectBoundsMax;
static SamplerState Material_Wrap_WorldGroupSettings;
static SamplerState Material_Clamp_WorldGroupSettings;

#include "UnrealCommon.cginc"

static MaterialStruct Material;
void InitializeExpressions()
{
	Material.PreshaderBuffer[0] = float4(-0.002441,-0.002441,-0.002441,-2.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(0.020000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(3.000000,2.812035,2.512500,-0.179200);//(Unknown)
	Material.PreshaderBuffer[4] = float4(3.538731,-2.538731,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(0.030000,0.030000,0.030000,0.951503);//(Unknown)
	Material.PreshaderBuffer[6] = float4(0.134849,0.000000,1.000000,0.600000);//(Unknown)
}
MaterialFloat CustomExpression0(FMaterialPixelParameters Parameters,MaterialFloat2 p)
{
return Mod( ((uint)(p.x) + 2 * (uint)(p.y)) , 5 );
}
float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
	return MaterialFloat3(0.00000000,0.00000000,0.00000000);;
}
void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	//WorldAligned texturing & others use normals & stuff that think Z is up
	Parameters.TangentToWorld[0] = Parameters.TangentToWorld[0].xzy;
	Parameters.TangentToWorld[1] = Parameters.TangentToWorld[1].xzy;
	Parameters.TangentToWorld[2] = Parameters.TangentToWorld[2].xzy;

	float3 WorldNormalCopy = Parameters.WorldNormal;

	// Initial calculations (required for Normal)
	FWSVector3 Local0 = GetWorldPosition(Parameters);
	MaterialFloat3 Local1 = GetDistanceFieldGradientGlobal(DERIV_BASE_VALUE(Local0));
	MaterialFloat3 Local2 = normalize(Local1);
	MaterialFloat2 Local3 = Parameters.TexCoords[0].xy;
	MaterialFloat Local4 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local3), 10);
	MaterialFloat4 Local5 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local3),View.MaterialTextureMipBias));
	MaterialFloat Local6 = MaterialStoreTexSample(Parameters, Local5, 10);
	MaterialFloat Local7 = (Local5.rgb.b + 1.00000000);
	MaterialFloat3 Local8 = normalize(Parameters.TangentToWorld[2]);
	MaterialFloat3 Local9 = cross(Local8,normalize(MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb));
	MaterialFloat Local10 = dot(Local9,Local9);
	MaterialFloat3 Local11 = normalize(Local9);
	MaterialFloat4 Local12 = select((abs(Local10 - 0.00000100) > 0.00001000), select((Local10 >= 0.00000100), MaterialFloat4(Local11,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	FWSVector3 Local13 = GetWorldPosition_NoMaterialOffsets(Parameters);
	FWSVector3 Local14 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local13)), WSGetY(DERIV_BASE_VALUE(Local13)), WSGetZ(DERIV_BASE_VALUE(Local13)));
	FWSVector3 Local15 = WSMultiply(DERIV_BASE_VALUE(Local14), Material.PreshaderBuffer[0].xyz);
	FWSVector2 Local16 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local15)), WSGetY(DERIV_BASE_VALUE(Local15)));
	MaterialFloat2 Local17 = WSApplyAddressMode(DERIV_BASE_VALUE(Local16), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local18 = MaterialStoreTexCoordScale(Parameters, Local17, 5);
	MaterialFloat4 Local19 = UnpackNormalMap(Texture2DSample(Material_Texture2D_1,GetMaterialSharedSampler(samplerMaterial_Texture2D_1,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local20 = MaterialStoreTexSample(Parameters, Local19, 5);
	MaterialFloat Local21 = dot(Parameters.TangentToWorld[2],MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb);
	MaterialFloat Local22 = select((Local21 >= 0.00000000), 1.00000000, -1.00000000);
	MaterialFloat3 Local23 = (Local19.rgb * MaterialFloat3(MaterialFloat2(Local22,-1.00000000),1.00000000));
	MaterialFloat3 Local24 = (Local12.rgb * ((MaterialFloat3)Local23.r));
	MaterialFloat3 Local25 = cross(Local9,Local8);
	MaterialFloat Local26 = dot(Local25,Local25);
	MaterialFloat3 Local27 = normalize(Local25);
	MaterialFloat4 Local28 = select((abs(Local26 - 0.00000100) > 0.00001000), select((Local26 >= 0.00000100), MaterialFloat4(Local27,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	MaterialFloat3 Local29 = (Local28.rgb * ((MaterialFloat3)Local23.g));
	MaterialFloat3 Local30 = (Local24 + Local29);
	MaterialFloat3 Local31 = (Local8 * ((MaterialFloat3)Local23.b));
	MaterialFloat3 Local32 = (Local31 + MaterialFloat3(0.00000000,0.00000000,0.00000000));
	MaterialFloat3 Local33 = (Local30 + Local32);
	MaterialFloat3 Local34 = mul((MaterialFloat3x3)(Parameters.TangentToWorld), Local33);
	MaterialFloat3 Local35 = lerp(Local34,MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb,Material.PreshaderBuffer[0].w);
	MaterialFloat2 Local36 = (Local35.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local37 = dot(MaterialFloat3(Local5.rgb.rg,Local7),MaterialFloat3(Local36,Local35.b));
	MaterialFloat3 Local38 = (MaterialFloat3(Local5.rgb.rg,Local7) * ((MaterialFloat3)Local37));
	MaterialFloat3 Local39 = (((MaterialFloat3)Local7) * MaterialFloat3(Local36,Local35.b));
	MaterialFloat3 Local40 = (Local38 - Local39);
	MaterialFloat3 Local41 = mul(Local40, Parameters.TangentToWorld);
	MaterialFloat Local42 = GetDistanceToNearestSurfaceGlobal(DERIV_BASE_VALUE(Local0));
	MaterialFloat Local43 = (Local42 * Material.PreshaderBuffer[1].x);
	MaterialFloat Local44 = saturate(Local43);
	MaterialFloat3 Local45 = lerp(Local2,Local41,Local44);
	MaterialFloat3 Local46 = mul((MaterialFloat3x3)(Parameters.TangentToWorld), Local45);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local46;


#if TEMPLATE_USES_SUBSTRATE
	Parameters.SubstratePixelFootprint = SubstrateGetPixelFootprint(Parameters.WorldPosition_CamRelative, GetRoughnessFromNormalCurvature(Parameters));
	Parameters.SharedLocalBases = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTree = GetInitialisedSubstrateTree();
#if SUBSTRATE_USE_FULLYSIMPLIFIED_MATERIAL == 1
	Parameters.SharedLocalBasesFullySimplified = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTreeFullySimplified = GetInitialisedSubstrateTree();
#endif
#endif

	// Note that here MaterialNormal can be in world space or tangent space
	float3 MaterialNormal = GetMaterialNormal(Parameters, PixelMaterialInputs);

#if MATERIAL_TANGENTSPACENORMAL

#if FEATURE_LEVEL >= FEATURE_LEVEL_SM4
	// Mobile will rely on only the final normalize for performance
	MaterialNormal = normalize(MaterialNormal);
#endif

	// normalizing after the tangent space to world space conversion improves quality with sheared bases (UV layout to WS causes shrearing)
	// use full precision normalize to avoid overflows
	Parameters.WorldNormal = TransformTangentNormalToWorld(Parameters.TangentToWorld, MaterialNormal);

#else //MATERIAL_TANGENTSPACENORMAL

	Parameters.WorldNormal = normalize(MaterialNormal);

#endif //MATERIAL_TANGENTSPACENORMAL

#if MATERIAL_TANGENTSPACENORMAL || TWO_SIDED_WORLD_SPACE_SINGLELAYERWATER_NORMAL
	// flip the normal for backfaces being rendered with a two-sided material
	Parameters.WorldNormal *= Parameters.TwoSidedSign;
#endif

	Parameters.ReflectionVector = ReflectionAboutCustomWorldNormal(Parameters, Parameters.WorldNormal, false);

#if !PARTICLE_SPRITE_FACTORY
	Parameters.Particle.MotionBlurFade = 1.0f;
#endif // !PARTICLE_SPRITE_FACTORY

	// Now the rest of the inputs
	MaterialFloat3 Local47 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[2].xyz,Material.PreshaderBuffer[1].y);
	FWSVector2 Local48 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local15)), WSGetZ(DERIV_BASE_VALUE(Local15)));
	MaterialFloat2 Local49 = WSApplyAddressMode(DERIV_BASE_VALUE(Local48), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local50 = MaterialStoreTexCoordScale(Parameters, Local49, 3);
	MaterialFloat4 Local51 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local49));
	MaterialFloat Local52 = MaterialStoreTexSample(Parameters, Local51, 3);
	FWSVector2 Local53 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local15)), WSGetZ(DERIV_BASE_VALUE(Local15)));
	MaterialFloat2 Local54 = WSApplyAddressMode(DERIV_BASE_VALUE(Local53), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local55 = MaterialStoreTexCoordScale(Parameters, Local54, 3);
	MaterialFloat4 Local56 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local54));
	MaterialFloat Local57 = MaterialStoreTexSample(Parameters, Local56, 3);
	MaterialFloat Local58 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local59 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local58);
	MaterialFloat Local60 = saturate(Local59);
	MaterialFloat3 Local61 = lerp(Local51.rgb,Local56.rgb,Local60.r.r);
	MaterialFloat Local62 = MaterialStoreTexCoordScale(Parameters, Local17, 3);
	MaterialFloat4 Local63 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local64 = MaterialStoreTexSample(Parameters, Local63, 3);
	MaterialFloat Local65 = abs(Parameters.TangentToWorld[2].b);
	MaterialFloat Local66 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local65);
	MaterialFloat Local67 = saturate(Local66);
	MaterialFloat3 Local68 = lerp(Local61,Local63.rgb,Local67.r.r);
	MaterialFloat3 Local69 = (Local68 * Material.PreshaderBuffer[3].xyz);
	MaterialFloat3 Local70 = (((MaterialFloat3)1.00000000) - Local69);
	MaterialFloat3 Local71 = (Local70 * ((MaterialFloat3)2.00000000));
	MaterialFloat Local72 = MaterialStoreTexCoordScale(Parameters, Local49, 7);
	MaterialFloat4 Local73 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local49));
	MaterialFloat Local74 = MaterialStoreTexSample(Parameters, Local73, 7);
	MaterialFloat Local75 = MaterialStoreTexCoordScale(Parameters, Local54, 7);
	MaterialFloat4 Local76 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local54));
	MaterialFloat Local77 = MaterialStoreTexSample(Parameters, Local76, 7);
	MaterialFloat3 Local78 = lerp(Local73.rgb,Local76.rgb,Local60.r.r);
	MaterialFloat Local79 = MaterialStoreTexCoordScale(Parameters, Local17, 7);
	MaterialFloat4 Local80 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local81 = MaterialStoreTexSample(Parameters, Local80, 7);
	MaterialFloat3 Local82 = lerp(Local78,Local80.rgb,Local67.r.r);
	MaterialFloat Local83 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local3), 13);
	MaterialFloat4 Local84 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local3),View.MaterialTextureMipBias));
	MaterialFloat Local85 = MaterialStoreTexSample(Parameters, Local84, 13);
	MaterialFloat3 Local86 = lerp(Local82,((MaterialFloat3)Local84.r),0.50000000);
	MaterialFloat3 Local87 = (((MaterialFloat3)1.00000000) - Local86);
	MaterialFloat3 Local88 = (Local71 * Local87);
	MaterialFloat3 Local89 = (((MaterialFloat3)1.00000000) - Local88);
	MaterialFloat3 Local90 = (Local69 * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local91 = (Local90 * Local86);
	MaterialFloat Local92 = select((Local69.r >= 0.50000000), Local89.r, Local91.r);
	MaterialFloat Local93 = select((Local69.g >= 0.50000000), Local89.g, Local91.g);
	MaterialFloat Local94 = select((Local69.b >= 0.50000000), Local89.b, Local91.b);
	MaterialFloat3 Local95 = (Local87 + ((MaterialFloat3)Material.PreshaderBuffer[3].w));
	MaterialFloat Local96 = lerp(Material.PreshaderBuffer[4].y,Material.PreshaderBuffer[4].x,Local95.x);
	MaterialFloat Local97 = saturate(Local96);
	MaterialFloat3 Local98 = lerp(MaterialFloat3(MaterialFloat2(Local92,Local93),Local94),Material.PreshaderBuffer[5].xyz,Local97.r);
	MaterialFloat3 Local99 = lerp(Local98,MaterialFloat3(MaterialFloat2(Local92,Local93),Local94),Local44);
	MaterialFloat Local100 = MaterialStoreTexCoordScale(Parameters, Local49, 4);
	MaterialFloat4 Local101 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local49));
	MaterialFloat Local102 = MaterialStoreTexSample(Parameters, Local101, 4);
	MaterialFloat Local103 = MaterialStoreTexCoordScale(Parameters, Local54, 4);
	MaterialFloat4 Local104 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local54));
	MaterialFloat Local105 = MaterialStoreTexSample(Parameters, Local104, 4);
	MaterialFloat3 Local106 = lerp(Local101.rgb,Local104.rgb,Local60.r.r);
	MaterialFloat Local107 = MaterialStoreTexCoordScale(Parameters, Local17, 4);
	MaterialFloat4 Local108 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local109 = MaterialStoreTexSample(Parameters, Local108, 4);
	MaterialFloat3 Local110 = lerp(Local106,Local108.rgb,Local67.r.r);
	MaterialFloat3 Local111 = (Local110 * ((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat Local112 = (1.00000000 - Local44);
	MaterialFloat Local113 = (Material.PreshaderBuffer[6].x * Local112);
	MaterialFloat3 Local114 = (Local111 + ((MaterialFloat3)Local113));
	MaterialFloat3 Local115 = max(Local114,((MaterialFloat3)Material.PreshaderBuffer[6].y));
	MaterialFloat3 Local116 = min(Local115,((MaterialFloat3)Material.PreshaderBuffer[6].z));
	MaterialFloat Local117 = (Local44 + Material.PreshaderBuffer[6].w);
	MaterialFloat2 Local118 = GetPixelPosition(Parameters);
	MaterialFloat Local119 = View.TemporalAAParams.x;
	MaterialFloat2 Local120 = (Local118 + MaterialFloat2(Local119,Local119));
	MaterialFloat Local121 = CustomExpression0(Parameters,Local120);
	MaterialFloat2 Local122 = (Local118 / MaterialFloat2(64.00000000,64.00000000).rg);
	MaterialFloat Local123 = MaterialStoreTexCoordScale(Parameters, Local122, 0);
	MaterialFloat4 Local124 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSampleBias(Material_Texture2D_6,samplerMaterial_Texture2D_6,Local122,View.MaterialTextureMipBias));
	MaterialFloat Local125 = MaterialStoreTexSample(Parameters, Local124, 0);
	MaterialFloat Local126 = (Local121 + Local124.r);
	MaterialFloat Local127 = (Local126 * 0.16665000);
	MaterialFloat Local128 = (Local117 + Local127);
	MaterialFloat Local129 = (Local128 + -0.50000000);
	MaterialFloat Local130 = MaterialStoreTexCoordScale(Parameters, Local49, 6);
	MaterialFloat4 Local131 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local49));
	MaterialFloat Local132 = MaterialStoreTexSample(Parameters, Local131, 6);
	MaterialFloat Local133 = MaterialStoreTexCoordScale(Parameters, Local54, 6);
	MaterialFloat4 Local134 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local54));
	MaterialFloat Local135 = MaterialStoreTexSample(Parameters, Local134, 6);
	MaterialFloat3 Local136 = lerp(Local131.rgb,Local134.rgb,Local60.r.r);
	MaterialFloat Local137 = MaterialStoreTexCoordScale(Parameters, Local17, 6);
	MaterialFloat4 Local138 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local139 = MaterialStoreTexSample(Parameters, Local138, 6);
	MaterialFloat3 Local140 = lerp(Local136,Local138.rgb,Local67.r.r);

	PixelMaterialInputs.EmissiveColor = Local47;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = Local129;
	PixelMaterialInputs.BaseColor = Local99;
	PixelMaterialInputs.Metallic = 0.00000000;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local116;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local46;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local140;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 1;
	PixelMaterialInputs.FrontMaterial = GetInitialisedSubstrateData();
	PixelMaterialInputs.SurfaceThickness = 0.01000000;
	PixelMaterialInputs.Displacement = 0.50000000;


#if MATERIAL_USES_ANISOTROPY
	Parameters.WorldTangent = CalculateAnisotropyTangent(Parameters, PixelMaterialInputs);
#else
	Parameters.WorldTangent = 0;
#endif
}

#define UnityObjectToWorldDir TransformObjectToWorld

void SetupCommonData( int Parameters_PrimitiveId )
{
	View_MaterialTextureBilinearWrapedSampler = SamplerState_Linear_Repeat;
	View_MaterialTextureBilinearClampedSampler = SamplerState_Linear_Clamp;

	Material_Wrap_WorldGroupSettings = SamplerState_Linear_Repeat;
	Material_Clamp_WorldGroupSettings = SamplerState_Linear_Clamp;

	View.GameTime = View.RealTime = _Time.y;// _Time is (t/20, t, t*2, t*3)
	View.PrevFrameGameTime = View.GameTime - unity_DeltaTime.x;//(dt, 1/dt, smoothDt, 1/smoothDt)
	View.PrevFrameRealTime = View.RealTime;
	View.DeltaTime = unity_DeltaTime.x;
	View.MaterialTextureMipBias = 0.0;
	View.TemporalAAParams = float4( 0, 0, 0, 0 );
	View.ViewRectMin = float2( 0, 0 );
	View.ViewSizeAndInvSize = View_BufferSizeAndInvSize;
	View.MaterialTextureDerivativeMultiply = 1.0f;
	View.StateFrameIndexMod8 = 0;
	View.FrameNumber = (int)_Time.y;
	View.FieldOfViewWideAngles = float2( PI * 0.42f, PI * 0.42f );//75degrees, default unity
	View.RuntimeVirtualTextureMipLevel = float4( 0, 0, 0, 0 );
	View.PreExposure = 0;
	View.BufferBilinearUVMinMax = float4(
		View_BufferSizeAndInvSize.z * ( 0 + 0.5 ),//EffectiveViewRect.Min.X
		View_BufferSizeAndInvSize.w * ( 0 + 0.5 ),//EffectiveViewRect.Min.Y
		View_BufferSizeAndInvSize.z * ( View_BufferSizeAndInvSize.x - 0.5 ),//EffectiveViewRect.Max.X
		View_BufferSizeAndInvSize.w * ( View_BufferSizeAndInvSize.y - 0.5 ) );//EffectiveViewRect.Max.Y

	for( int i2 = 0; i2 < 40; i2++ )
		View.PrimitiveSceneData[ i2 ] = float4( 0, 0, 0, 0 );

	float4x4 LocalToWorld = transpose( UNITY_MATRIX_M );
    LocalToWorld[3] = float4(ToUnrealPos(LocalToWorld[3]), LocalToWorld[3].w);
	float4x4 WorldToLocal = transpose( UNITY_MATRIX_I_M );
	float4x4 ViewMatrix = transpose( UNITY_MATRIX_V );
	float4x4 InverseViewMatrix = transpose( UNITY_MATRIX_I_V );
	float4x4 ViewProjectionMatrix = transpose( UNITY_MATRIX_VP );
	uint PrimitiveBaseOffset = Parameters_PrimitiveId * PRIMITIVE_SCENE_DATA_STRIDE;
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 0 ] = LocalToWorld[ 0 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 1 ] = LocalToWorld[ 1 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 2 ] = LocalToWorld[ 2 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 3 ] = LocalToWorld[ 3 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 5 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 100.0 );//ObjectWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 6 ] = WorldToLocal[ 0 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 7 ] = WorldToLocal[ 1 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 8 ] = WorldToLocal[ 2 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 9 ] = WorldToLocal[ 3 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 10 ] = LocalToWorld[ 0 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 11 ] = LocalToWorld[ 1 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 12 ] = LocalToWorld[ 2 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 13 ] = LocalToWorld[ 3 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 18 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 0 );//ActorWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 19 ] = LocalObjectBoundsMax - LocalObjectBoundsMin;//ObjectBounds
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 21 ] = mul( LocalToWorld, float3( 1, 0, 0 ) );
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 23 ] = LocalObjectBoundsMin;//LocalObjectBoundsMin 
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 24 ] = LocalObjectBoundsMax;//LocalObjectBoundsMax

#ifdef UE5
	ResolvedView.WorldCameraOrigin = LWCPromote( ToUnrealPos( _WorldSpaceCameraPos.xyz ) );
	ResolvedView.PreViewTranslation = LWCPromote( float3( 0, 0, 0 ) );
	ResolvedView.WorldViewOrigin = LWCPromote( float3( 0, 0, 0 ) );
#else
	ResolvedView.WorldCameraOrigin = ToUnrealPos( _WorldSpaceCameraPos.xyz );
	ResolvedView.PreViewTranslation = float3( 0, 0, 0 );
	ResolvedView.WorldViewOrigin = float3( 0, 0, 0 );
#endif
	ResolvedView.PrevWorldCameraOrigin = ResolvedView.WorldCameraOrigin;
	ResolvedView.ScreenPositionScaleBias = float4( 1, 1, 0, 0 );
	ResolvedView.TranslatedWorldToView		 = ViewMatrix;
	ResolvedView.TranslatedWorldToCameraView = ViewMatrix;
	ResolvedView.TranslatedWorldToClip		 = ViewProjectionMatrix;
	ResolvedView.ViewToTranslatedWorld		 = InverseViewMatrix;
	ResolvedView.PrevViewToTranslatedWorld = ResolvedView.ViewToTranslatedWorld;
	ResolvedView.CameraViewToTranslatedWorld = InverseViewMatrix;
	ResolvedView.BufferBilinearUVMinMax = View.BufferBilinearUVMinMax;
	Primitive.WorldToLocal = WorldToLocal;
	Primitive.LocalToWorld = LocalToWorld;
}
#define VS_USES_UNREAL_SPACE 1
float3 PrepareAndGetWPO( float4 VertexColor, float3 UnrealWorldPos, float3 UnrealNormal, float4 InTangent,
						 float4 UV0, float4 UV1 )
{
	InitializeExpressions();
	FMaterialVertexParameters Parameters = (FMaterialVertexParameters)0;

	float3 InWorldNormal = UnrealNormal;
	float4 tangentWorld = InTangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	
	#ifdef VS_USES_UNREAL_SPACE
		UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	#endif
	Parameters.WorldPosition = UnrealWorldPos;
	#ifdef VS_USES_UNREAL_SPACE
		Parameters.TangentToWorld[ 0 ] = Parameters.TangentToWorld[ 0 ].xzy;
		Parameters.TangentToWorld[ 1 ] = Parameters.TangentToWorld[ 1 ].xzy;
		Parameters.TangentToWorld[ 2 ] = Parameters.TangentToWorld[ 2 ].xzy;//WorldAligned texturing uses normals that think Z is up
	#endif

	Parameters.VertexColor = VertexColor;

#if NUM_MATERIAL_TEXCOORDS_VERTEX > 0			
	Parameters.TexCoords[ 0 ] = float2( UV0.x, UV0.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 1
	Parameters.TexCoords[ 1 ] = float2( UV1.x, UV1.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( UV0.x, UV0.y );
	}
#endif

	Parameters.PrimitiveId = 0;

	SetupCommonData( Parameters.PrimitiveId );

#ifdef UE5
	Parameters.PrevFrameLocalToWorld = MakeLWCMatrix( float3( 0, 0, 0 ), Primitive.LocalToWorld );
#else
	Parameters.PrevFrameLocalToWorld = Primitive.LocalToWorld;
#endif
	
	float3 Offset = float3( 0, 0, 0 );
	Offset = GetMaterialWorldPositionOffset( Parameters );
	#ifdef VS_USES_UNREAL_SPACE
		//Convert from unreal units to unity
		Offset /= float3( 100, 100, 100 );
		Offset = Offset.xzy;
	#endif
	return Offset;
}

void SurfaceReplacement( Input In, out SurfaceOutputStandard o )
{
	InitializeExpressions();

	float3 Z3 = float3( 0, 0, 0 );
	float4 Z4 = float4( 0, 0, 0, 0 );

	float3 UnrealWorldPos = float3( In.worldPos.x, In.worldPos.y, In.worldPos.z );

	float3 UnrealNormal = In.normal2;	

	FMaterialPixelParameters Parameters = (FMaterialPixelParameters)0;
#if NUM_TEX_COORD_INTERPOLATORS > 0			
	Parameters.TexCoords[ 0 ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 1
	Parameters.TexCoords[ 1 ] = float2( In.uv2_Material_Texture2D_0.x, 1.0 - In.uv2_Material_Texture2D_0.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
	}
#endif
	Parameters.VertexColor = In.color;
	Parameters.WorldNormal = UnrealNormal;
	Parameters.ReflectionVector = half3( 0, 0, 1 );
	Parameters.CameraVector = normalize( _WorldSpaceCameraPos.xyz - UnrealWorldPos.xyz );
	//Parameters.CameraVector = mul( ( float3x3 )unity_CameraToWorld, float3( 0, 0, 1 ) ) * -1;
	Parameters.LightVector = half3( 0, 0, 0 );
	//float4 screenpos = In.screenPos;
	//screenpos /= screenpos.w;
	Parameters.SvPosition = In.screenPos;
	Parameters.ScreenPosition = Parameters.SvPosition;

	Parameters.UnMirrored = 1;

	Parameters.TwoSidedSign = 1;


	float3 InWorldNormal = UnrealNormal;	
	float4 tangentWorld = In.tangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	//WorldAlignedTexturing in UE relies on the fact that coords there are 100x larger, prepare values for that
	//but watch out for any computation that might get skewed as a side effect
	UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	
	Parameters.AbsoluteWorldPosition = UnrealWorldPos;
	Parameters.WorldPosition_CamRelative = UnrealWorldPos;
	Parameters.WorldPosition_NoOffsets = UnrealWorldPos;

	Parameters.WorldPosition_NoOffsets_CamRelative = Parameters.WorldPosition_CamRelative;
	Parameters.LightingPositionOffset = float3( 0, 0, 0 );

	Parameters.AOMaterialMask = 0;

	Parameters.Particle.RelativeTime = 0;
	Parameters.Particle.MotionBlurFade;
	Parameters.Particle.Random = 0;
	Parameters.Particle.Velocity = half4( 1, 1, 1, 1 );
	Parameters.Particle.Color = half4( 1, 1, 1, 1 );
	Parameters.Particle.TranslatedWorldPositionAndSize = float4( UnrealWorldPos, 0 );
	Parameters.Particle.MacroUV = half4( 0, 0, 1, 1 );
	Parameters.Particle.DynamicParameter = half4( 0, 0, 0, 0 );
	Parameters.Particle.LocalToWorld = float4x4( Z4, Z4, Z4, Z4 );
	Parameters.Particle.Size = float2( 1, 1 );
	Parameters.Particle.SubUVCoords[ 0 ] = Parameters.Particle.SubUVCoords[ 1 ] = float2( 0, 0 );
	Parameters.Particle.SubUVLerp = 0.0;
	Parameters.TexCoordScalesParams = float2( 0, 0 );
	Parameters.PrimitiveId = 0;
	Parameters.VirtualTextureFeedback = 0;

	FPixelMaterialInputs PixelMaterialInputs = (FPixelMaterialInputs)0;
	PixelMaterialInputs.Normal = float3( 0, 0, 1 );
	PixelMaterialInputs.ShadingModel = 0;
	PixelMaterialInputs.FrontMaterial = 0;

	SetupCommonData( Parameters.PrimitiveId );
	//CustomizedUVs
	#if NUM_TEX_COORD_INTERPOLATORS > 0 && HAS_CUSTOMIZED_UVS
		float2 OutTexCoords[ NUM_TEX_COORD_INTERPOLATORS ];
		//Prevent uninitialized reads
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			OutTexCoords[ i ] = float2( 0, 0 );
		}
		GetMaterialCustomizedUVs( Parameters, OutTexCoords );
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			Parameters.TexCoords[ i ] = OutTexCoords[ i ];
		}
	#endif
	//<-
	CalcPixelMaterialInputs( Parameters, PixelMaterialInputs );

	#define HAS_WORLDSPACE_NORMAL 0
	#if HAS_WORLDSPACE_NORMAL
		PixelMaterialInputs.Normal = mul( PixelMaterialInputs.Normal, (MaterialFloat3x3)( transpose( Parameters.TangentToWorld ) ) );
	#endif

	o.Albedo = PixelMaterialInputs.BaseColor.rgb;
	o.Alpha = PixelMaterialInputs.Opacity;
	if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}