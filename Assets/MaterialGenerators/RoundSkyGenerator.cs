using UnityEngine;
using System.Collections;

public class RoundSkyGenerator : MonoBehaviour {

	public Material material;
	
	// Width and height of the texture in pixels.
	public int pixWidth;
	public int pixHeight;
	
	public Color color1;
	public Color color2;

	public int numStars;
	public float starThreshold;
	public float starProbabilityBase;
	public float starProbabilityHeightModifier;
	public Color starColor;
	
	private Texture2D noiseTex;
	private Color[] pix; 

	// Use this for initialization
	void Start () {
		// Set up the texture and a Color array to hold pixels during processing.
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		material.mainTexture = noiseTex;
		CalcTexture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CalcTexture()
	{
		Color color = new Color ();
		
		Vector2 center = new Vector2 (noiseTex.width / 2, noiseTex.height / 2);
		float maxValue = center.magnitude;
		
		// For each pixel in the texture...
		for (int y = 0; y < noiseTex.height; y++)
		{
			for (int x = 0; x < noiseTex.width; x++)
			{
				float gradient = Vector2.Distance(center, new Vector2(x,y)) / maxValue;

				if (gradient >  starThreshold && Random.value < (starProbabilityBase + starProbabilityHeightModifier * gradient))
				{
					color = starColor;
				}
				else
				{
					color.r = Mathf.Lerp(color1.r, color2.r, gradient);
					color.g = Mathf.Lerp(color1.g, color2.g, gradient);
					color.b = Mathf.Lerp(color1.b, color2.b, gradient);
				}
				
				pix[y * noiseTex.width + x] = color;
			}
		}
		
		// Copy the pixel data to the texture and load it into the GPU.
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
}
