using UnityEngine;
using System.Collections;

public class SkyGenerator : MonoBehaviour {

	public Material material;
	
	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;

	public Gradient flatGradient;
	public Gradient roundGradient;

	public int numStars;
	public float starThreshold;
	public float starProbabilityBase;
	public float starProbabilityHeightModifier;
	public Color starColor;
	
	private Texture2D flatTex;
	private Texture2D roundTex;
	private Texture2D lerpTex;
	private Color[] pix; 

	// Use this for initialization
	void Start () {
		// Set up the texture and a Color array to hold pixels during processing.
		flatTex = new Texture2D(pixWidth, pixHeight);
		roundTex = new Texture2D(pixWidth, pixHeight);
		lerpTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[flatTex.width * flatTex.height];
		CalcFlatTexture ();
		CalcRoundTexture ();
	}
	
	// Update is called once per frame
	void Update () {
		if (WrapController.instance.wrapFactor == 0.0f)
			material.mainTexture = flatTex;
		else if (WrapController.instance.wrapFactor == 1.0f)
			material.mainTexture = roundTex;
		else
		{
			CalcLerpTexture();
			material.mainTexture = lerpTex;
		}
	}

	void CalculateFlatColor(ref Color flatColor, int y)
	{
		float gradient = 1.0f - (float)y / flatTex.height;
		flatColor = flatGradient.Evaluate (gradient);
		//flatColor = Color.Lerp(flatColor1, flatColor2, gradient);
	}

	void CalculateRoundColor(ref Color roundColor, int x, int y)
	{
		Vector2 center = new Vector2 (roundTex.width / 2, roundTex.height / 2);
		float maxValue = center.magnitude;
		float gradient = Vector2.Distance(center, new Vector2(x,y)) / maxValue;
		
		if (gradient >  starThreshold && Random.value < (starProbabilityBase + starProbabilityHeightModifier * gradient))
		{
			roundColor = starColor;
		}
		else
		{
			//roundColor = Color.Lerp(roundColor1, roundColor2, gradient);
			roundColor = roundGradient.Evaluate(gradient);
		}
	}
	
	void CalcFlatTexture()
	{
		Color flatColor = new Color ();

		// For each pixel in the texture...
		for (int y = 0; y < flatTex.height; y++)
		{
			CalculateFlatColor(ref flatColor, y);

			for (int x = 0; x < flatTex.width; x++)
			{
				pix[y * flatTex.width + x] = flatColor; 
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		flatTex.SetPixels(pix);
		flatTex.Apply();
	}

	void CalcRoundTexture()
	{
		Color roundColor = new Color ();
		
		// For each pixel in the texture...
		for (int y = 0; y < roundTex.height; y++)
		{
			for (int x = 0; x < roundTex.width; x++)
			{
				CalculateRoundColor(ref roundColor, x, y);
				pix[y * roundTex.width + x] = roundColor; 
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		roundTex.SetPixels(pix);
		roundTex.Apply();
	}

	void CalcLerpTexture()
	{
		Color roundColor = new Color ();
		
		// For each pixel in the texture...
		for (int y = 0; y < flatTex.height; y++)
		{
			for (int x = 0; x < flatTex.width; x++)
			{
				pix[y * flatTex.width + x] = Color.Lerp(flatTex.GetPixel(x,y), roundTex.GetPixel(x,y), WrapController.instance.wrapFactor); 
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		lerpTex.SetPixels(pix);
		lerpTex.Apply();
	}
}
