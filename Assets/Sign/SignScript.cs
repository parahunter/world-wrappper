using UnityEngine;
using System.Collections;

public class SignScript : MonoBehaviour {

	public float bubbleHeight;
	public Texture2D bubbleTexture;
	public float bubbleHeightRatio;
	public float bubbleAspect;

	private float bubbleTexWidth = 0.0f;
	private float bubbleTexHeight = 0.0f;
	private bool guiDraw = false;

	// Use this for initialization
	void Start () {
		bubbleTexHeight = Screen.height * bubbleHeightRatio;
		bubbleTexWidth = bubbleTexHeight * bubbleAspect;
	}

	void OnGUI()
	{
		if (guiDraw && !WrapController.instance.isWrapping) 
		{
			Vector3 worldPos = transform.position + transform.up * bubbleHeight;

			Vector3 bubblePos = Camera.main.WorldToScreenPoint(worldPos);
			bubblePos.x -= bubbleTexWidth * 0.5f;
			bubblePos.y += bubbleTexHeight ;
			//Rect bubbleRect = new Rect(0, 0, bubbleTexWidth, bubbleTexHeight);
			
			Rect bubbleRect = new Rect(bubblePos.x,Screen.height - bubblePos.y, bubbleTexWidth, bubbleTexHeight);
			GUI.DrawTexture(bubbleRect, bubbleTexture, ScaleMode.ScaleToFit, true);

		}
	}

	void OnTriggerEnter()
	{
		guiDraw = true;
	}

	void OnTriggerExit()
	{
		guiDraw = false;
	}
}
