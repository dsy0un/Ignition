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
TEXTURE2D(       Material_Texture2D_8 );
SAMPLER(  samplerMaterial_Texture2D_8 );
float4 Material_Texture2D_8_TexelSize;
float4 Material_Texture2D_8_ST;
TEXTURE2D(       Material_Texture2D_9 );
SAMPLER(  samplerMaterial_Texture2D_9 );
float4 Material_Texture2D_9_TexelSize;
float4 Material_Texture2D_9_ST;

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
	float4 PreshaderBuffer[18];
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
	Material.PreshaderBuffer[0] = float4(-0.001091,16256.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(0.000000,0.000000,0.000000,0.000135);//(Unknown)
	Material.PreshaderBuffer[2] = float4(-0.000073,1.473589,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.851251,1.000000,0.575000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(1.000000,0.844167,0.450000,0.000949);//(Unknown)
	Material.PreshaderBuffer[5] = float4(0.767000,5.000000,1.000000,4.000000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(-0.000488,-0.000488,-0.000488,0.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(3.000000,2.529550,0.775001,-0.000382);//(Unknown)
	Material.PreshaderBuffer[8] = float4(1.000000,1.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[9] = float4(3.000000,3.000000,3.000000,0.019589);//(Unknown)
	Material.PreshaderBuffer[10] = float4(0.470836,0.013528,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[11] = float4(-0.000049,-0.000049,-0.000049,0.913000);//(Unknown)
	Material.PreshaderBuffer[12] = float4(0.108000,5.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[13] = float4(50.000000,50.000000,0.000000,0.367000);//(Unknown)
	Material.PreshaderBuffer[14] = float4(0.100002,4112.532715,0.072000,0.000000);//(Unknown)
	Material.PreshaderBuffer[15] = float4(-0.000063,-0.000063,-0.000063,0.647806);//(Unknown)
	Material.PreshaderBuffer[16] = float4(1.000000,0.900000,0.000000,0.564800);//(Unknown)
	Material.PreshaderBuffer[17] = float4(0.475812,1.010000,0.000000,0.000000);//(Unknown)
}float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
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
	MaterialFloat3 Local0 = normalize(Parameters.TangentToWorld[2]);
	MaterialFloat3 Local1 = cross(Local0,normalize(MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb));
	MaterialFloat Local2 = dot(Local1,Local1);
	MaterialFloat3 Local3 = normalize(Local1);
	MaterialFloat4 Local4 = select((abs(Local2 - 0.00000100) > 0.00001000), select((Local2 >= 0.00000100), MaterialFloat4(Local3,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	FWSVector3 Local5 = GetWorldPosition_NoMaterialOffsets(Parameters);
	FWSVector3 Local6 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local5)), WSGetY(DERIV_BASE_VALUE(Local5)), WSGetZ(DERIV_BASE_VALUE(Local5)));
	FWSVector3 Local7 = WSMultiply(DERIV_BASE_VALUE(Local6), ((MaterialFloat3)Material.PreshaderBuffer[0].x));
	FWSVector2 Local8 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local7)), WSGetZ(DERIV_BASE_VALUE(Local7)));
	MaterialFloat2 Local9 = WSApplyAddressMode(DERIV_BASE_VALUE(Local8), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local10 = MaterialStoreTexCoordScale(Parameters, Local9, 5);
	MaterialFloat4 Local11 = UnpackNormalMap(Texture2DSample(Material_Texture2D_0,GetMaterialSharedSampler(samplerMaterial_Texture2D_0,View_MaterialTextureBilinearWrapedSampler),Local9));
	MaterialFloat Local12 = MaterialStoreTexSample(Parameters, Local11, 5);
	MaterialFloat Local13 = dot(Parameters.TangentToWorld[2],MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb);
	MaterialFloat Local14 = select((Local13 >= 0.00000000), -1.00000000, 1.00000000);
	MaterialFloat3 Local15 = (Local11.rgb * MaterialFloat3(MaterialFloat2(Local14,-1.00000000),1.00000000));
	FWSVector2 Local16 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local7)), WSGetZ(DERIV_BASE_VALUE(Local7)));
	MaterialFloat2 Local17 = WSApplyAddressMode(DERIV_BASE_VALUE(Local16), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local18 = MaterialStoreTexCoordScale(Parameters, Local17, 5);
	MaterialFloat4 Local19 = UnpackNormalMap(Texture2DSample(Material_Texture2D_0,GetMaterialSharedSampler(samplerMaterial_Texture2D_0,View_MaterialTextureBilinearWrapedSampler),Local17));
	MaterialFloat Local20 = MaterialStoreTexSample(Parameters, Local19, 5);
	MaterialFloat Local21 = dot(Parameters.TangentToWorld[2],MaterialFloat3(1.00000000,0.00000000,0.00000000).rgb);
	MaterialFloat Local22 = select((Local21 >= 0.00000000), 1.00000000, -1.00000000);
	MaterialFloat3 Local23 = (Local19.rgb * MaterialFloat3(MaterialFloat2(Local22,-1.00000000),1.00000000));
	MaterialFloat3 Local24 = mul(MaterialFloat3(0.00000000,0.00000000,1.00000000), Parameters.TangentToWorld);
	MaterialFloat Local25 = abs(Local24.r);
	MaterialFloat Local26 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),DERIV_BASE_VALUE(Local25));
	MaterialFloat Local27 = saturate(DERIV_BASE_VALUE(Local26));
	MaterialFloat Local28 = DERIV_BASE_VALUE(Local27).r;
	MaterialFloat3 Local29 = lerp(Local15,Local23,DERIV_BASE_VALUE(Local28));
	MaterialFloat3 Local30 = (Local4.rgb * ((MaterialFloat3)Local29.r));
	MaterialFloat3 Local31 = cross(Local1,Local0);
	MaterialFloat Local32 = dot(Local31,Local31);
	MaterialFloat3 Local33 = normalize(Local31);
	MaterialFloat4 Local34 = select((abs(Local32 - 0.00000100) > 0.00001000), select((Local32 >= 0.00000100), MaterialFloat4(Local33,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	MaterialFloat3 Local35 = (Local34.rgb * ((MaterialFloat3)Local29.g));
	MaterialFloat3 Local36 = (Local30 + Local35);
	MaterialFloat3 Local37 = (Local0 * ((MaterialFloat3)Local29.b));
	MaterialFloat3 Local38 = (Local37 + MaterialFloat3(0.00000000,0.00000000,0.00000000));
	MaterialFloat3 Local39 = (Local36 + Local38);
	MaterialFloat3 Local40 = cross(Local0,normalize(MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb));
	MaterialFloat Local41 = dot(Local40,Local40);
	MaterialFloat3 Local42 = normalize(Local40);
	MaterialFloat4 Local43 = select((abs(Local41 - 0.00000100) > 0.00001000), select((Local41 >= 0.00000100), MaterialFloat4(Local42,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	FWSVector2 Local44 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local7)), WSGetY(DERIV_BASE_VALUE(Local7)));
	MaterialFloat2 Local45 = WSApplyAddressMode(DERIV_BASE_VALUE(Local44), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local46 = MaterialStoreTexCoordScale(Parameters, Local45, 5);
	MaterialFloat4 Local47 = UnpackNormalMap(Texture2DSample(Material_Texture2D_0,GetMaterialSharedSampler(samplerMaterial_Texture2D_0,View_MaterialTextureBilinearWrapedSampler),Local45));
	MaterialFloat Local48 = MaterialStoreTexSample(Parameters, Local47, 5);
	MaterialFloat Local49 = dot(Parameters.TangentToWorld[2],MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb);
	MaterialFloat Local50 = select((Local49 >= 0.00000000), 1.00000000, -1.00000000);
	MaterialFloat3 Local51 = (Local47.rgb * MaterialFloat3(MaterialFloat2(Local50,-1.00000000),1.00000000));
	MaterialFloat3 Local52 = (Local43.rgb * ((MaterialFloat3)Local51.r));
	MaterialFloat3 Local53 = cross(Local40,Local0);
	MaterialFloat Local54 = dot(Local53,Local53);
	MaterialFloat3 Local55 = normalize(Local53);
	MaterialFloat4 Local56 = select((abs(Local54 - 0.00000100) > 0.00001000), select((Local54 >= 0.00000100), MaterialFloat4(Local55,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	MaterialFloat3 Local57 = (Local56.rgb * ((MaterialFloat3)Local51.g));
	MaterialFloat3 Local58 = (Local52 + Local57);
	MaterialFloat3 Local59 = (Local0 * ((MaterialFloat3)Local51.b));
	MaterialFloat3 Local60 = (Local59 + MaterialFloat3(0.00000000,0.00000000,0.00000000));
	MaterialFloat3 Local61 = (Local58 + Local60);
	MaterialFloat Local62 = abs(Local24.b);
	MaterialFloat Local63 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),DERIV_BASE_VALUE(Local62));
	MaterialFloat Local64 = saturate(DERIV_BASE_VALUE(Local63));
	MaterialFloat Local65 = DERIV_BASE_VALUE(Local64).r;
	MaterialFloat3 Local66 = lerp(Local39,Local61,DERIV_BASE_VALUE(Local65));
	MaterialFloat3 Local67 = mul((MaterialFloat3x3)(Parameters.TangentToWorld), Local66);
	MaterialFloat Local68 = (Local67.b + 1.00000000);
	MaterialFloat Local69 = MaterialStoreTexCoordScale(Parameters, ((MaterialFloat2)Material.PreshaderBuffer[0].y), 6);
	MaterialFloat4 Local70 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,((MaterialFloat2)Material.PreshaderBuffer[0].y),View.MaterialTextureMipBias));
	MaterialFloat Local71 = MaterialStoreTexSample(Parameters, Local70, 6);
	MaterialFloat2 Local72 = (Local70.rgb.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local73 = dot(MaterialFloat3(Local67.rg,Local68),MaterialFloat3(Local72,Local70.rgb.b));
	MaterialFloat3 Local74 = (MaterialFloat3(Local67.rg,Local68) * ((MaterialFloat3)Local73));
	MaterialFloat3 Local75 = (((MaterialFloat3)Local68) * MaterialFloat3(Local72,Local70.rgb.b));
	MaterialFloat3 Local76 = (Local74 - Local75);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local76;


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
	MaterialFloat3 Local77 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[1].xyz,Material.PreshaderBuffer[0].z);
	MaterialFloat Local78 = GetPixelDepth(Parameters);
	MaterialFloat Local79 = CalcSceneDepth(ScreenAlignedPosition(GetScreenPosition(Parameters)));
	MaterialFloat Local80 = (Local79 - DERIV_BASE_VALUE(Local78));
	MaterialFloat Local81 = (Local80 * Material.PreshaderBuffer[1].w);
	MaterialFloat Local82 = saturate(Local81);
	FWSVector3 Local83 = WSMultiply(DERIV_BASE_VALUE(Local6), ((MaterialFloat3)Material.PreshaderBuffer[2].x));
	FWSVector2 Local84 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local83)), WSGetZ(DERIV_BASE_VALUE(Local83)));
	MaterialFloat2 Local85 = WSApplyAddressMode(DERIV_BASE_VALUE(Local84), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local86 = MaterialStoreTexCoordScale(Parameters, Local85, 12);
	MaterialFloat4 Local87 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local85));
	MaterialFloat Local88 = MaterialStoreTexSample(Parameters, Local87, 12);
	FWSVector2 Local89 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local83)), WSGetZ(DERIV_BASE_VALUE(Local83)));
	MaterialFloat2 Local90 = WSApplyAddressMode(DERIV_BASE_VALUE(Local89), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local91 = MaterialStoreTexCoordScale(Parameters, Local90, 12);
	MaterialFloat4 Local92 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local90));
	MaterialFloat Local93 = MaterialStoreTexSample(Parameters, Local92, 12);
	MaterialFloat Local94 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local95 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local94);
	MaterialFloat Local96 = saturate(Local95);
	MaterialFloat3 Local97 = lerp(Local87.rgb,Local92.rgb,Local96.r.r);
	FWSVector2 Local98 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local83)), WSGetY(DERIV_BASE_VALUE(Local83)));
	MaterialFloat2 Local99 = WSApplyAddressMode(DERIV_BASE_VALUE(Local98), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local100 = MaterialStoreTexCoordScale(Parameters, Local99, 12);
	MaterialFloat4 Local101 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local99));
	MaterialFloat Local102 = MaterialStoreTexSample(Parameters, Local101, 12);
	MaterialFloat Local103 = abs(Parameters.TangentToWorld[2].b);
	MaterialFloat Local104 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local103);
	MaterialFloat Local105 = saturate(Local104);
	MaterialFloat3 Local106 = lerp(Local97,Local101.rgb,Local105.r.r);
	MaterialFloat3 Local107 = (((MaterialFloat3)Local82) * Local106);
	MaterialFloat3 Local108 = lerp(Local107,((MaterialFloat3)Local82),Material.PreshaderBuffer[2].y);
	MaterialFloat3 Local109 = lerp(Material.PreshaderBuffer[4].xyz,Material.PreshaderBuffer[3].xyz,Local108);
	MaterialFloat Local110 = (Local80 * Material.PreshaderBuffer[4].w);
	MaterialFloat Local111 = saturate(Local110);
	MaterialFloat Local112 = (Local111 + Material.PreshaderBuffer[5].x);
	MaterialFloat Local113 = PositiveClampedPow(Local112,Material.PreshaderBuffer[5].y);
	MaterialFloat Local114 = lerp(Local113,1.00000000,0.50000000);
	MaterialFloat Local115 = saturate(Local114);
	MaterialFloat Local116 = (Local115 * Material.PreshaderBuffer[5].z);
	MaterialFloat Local117 = (1.00000000 - Local116);
	FWSVector3 Local118 = GetWorldPosition(Parameters);
	FWSVector3 Local119 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local118)), WSGetY(DERIV_BASE_VALUE(Local118)), WSGetZ(DERIV_BASE_VALUE(Local118)));
	MaterialFloat Local120 = (Material.PreshaderBuffer[5].w * View.GameTime);
	FWSVector3 Local121 = WSAdd(DERIV_BASE_VALUE(Local119), ((MaterialFloat3)Local120));
	FWSVector3 Local122 = WSMultiply(DERIV_BASE_VALUE(Local121), Material.PreshaderBuffer[6].xyz);
	FWSVector2 Local123 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local122)), WSGetZ(DERIV_BASE_VALUE(Local122)));
	MaterialFloat2 Local124 = WSApplyAddressMode(DERIV_BASE_VALUE(Local123), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local125 = MaterialStoreTexCoordScale(Parameters, Local124, 9);
	MaterialFloat4 Local126 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local124));
	MaterialFloat Local127 = MaterialStoreTexSample(Parameters, Local126, 9);
	FWSVector2 Local128 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local122)), WSGetZ(DERIV_BASE_VALUE(Local122)));
	MaterialFloat2 Local129 = WSApplyAddressMode(DERIV_BASE_VALUE(Local128), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local130 = MaterialStoreTexCoordScale(Parameters, Local129, 9);
	MaterialFloat4 Local131 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local129));
	MaterialFloat Local132 = MaterialStoreTexSample(Parameters, Local131, 9);
	MaterialFloat3 Local133 = lerp(Local126.rgb,Local131.rgb,Local96.r.r);
	FWSVector2 Local134 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local122)), WSGetY(DERIV_BASE_VALUE(Local122)));
	MaterialFloat2 Local135 = WSApplyAddressMode(DERIV_BASE_VALUE(Local134), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local136 = MaterialStoreTexCoordScale(Parameters, Local135, 9);
	MaterialFloat4 Local137 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local135));
	MaterialFloat Local138 = MaterialStoreTexSample(Parameters, Local137, 9);
	MaterialFloat3 Local139 = lerp(Local133,Local137.rgb,Local105.r.r);
	MaterialFloat3 Local140 = (((MaterialFloat3)0.20000000) * Local139);
	MaterialFloat3 Local141 = (((MaterialFloat3)Local117) - Local140);
	MaterialFloat3 Local142 = saturate(Local141);
	MaterialFloat3 Local143 = lerp(Local109,Material.PreshaderBuffer[7].xyz,Local142);
	FWSVector3 Local144 = WSMultiply(DERIV_BASE_VALUE(Local6), ((MaterialFloat3)Material.PreshaderBuffer[7].w));
	FWSVector2 Local145 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local144)), WSGetZ(DERIV_BASE_VALUE(Local144)));
	MaterialFloat2 Local146 = WSApplyAddressMode(DERIV_BASE_VALUE(Local145), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local147 = MaterialStoreTexCoordScale(Parameters, Local146, 4);
	MaterialFloat4 Local148 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_4,GetMaterialSharedSampler(samplerMaterial_Texture2D_4,View_MaterialTextureBilinearWrapedSampler),Local146));
	MaterialFloat Local149 = MaterialStoreTexSample(Parameters, Local148, 4);
	FWSVector2 Local150 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local144)), WSGetZ(DERIV_BASE_VALUE(Local144)));
	MaterialFloat2 Local151 = WSApplyAddressMode(DERIV_BASE_VALUE(Local150), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local152 = MaterialStoreTexCoordScale(Parameters, Local151, 4);
	MaterialFloat4 Local153 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_4,GetMaterialSharedSampler(samplerMaterial_Texture2D_4,View_MaterialTextureBilinearWrapedSampler),Local151));
	MaterialFloat Local154 = MaterialStoreTexSample(Parameters, Local153, 4);
	MaterialFloat3 Local155 = lerp(Local148.rgb,Local153.rgb,Local96.r.r);
	FWSVector2 Local156 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local144)), WSGetY(DERIV_BASE_VALUE(Local144)));
	MaterialFloat2 Local157 = WSApplyAddressMode(DERIV_BASE_VALUE(Local156), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local158 = MaterialStoreTexCoordScale(Parameters, Local157, 4);
	MaterialFloat4 Local159 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_4,GetMaterialSharedSampler(samplerMaterial_Texture2D_4,View_MaterialTextureBilinearWrapedSampler),Local157));
	MaterialFloat Local160 = MaterialStoreTexSample(Parameters, Local159, 4);
	MaterialFloat3 Local161 = lerp(Local155,Local159.rgb,Local105.r.r);
	MaterialFloat3 Local162 = lerp(Local143,Material.PreshaderBuffer[8].xyz,Local161);
	FWSVector3 Local163 = WSMultiply(DERIV_BASE_VALUE(Local6), Material.PreshaderBuffer[6].xyz);
	FWSVector2 Local164 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local163)), WSGetZ(DERIV_BASE_VALUE(Local163)));
	MaterialFloat2 Local165 = WSApplyAddressMode(DERIV_BASE_VALUE(Local164), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local166 = MaterialStoreTexCoordScale(Parameters, Local165, 8);
	MaterialFloat4 Local167 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local165));
	MaterialFloat Local168 = MaterialStoreTexSample(Parameters, Local167, 8);
	FWSVector2 Local169 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local163)), WSGetZ(DERIV_BASE_VALUE(Local163)));
	MaterialFloat2 Local170 = WSApplyAddressMode(DERIV_BASE_VALUE(Local169), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local171 = MaterialStoreTexCoordScale(Parameters, Local170, 8);
	MaterialFloat4 Local172 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local170));
	MaterialFloat Local173 = MaterialStoreTexSample(Parameters, Local172, 8);
	MaterialFloat3 Local174 = lerp(Local167.rgb,Local172.rgb,Local96.r.r);
	FWSVector2 Local175 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local163)), WSGetY(DERIV_BASE_VALUE(Local163)));
	MaterialFloat2 Local176 = WSApplyAddressMode(DERIV_BASE_VALUE(Local175), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local177 = MaterialStoreTexCoordScale(Parameters, Local176, 8);
	MaterialFloat4 Local178 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_5,GetMaterialSharedSampler(samplerMaterial_Texture2D_5,View_MaterialTextureBilinearWrapedSampler),Local176));
	MaterialFloat Local179 = MaterialStoreTexSample(Parameters, Local178, 8);
	MaterialFloat3 Local180 = lerp(Local174,Local178.rgb,Local105.r.r);
	MaterialFloat3 Local181 = (Local180 * Material.PreshaderBuffer[9].xyz);
	MaterialFloat Local182 = (Local80 * Material.PreshaderBuffer[9].w);
	MaterialFloat Local183 = saturate(Local182);
	MaterialFloat Local184 = lerp(Material.PreshaderBuffer[10].y,Material.PreshaderBuffer[10].x,Local183);
	FWSVector3 Local185 = WSMultiply(DERIV_BASE_VALUE(Local121), Material.PreshaderBuffer[11].xyz);
	FWSVector2 Local186 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local185)), WSGetZ(DERIV_BASE_VALUE(Local185)));
	MaterialFloat2 Local187 = WSApplyAddressMode(DERIV_BASE_VALUE(Local186), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local188 = MaterialStoreTexCoordScale(Parameters, Local187, 9);
	MaterialFloat4 Local189 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local187));
	MaterialFloat Local190 = MaterialStoreTexSample(Parameters, Local189, 9);
	FWSVector2 Local191 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local185)), WSGetZ(DERIV_BASE_VALUE(Local185)));
	MaterialFloat2 Local192 = WSApplyAddressMode(DERIV_BASE_VALUE(Local191), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local193 = MaterialStoreTexCoordScale(Parameters, Local192, 9);
	MaterialFloat4 Local194 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local192));
	MaterialFloat Local195 = MaterialStoreTexSample(Parameters, Local194, 9);
	MaterialFloat3 Local196 = lerp(Local189.rgb,Local194.rgb,Local96.r.r);
	FWSVector2 Local197 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local185)), WSGetY(DERIV_BASE_VALUE(Local185)));
	MaterialFloat2 Local198 = WSApplyAddressMode(DERIV_BASE_VALUE(Local197), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local199 = MaterialStoreTexCoordScale(Parameters, Local198, 9);
	MaterialFloat4 Local200 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_3,GetMaterialSharedSampler(samplerMaterial_Texture2D_3,View_MaterialTextureBilinearWrapedSampler),Local198));
	MaterialFloat Local201 = MaterialStoreTexSample(Parameters, Local200, 9);
	MaterialFloat3 Local202 = lerp(Local196,Local200.rgb,Local105.r.r);
	MaterialFloat3 Local203 = lerp(Local202,Local139,0.50000000);
	MaterialFloat3 Local204 = (Local203 * ((MaterialFloat3)Material.PreshaderBuffer[11].w));
	MaterialFloat3 Local205 = (Local204 + ((MaterialFloat3)Material.PreshaderBuffer[12].x));
	MaterialFloat3 Local206 = PositiveClampedPow(Local205,((MaterialFloat3)Material.PreshaderBuffer[12].y));
	MaterialFloat3 Local207 = saturate(Local206);
	MaterialFloat Local208 = (Local184 * Local207.r);
	MaterialFloat3 Local209 = lerp(Local162,Local181,Local208);
	MaterialFloat3 Local210 = (Material.PreshaderBuffer[13].xyz * ((MaterialFloat3)View.GameTime));
	FWSVector3 Local211 = WSAdd(DERIV_BASE_VALUE(Local119), Local210);
	MaterialFloat Local212 = (View.GameTime * Material.PreshaderBuffer[13].w);
	MaterialFloat Local213 = (View.GameTime * Material.PreshaderBuffer[14].x);
	MaterialFloat2 Local214 = Parameters.TexCoords[0].xy;
	MaterialFloat2 Local215 = (DERIV_BASE_VALUE(Local214) * ((MaterialFloat2)Material.PreshaderBuffer[14].y));
	MaterialFloat2 Local216 = (MaterialFloat2(Local212,Local213) + DERIV_BASE_VALUE(Local215));
	MaterialFloat Local217 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local216), 14);
	MaterialFloat4 Local218 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_6,samplerMaterial_Texture2D_6,DERIV_BASE_VALUE(Local216),View.MaterialTextureMipBias));
	MaterialFloat Local219 = MaterialStoreTexSample(Parameters, Local218, 14);
	FWSVector3 Local220 = WSMultiply(DERIV_BASE_VALUE(Local211), Local218.rgb);
	MaterialFloat Local221 = (Material.PreshaderBuffer[14].z * Local82);
	FWSVector3 Local222 = WSLerp(DERIV_BASE_VALUE(Local211),Local220,Local221);
	FWSVector3 Local223 = WSMultiply(Local222, Material.PreshaderBuffer[15].xyz);
	MaterialFloat2 Local224 = WSApplyAddressMode(MakeWSVector(WSGetX(Local223), WSGetZ(Local223)), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local225 = MaterialStoreTexCoordScale(Parameters, Local224, 13);
	MaterialFloat4 Local226 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local224));
	MaterialFloat Local227 = MaterialStoreTexSample(Parameters, Local226, 13);
	MaterialFloat2 Local228 = WSApplyAddressMode(MakeWSVector(WSGetY(Local223), WSGetZ(Local223)), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local229 = MaterialStoreTexCoordScale(Parameters, Local228, 13);
	MaterialFloat4 Local230 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local228));
	MaterialFloat Local231 = MaterialStoreTexSample(Parameters, Local230, 13);
	MaterialFloat3 Local232 = lerp(Local226.rgb,Local230.rgb,Local96.r.r);
	MaterialFloat2 Local233 = WSApplyAddressMode(MakeWSVector(WSGetX(Local223), WSGetY(Local223)), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local234 = MaterialStoreTexCoordScale(Parameters, Local233, 13);
	MaterialFloat4 Local235 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),Local233));
	MaterialFloat Local236 = MaterialStoreTexSample(Parameters, Local235, 13);
	MaterialFloat3 Local237 = lerp(Local232,Local235.rgb,Local105.r.r);
	MaterialFloat3 Local238 = (Local237 * ((MaterialFloat3)Material.PreshaderBuffer[15].w));
	MaterialFloat3 Local239 = lerp(Local209,Material.PreshaderBuffer[16].xyz,Local238);
	MaterialFloat Local240 = lerp(Material.PreshaderBuffer[17].x,Material.PreshaderBuffer[16].w,Local184);
	MaterialFloat Local241 = MaterialStoreTexCoordScale(Parameters, Local124, 11);
	MaterialFloat4 Local242 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_8,GetMaterialSharedSampler(samplerMaterial_Texture2D_8,View_MaterialTextureBilinearWrapedSampler),Local124));
	MaterialFloat Local243 = MaterialStoreTexSample(Parameters, Local242, 11);
	MaterialFloat Local244 = MaterialStoreTexCoordScale(Parameters, Local129, 11);
	MaterialFloat4 Local245 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_8,GetMaterialSharedSampler(samplerMaterial_Texture2D_8,View_MaterialTextureBilinearWrapedSampler),Local129));
	MaterialFloat Local246 = MaterialStoreTexSample(Parameters, Local245, 11);
	MaterialFloat3 Local247 = lerp(Local242.rgb,Local245.rgb,Local96.r.r);
	MaterialFloat Local248 = MaterialStoreTexCoordScale(Parameters, Local135, 11);
	MaterialFloat4 Local249 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_8,GetMaterialSharedSampler(samplerMaterial_Texture2D_8,View_MaterialTextureBilinearWrapedSampler),Local135));
	MaterialFloat Local250 = MaterialStoreTexSample(Parameters, Local249, 11);
	MaterialFloat3 Local251 = lerp(Local247,Local249.rgb,Local105.r.r);
	MaterialFloat3 Local252 = lerp(((MaterialFloat3)Local240),Local251,Local208);
	MaterialFloat3 Local253 = (((MaterialFloat3)Local184) + Local161);
	MaterialFloat3 Local254 = saturate(Local253);
	MaterialFloat3 Local255 = (Local254 + ((MaterialFloat3)Local208));
	MaterialFloat3 Local256 = saturate(Local255);
	MaterialFloat Local257 = MaterialStoreTexCoordScale(Parameters, Local124, 7);
	MaterialFloat4 Local258 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_9,GetMaterialSharedSampler(samplerMaterial_Texture2D_9,View_MaterialTextureBilinearWrapedSampler),Local124));
	MaterialFloat Local259 = MaterialStoreTexSample(Parameters, Local258, 7);
	MaterialFloat Local260 = MaterialStoreTexCoordScale(Parameters, Local129, 7);
	MaterialFloat4 Local261 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_9,GetMaterialSharedSampler(samplerMaterial_Texture2D_9,View_MaterialTextureBilinearWrapedSampler),Local129));
	MaterialFloat Local262 = MaterialStoreTexSample(Parameters, Local261, 7);
	MaterialFloat3 Local263 = lerp(Local258.rgb,Local261.rgb,Local96.r.r);
	MaterialFloat Local264 = MaterialStoreTexCoordScale(Parameters, Local135, 7);
	MaterialFloat4 Local265 = ProcessMaterialLinearColorTextureLookup(Texture2DSample(Material_Texture2D_9,GetMaterialSharedSampler(samplerMaterial_Texture2D_9,View_MaterialTextureBilinearWrapedSampler),Local135));
	MaterialFloat Local266 = MaterialStoreTexSample(Parameters, Local265, 7);
	MaterialFloat3 Local267 = lerp(Local263,Local265.rgb,Local105.r.r);
	MaterialFloat3 Local268 = lerp(((MaterialFloat3)1.00000000),Local267,Local208);
	MaterialFloat Local269 = dot(WorldNormalCopy,Parameters.CameraVector);
	MaterialFloat Local270 = max(0.00000000,Local269);
	MaterialFloat Local271 = (1.00000000 - Local270);
	MaterialFloat Local272 = abs(Local271);
	MaterialFloat Local273 = max(Local272,0.00010000);
	MaterialFloat Local274 = PositiveClampedPow(Local273,Local76.x);
	MaterialFloat Local275 = (Local274 * (1.00000000 - 0.04000000));
	MaterialFloat Local276 = (Local275 + 0.04000000);
	MaterialFloat Local277 = lerp(1.00000000,Material.PreshaderBuffer[17].y,Local276);
	MaterialFloat Local278 = lerp(1.00000000,Local277,Local256.x);

	PixelMaterialInputs.EmissiveColor = Local77;
	PixelMaterialInputs.Opacity = Local256;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local239;
	PixelMaterialInputs.Metallic = 0.00000000;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local252;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local76;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local268;
	PixelMaterialInputs.Refraction = MaterialFloat3(MaterialFloat2(Local278,0.0f),Material.PreshaderBuffer[17].z);
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
	//if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}