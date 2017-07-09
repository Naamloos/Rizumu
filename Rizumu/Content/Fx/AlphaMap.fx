// Screen Space Static Postprocessor 
// 
// Produces an analogue noise overlay similar to a film grain / TV static 
// 
// Original implementation and noise algorithm 
// Pat 'Hawthorne' Shearon 
// 
// Optimized scanlines + noise version with intensity scaling 
// Georg 'Leviathan' Steinröhder 
// 
// This version is provided under a Creative Commons Attribution 3.0 License 
// http://creativecommons.org/licenses/by/3.0/  
//  

//Global Variables 
float Timer : TIME;

//noise effect intensity value (0 = no effect, 1 = full effect) 
float fNintensity = 0.5;
//scanlines effect intensity value (0 = no effect, 1 = full effect) 
float fSintensity = 0.1;
//scanlines effect count value (0 = no effect, 4096 = full effect) 
float fScount = 2048;
#define RGB 
//#define MONOCHROME 

texture texScreen;
sampler2D sampScreen = sampler_state
{
	Texture = <texScreen>;
	MinFilter = Point;
	MagFilter = Point;
	AddressU = Wrap;
	AddressV = Wrap;
};

// Pixel Shader Output 
float4 ps_main(float2 Tex : TEXCOORD0) : COLOR0
{
	// sample the source 
	float4 cTextureScreen = tex2D(sampScreen, Tex.xy);

	// make some noise 
	float x = Tex.x * Tex.y * Timer * 1000;
	x = fmod(x, 13) * fmod(x, 123);
	float dx = fmod(x, 0.01);

	// add noise 
	float3 cResult = cTextureScreen.rgb + cTextureScreen.rgb * saturate(0.1f + dx.xxx * 100);

	// get us a sine and cosine 
	float2 sc; sincos(Tex.y * fScount, sc.x, sc.y);

	// add scanlines 
	cResult += cTextureScreen.rgb * float3(sc.x, sc.y, sc.x) * fSintensity;

	// interpolate between source and result by intensity 
	cResult = lerp(cTextureScreen, cResult, saturate(fNintensity));

	// convert to grayscale if desired 
#ifdef MONOCHROME 
	cResult.rgb = dot(cResult.rgb, float3(0.3, 0.59, 0.11));
#endif 

	// return with source alpha 
	return float4(cResult, cTextureScreen.a);
}

//Technique Calls 
technique PostProcess
{
	pass Pass_0
	{
		PixelShader = compile ps_4_0_level_9_1 ps_main();
	}
}