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
	Material.PreshaderBuffer[0] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(0.033333,0.033333,0.033333,0.000000);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.062500,0.062500,0.062500,0.429409);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.120833,0.120833,0.120833,0.339071);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.121600,0.136519,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(256.000000,256.000000,256.000000,0.344067);//(Unknown)
	Material.PreshaderBuffer[6] = float4(15.780495,512.000000,512.000000,512.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(0.468310,0.128644,0.359248,0.000000);//(Unknown)
	Material.PreshaderBuffer[8] = float4(0.033333,0.033333,0.033333,0.161109);//(Unknown)
	Material.PreshaderBuffer[9] = float4(0.027891,256.000000,256.000000,256.000000);//(Unknown)
	Material.PreshaderBuffer[10] = float4(0.191667,0.178090,0.178158,0.262315);//(Unknown)
	Material.PreshaderBuffer[11] = float4(0.492800,0.230400,9.358771,-8.358771);//(Unknown)
	Material.PreshaderBuffer[12] = float4(512.000000,256.000000,256.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[13] = float4(844.799988,422.399994,422.399994,0.965021);//(Unknown)
	Material.PreshaderBuffer[14] = float4(1.891828,-0.891828,0.286523,0.000000);//(Unknown)
	Material.PreshaderBuffer[15] = float4(0.175000,0.175000,0.175000,0.000000);//(Unknown)
	Material.PreshaderBuffer[16] = float4(2.784022,2.515401,0.040551,0.800000);//(Unknown)
	Material.PreshaderBuffer[17] = float4(0.371564,0.750000,0.000000,0.000000);//(Unknown)
}
MaterialFloat3 CustomExpression1(FMaterialPixelParameters Parameters)
{
return GetPrimitiveData(Parameters).LocalObjectBoundsMin.xyz;
}

MaterialFloat3 CustomExpression0(FMaterialPixelParameters Parameters)
{
return GetPrimitiveData(Parameters).LocalObjectBoundsMax.xyz;
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
	MaterialFloat2 Local0 = Parameters.TexCoords[0].xy;
	MaterialFloat Local1 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 0);
	MaterialFloat4 Local2 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local3 = MaterialStoreTexSample(Parameters, Local2, 0);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local2.rgb;


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
	MaterialFloat3 Local4 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[0].yzw,Material.PreshaderBuffer[0].x);
	MaterialFloat Local5 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 3);
	MaterialFloat4 Local6 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local7 = MaterialStoreTexSample(Parameters, Local6, 3);
	MaterialFloat3 Local8 = (Local6.rgb * Material.PreshaderBuffer[1].xyz);
	MaterialFloat3 Local9 = (Local6.rgb * Material.PreshaderBuffer[2].xyz);
	MaterialFloat4 Local10 = Parameters.VertexColor;
	MaterialFloat Local11 = DERIV_BASE_VALUE(Local10).r;
	MaterialFloat3 Local12 = abs(Parameters.TangentToWorld[2]);
	MaterialFloat3 Local13 = PositiveClampedPow(Local12,((MaterialFloat3)2.00000000));
	MaterialFloat Local14 = dot(Local13,((MaterialFloat3)Material.PreshaderBuffer[2].w));
	MaterialFloat Local15 = (Local14 - 0.50000000);
	MaterialFloat Local16 = abs(Local15);
	MaterialFloat Local17 = (Local16 * 2.00000000);
	MaterialFloat Local18 = (DERIV_BASE_VALUE(Local11) * Local17);
	MaterialFloat Local19 = (Local18 + DERIV_BASE_VALUE(Local11));
	MaterialFloat Local20 = lerp(DERIV_BASE_VALUE(Local11),Local19,1.00000000);
	MaterialFloat3 Local21 = lerp(Local8,Local9,Local20);
	MaterialFloat3 Local22 = (Local6.rgb * Material.PreshaderBuffer[3].xyz);
	MaterialFloat Local23 = (Local17 - Material.PreshaderBuffer[3].w);
	MaterialFloat Local24 = abs(Local23);
	MaterialFloat Local25 = (Local24 * 2.00000000);
	MaterialFloat Local26 = (1.00000000 - Local25);
	MaterialFloat Local27 = saturate(Local26);
	MaterialFloat Local28 = (DERIV_BASE_VALUE(Local11) * Material.PreshaderBuffer[4].x);
	MaterialFloat Local29 = (Local27 + DERIV_BASE_VALUE(Local28));
	MaterialFloat Local30 = (Local29 + Material.PreshaderBuffer[4].y);
	FWSVector3 Local31 = GetWorldPosition(Parameters);
	FWSVector3 Local32 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local31)), WSGetY(DERIV_BASE_VALUE(Local31)), WSGetZ(DERIV_BASE_VALUE(Local31)));
	FWSVector3 Local33 = WSSubtract(DERIV_BASE_VALUE(Local32), GetObjectWorldPosition(Parameters));
	MaterialFloat3 Local34 = WSDemote(DERIV_BASE_VALUE(Local33));
	MaterialFloat3 Local35 = WSMultiplyVector(DERIV_BASE_VALUE(Local34), GetWorldToLocal(Parameters));
	MaterialFloat3 Local36 = WSMultiplyVector(MaterialFloat3(1.00000000,0.00000000,0.00000000).rgb, GetInstanceToWorld(Parameters));
	MaterialFloat3 Local37 = (((MaterialFloat3)0.00000000) - Local36);
	MaterialFloat Local38 = length(Local37);
	MaterialFloat3 Local39 = WSMultiplyVector(MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb, GetInstanceToWorld(Parameters));
	MaterialFloat3 Local40 = (((MaterialFloat3)0.00000000) - Local39);
	MaterialFloat Local41 = length(Local40);
	MaterialFloat2 Local42 = MaterialFloat2(DERIV_BASE_VALUE(Local38),DERIV_BASE_VALUE(Local41));
	MaterialFloat3 Local43 = WSMultiplyVector(MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb, GetInstanceToWorld(Parameters));
	MaterialFloat3 Local44 = (((MaterialFloat3)0.00000000) - Local43);
	MaterialFloat Local45 = length(Local44);
	MaterialFloat3 Local46 = MaterialFloat3(DERIV_BASE_VALUE(Local42),DERIV_BASE_VALUE(Local45));
	MaterialFloat3 Local47 = (DERIV_BASE_VALUE(Local35) * DERIV_BASE_VALUE(Local46));
	MaterialFloat3 Local48 = (DERIV_BASE_VALUE(Local47) / ((MaterialFloat3)Material.PreshaderBuffer[5].xyz.x));
	MaterialFloat3 Local49 = (DERIV_BASE_VALUE(Local48) * ((MaterialFloat3)-1.00000000));
	MaterialFloat2 Local50 = DERIV_BASE_VALUE(Local49).gb;
	MaterialFloat3 Local51 = WSMultiplyVector(Parameters.TangentToWorld[2], GetWorldToLocal(Parameters));
	MaterialFloat3 Local52 = sign(Local51);
	MaterialFloat3 Local53 = (Local52 * MaterialFloat3(1.00000000,-1.00000000,1.00000000).rgb);
	MaterialFloat2 Local54 = (DERIV_BASE_VALUE(Local50) * MaterialFloat2(Local53.r,1.00000000));
	MaterialFloat Local55 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local54), 5);
	MaterialFloat4 Local56 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local54),View.MaterialTextureMipBias));
	MaterialFloat Local57 = MaterialStoreTexSample(Parameters, Local56, 5);
	MaterialFloat3 Local58 = normalize(Local51);
	MaterialFloat3 Local59 = abs(Local58);
	MaterialFloat3 Local60 = PositiveClampedPow(Local59,((MaterialFloat3)8.00000000));
	MaterialFloat Local61 = dot(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb,Local60);
	MaterialFloat3 Local62 = (Local60 / ((MaterialFloat3)Local61));
	MaterialFloat3 Local63 = (Local56.rgb * ((MaterialFloat3)Local62.r));
	MaterialFloat2 Local64 = DERIV_BASE_VALUE(Local49).rb;
	MaterialFloat2 Local65 = (DERIV_BASE_VALUE(Local64) * MaterialFloat2(Local53.g,1.00000000));
	MaterialFloat Local66 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local65), 5);
	MaterialFloat4 Local67 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local65),View.MaterialTextureMipBias));
	MaterialFloat Local68 = MaterialStoreTexSample(Parameters, Local67, 5);
	MaterialFloat3 Local69 = (Local67.rgb * ((MaterialFloat3)Local62.g));
	MaterialFloat3 Local70 = (Local63 + Local69);
	MaterialFloat2 Local71 = DERIV_BASE_VALUE(Local49).rg;
	MaterialFloat2 Local72 = (DERIV_BASE_VALUE(Local71) * MaterialFloat2(Local53.b,1.00000000));
	MaterialFloat Local73 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local72), 5);
	MaterialFloat4 Local74 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local72),View.MaterialTextureMipBias));
	MaterialFloat Local75 = MaterialStoreTexSample(Parameters, Local74, 5);
	MaterialFloat3 Local76 = (Local74.rgb * ((MaterialFloat3)Local62.b));
	MaterialFloat3 Local77 = (Local70 + Local76);
	MaterialFloat3 Local78 = (Local77 * ((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat3 Local79 = (((MaterialFloat3)Local30) - Local78);
	MaterialFloat3 Local80 = PositiveClampedPow(Local79,((MaterialFloat3)Material.PreshaderBuffer[6].x));
	MaterialFloat3 Local81 = saturate(Local80);
	MaterialFloat3 Local82 = lerp(Local21,Local22,Local81);
	MaterialFloat3 Local83 = (DERIV_BASE_VALUE(Local47) / ((MaterialFloat3)Material.PreshaderBuffer[6].yzw.x));
	MaterialFloat3 Local84 = (DERIV_BASE_VALUE(Local83) * ((MaterialFloat3)-1.00000000));
	MaterialFloat2 Local85 = DERIV_BASE_VALUE(Local84).gb;
	MaterialFloat2 Local86 = (DERIV_BASE_VALUE(Local85) * MaterialFloat2(Local53.r,1.00000000));
	MaterialFloat Local87 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local86), 10);
	MaterialFloat4 Local88 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local86),View.MaterialTextureMipBias));
	MaterialFloat Local89 = MaterialStoreTexSample(Parameters, Local88, 10);
	MaterialFloat3 Local90 = (Local88.rgb * ((MaterialFloat3)Local62.r));
	MaterialFloat2 Local91 = DERIV_BASE_VALUE(Local84).rb;
	MaterialFloat2 Local92 = (DERIV_BASE_VALUE(Local91) * MaterialFloat2(Local53.g,1.00000000));
	MaterialFloat Local93 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local92), 10);
	MaterialFloat4 Local94 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local92),View.MaterialTextureMipBias));
	MaterialFloat Local95 = MaterialStoreTexSample(Parameters, Local94, 10);
	MaterialFloat3 Local96 = (Local94.rgb * ((MaterialFloat3)Local62.g));
	MaterialFloat3 Local97 = (Local90 + Local96);
	MaterialFloat2 Local98 = DERIV_BASE_VALUE(Local84).rg;
	MaterialFloat2 Local99 = (DERIV_BASE_VALUE(Local98) * MaterialFloat2(Local53.b,1.00000000));
	MaterialFloat Local100 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local99), 10);
	MaterialFloat4 Local101 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local99),View.MaterialTextureMipBias));
	MaterialFloat Local102 = MaterialStoreTexSample(Parameters, Local101, 10);
	MaterialFloat3 Local103 = (Local101.rgb * ((MaterialFloat3)Local62.b));
	MaterialFloat3 Local104 = (Local97 + Local103);
	MaterialFloat3 Local105 = (Local77 * ((MaterialFloat3)Material.PreshaderBuffer[7].x));
	MaterialFloat3 Local106 = (((MaterialFloat3)Local20) - Local105);
	MaterialFloat3 Local107 = (Local106 - ((MaterialFloat3)0.50000000));
	MaterialFloat Local108 = length(Local107);
	MaterialFloat Local109 = (1.00000000 - Local108);
	MaterialFloat Local110 = (Local109 * 2.00000000);
	MaterialFloat Local111 = (Material.PreshaderBuffer[7].y * Local110);
	MaterialFloat3 Local112 = (Local104 - ((MaterialFloat3)Local111));
	MaterialFloat Local113 = (WorldNormalCopy.b - 0.00000000);
	MaterialFloat Local114 = abs(Local113);
	MaterialFloat Local115 = (1.00000000 - Local114);
	MaterialFloat Local116 = saturate(Local115);
	MaterialFloat3 Local117 = (Local112 * ((MaterialFloat3)Local116));
	MaterialFloat3 Local118 = (Local117 * ((MaterialFloat3)Material.PreshaderBuffer[7].z));
	MaterialFloat3 Local119 = saturate(Local118);
	MaterialFloat3 Local120 = lerp(Local82,Material.PreshaderBuffer[8].xyz,Local119);
	MaterialFloat Local121 = (WorldNormalCopy.b + 1.00000000);
	MaterialFloat Local122 = saturate(Local121);
	MaterialFloat Local123 = (1.00000000 - Local122);
	MaterialFloat Local124 = (Local123 * Material.PreshaderBuffer[8].w);
	MaterialFloat Local125 = (1.00000000 - Local20);
	MaterialFloat Local126 = (Local125 * Material.PreshaderBuffer[9].x);
	MaterialFloat Local127 = saturate(Local126);
	MaterialFloat Local128 = (Local124 + Local127);
	MaterialFloat3 Local129 = (DERIV_BASE_VALUE(Local47) / ((MaterialFloat3)Material.PreshaderBuffer[9].yzw.x));
	MaterialFloat3 Local130 = (DERIV_BASE_VALUE(Local129) * ((MaterialFloat3)-1.00000000));
	MaterialFloat2 Local131 = DERIV_BASE_VALUE(Local130).gb;
	MaterialFloat2 Local132 = (DERIV_BASE_VALUE(Local131) * MaterialFloat2(Local53.r,1.00000000));
	MaterialFloat Local133 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local132), 5);
	MaterialFloat4 Local134 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local132),View.MaterialTextureMipBias));
	MaterialFloat Local135 = MaterialStoreTexSample(Parameters, Local134, 5);
	MaterialFloat3 Local136 = (Local134.rgb * ((MaterialFloat3)Local62.r));
	MaterialFloat2 Local137 = DERIV_BASE_VALUE(Local130).rb;
	MaterialFloat2 Local138 = (DERIV_BASE_VALUE(Local137) * MaterialFloat2(Local53.g,1.00000000));
	MaterialFloat Local139 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local138), 5);
	MaterialFloat4 Local140 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local138),View.MaterialTextureMipBias));
	MaterialFloat Local141 = MaterialStoreTexSample(Parameters, Local140, 5);
	MaterialFloat3 Local142 = (Local140.rgb * ((MaterialFloat3)Local62.g));
	MaterialFloat3 Local143 = (Local136 + Local142);
	MaterialFloat2 Local144 = DERIV_BASE_VALUE(Local130).rg;
	MaterialFloat2 Local145 = (DERIV_BASE_VALUE(Local144) * MaterialFloat2(Local53.b,1.00000000));
	MaterialFloat Local146 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local145), 5);
	MaterialFloat4 Local147 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local145),View.MaterialTextureMipBias));
	MaterialFloat Local148 = MaterialStoreTexSample(Parameters, Local147, 5);
	MaterialFloat3 Local149 = (Local147.rgb * ((MaterialFloat3)Local62.b));
	MaterialFloat3 Local150 = (Local143 + Local149);
	MaterialFloat3 Local151 = (((MaterialFloat3)Local128) * Local150);
	MaterialFloat3 Local152 = lerp(Local120,Material.PreshaderBuffer[10].xyz,Local151);
	MaterialFloat3 Local153 = WSMultiplyDemote(DERIV_BASE_VALUE(Local32), GetWorldToLocal(Parameters));
	MaterialFloat3 Local154 = WSMultiplyDemote(GetObjectWorldPosition(Parameters), GetWorldToLocal(Parameters));
	MaterialFloat3 Local155 = (DERIV_BASE_VALUE(Local153) - Local154);
	MaterialFloat3 Local156 = CustomExpression0(Parameters);
	MaterialFloat3 Local157 = CustomExpression1(Parameters);
	MaterialFloat3 Local158 = (Local156 - Local157);
	MaterialFloat3 Local159 = (DERIV_BASE_VALUE(Local155) / Local158);
	MaterialFloat3 Local160 = (Local159 + ((MaterialFloat3)0.50000000));
	MaterialFloat Local161 = (1.00000000 - Local160.b);
	MaterialFloat Local162 = (Local161 * Material.PreshaderBuffer[10].w);
	MaterialFloat3 Local163 = (Local106 * ((MaterialFloat3)Material.PreshaderBuffer[11].x));
	MaterialFloat3 Local164 = (((MaterialFloat3)Local162) - Local163);
	MaterialFloat3 Local165 = (Local164 * Local150);
	MaterialFloat3 Local166 = lerp(Local164,Local165,Material.PreshaderBuffer[11].y);
	MaterialFloat Local167 = lerp(Material.PreshaderBuffer[11].w,Material.PreshaderBuffer[11].z,Local166.x);
	MaterialFloat Local168 = saturate(Local167);
	MaterialFloat Local169 = saturate(Local168.r);
	MaterialFloat3 Local170 = lerp(Local152,Material.PreshaderBuffer[10].xyz,Local169);
	MaterialFloat3 Local171 = (DERIV_BASE_VALUE(Local47) / ((MaterialFloat3)Material.PreshaderBuffer[12].xyz.x));
	MaterialFloat3 Local172 = (DERIV_BASE_VALUE(Local171) * ((MaterialFloat3)-1.00000000));
	MaterialFloat2 Local173 = DERIV_BASE_VALUE(Local172).gb;
	MaterialFloat2 Local174 = (DERIV_BASE_VALUE(Local173) * MaterialFloat2(Local53.r,1.00000000));
	MaterialFloat Local175 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local174), 9);
	MaterialFloat4 Local176 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local174),View.MaterialTextureMipBias));
	MaterialFloat Local177 = MaterialStoreTexSample(Parameters, Local176, 9);
	MaterialFloat3 Local178 = (Local176.rgb * ((MaterialFloat3)Local62.r));
	MaterialFloat2 Local179 = DERIV_BASE_VALUE(Local172).rb;
	MaterialFloat2 Local180 = (DERIV_BASE_VALUE(Local179) * MaterialFloat2(Local53.g,1.00000000));
	MaterialFloat Local181 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local180), 9);
	MaterialFloat4 Local182 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local180),View.MaterialTextureMipBias));
	MaterialFloat Local183 = MaterialStoreTexSample(Parameters, Local182, 9);
	MaterialFloat3 Local184 = (Local182.rgb * ((MaterialFloat3)Local62.g));
	MaterialFloat3 Local185 = (Local178 + Local184);
	MaterialFloat2 Local186 = DERIV_BASE_VALUE(Local172).rg;
	MaterialFloat2 Local187 = (DERIV_BASE_VALUE(Local186) * MaterialFloat2(Local53.b,1.00000000));
	MaterialFloat Local188 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local187), 9);
	MaterialFloat4 Local189 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local187),View.MaterialTextureMipBias));
	MaterialFloat Local190 = MaterialStoreTexSample(Parameters, Local189, 9);
	MaterialFloat3 Local191 = (Local189.rgb * ((MaterialFloat3)Local62.b));
	MaterialFloat3 Local192 = (Local185 + Local191);
	MaterialFloat3 Local193 = (DERIV_BASE_VALUE(Local47) / ((MaterialFloat3)Material.PreshaderBuffer[13].xyz.x));
	MaterialFloat3 Local194 = (DERIV_BASE_VALUE(Local193) * ((MaterialFloat3)-1.00000000));
	MaterialFloat2 Local195 = DERIV_BASE_VALUE(Local194).gb;
	MaterialFloat2 Local196 = (DERIV_BASE_VALUE(Local195) * MaterialFloat2(Local53.r,1.00000000));
	MaterialFloat Local197 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local196), 9);
	MaterialFloat4 Local198 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local196),View.MaterialTextureMipBias));
	MaterialFloat Local199 = MaterialStoreTexSample(Parameters, Local198, 9);
	MaterialFloat3 Local200 = (Local198.rgb * ((MaterialFloat3)Local62.r));
	MaterialFloat2 Local201 = DERIV_BASE_VALUE(Local194).rb;
	MaterialFloat2 Local202 = (DERIV_BASE_VALUE(Local201) * MaterialFloat2(Local53.g,1.00000000));
	MaterialFloat Local203 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local202), 9);
	MaterialFloat4 Local204 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local202),View.MaterialTextureMipBias));
	MaterialFloat Local205 = MaterialStoreTexSample(Parameters, Local204, 9);
	MaterialFloat3 Local206 = (Local204.rgb * ((MaterialFloat3)Local62.g));
	MaterialFloat3 Local207 = (Local200 + Local206);
	MaterialFloat2 Local208 = DERIV_BASE_VALUE(Local194).rg;
	MaterialFloat2 Local209 = (DERIV_BASE_VALUE(Local208) * MaterialFloat2(Local53.b,1.00000000));
	MaterialFloat Local210 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local209), 9);
	MaterialFloat4 Local211 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local209),View.MaterialTextureMipBias));
	MaterialFloat Local212 = MaterialStoreTexSample(Parameters, Local211, 9);
	MaterialFloat3 Local213 = (Local211.rgb * ((MaterialFloat3)Local62.b));
	MaterialFloat3 Local214 = (Local207 + Local213);
	MaterialFloat3 Local215 = (Local192 * Local214);
	MaterialFloat3 Local216 = (Local215 * ((MaterialFloat3)WorldNormalCopy.b));
	MaterialFloat3 Local217 = (((MaterialFloat3)Material.PreshaderBuffer[13].w) * Local216);
	MaterialFloat Local218 = lerp(Material.PreshaderBuffer[14].y,Material.PreshaderBuffer[14].x,Local217.x);
	MaterialFloat Local219 = saturate(Local218);
	MaterialFloat3 Local220 = (((MaterialFloat3)Local219.r) - Local150);
	MaterialFloat3 Local221 = (Local220 - ((MaterialFloat3)1.00000000));
	MaterialFloat Local222 = (WorldNormalCopy.b * Material.PreshaderBuffer[14].z);
	MaterialFloat3 Local223 = (Local221 + ((MaterialFloat3)Local222));
	MaterialFloat3 Local224 = (Local223 - ((MaterialFloat3)0.00000000));
	MaterialFloat3 Local225 = saturate(Local224);
	MaterialFloat3 Local226 = lerp(Local170,Material.PreshaderBuffer[15].xyz,Local225);
	MaterialFloat Local227 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 2);
	MaterialFloat4 Local228 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_6,samplerMaterial_Texture2D_6,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local229 = MaterialStoreTexSample(Parameters, Local228, 2);
	MaterialFloat Local230 = max(Local228.r,Material.PreshaderBuffer[15].w);
	MaterialFloat Local231 = min(Local230,1.00000000);
	MaterialFloat3 Local232 = (((MaterialFloat3)Local231) - Local81);
	MaterialFloat3 Local233 = (Local232 - Local119);
	MaterialFloat3 Local234 = (Local233 - Local151);
	MaterialFloat3 Local235 = (Local234 - ((MaterialFloat3)Local169));
	MaterialFloat3 Local236 = (Local235 - Local225);
	MaterialFloat Local237 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 4);
	MaterialFloat4 Local238 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_7,samplerMaterial_Texture2D_7,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local239 = MaterialStoreTexSample(Parameters, Local238, 4);
	MaterialFloat Local240 = (Local238.r * Material.PreshaderBuffer[16].x);
	MaterialFloat Local241 = (Local238.r * Material.PreshaderBuffer[16].y);
	MaterialFloat Local242 = lerp(Local240,Local241,Local106.x);
	MaterialFloat Local243 = lerp(Local242,Material.PreshaderBuffer[16].z,Local106.x);
	MaterialFloat Local244 = lerp(Local242,Local243,Local81.x);
	MaterialFloat Local245 = lerp(Local244,Material.PreshaderBuffer[16].w,Local119.x);
	MaterialFloat Local246 = lerp(Local245,Material.PreshaderBuffer[17].x,Local151.x);
	MaterialFloat Local247 = lerp(Local246,Material.PreshaderBuffer[17].x,Local169);
	MaterialFloat Local248 = lerp(Local247,Material.PreshaderBuffer[17].y,Local225.x);
	MaterialFloat Local249 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 1);
	MaterialFloat4 Local250 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_8,samplerMaterial_Texture2D_8,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local251 = MaterialStoreTexSample(Parameters, Local250, 1);

	PixelMaterialInputs.EmissiveColor = Local4;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local226;
	PixelMaterialInputs.Metallic = Local236;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local248;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local2.rgb;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local250.r;
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
	//if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}