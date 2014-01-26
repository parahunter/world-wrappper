using UnityEngine;
using System.Collections;

public class SignScript : MonoBehaviour {

	public float bubbleHeight;
	public Texture bubbleTexture;
	public static 

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter()
	{
		if (!WrapController.instance.isWrapping) {
			Vector3 worldPos = (rigidbody.transform.up * bubbleHeight) - new Vector3(-0.5f * bubbleTexture.width, 0.0f, 0.0f);
			Vector3 bubblePos = Camera.main.WorldToScreenPoint(worldPos);

			Debug.Log (bubblePos);
			 //bubbleTexture
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
