using UnityEngine;
using System.Collections;

public class SkyPositioner : MonoBehaviour 
{

	Vector3 flatPosition = new Vector3(180,70,200);
	Vector3 roundPoisition = new Vector3(0,0,200);

	// Use this for initialization
	void Start () {
	
	}

	void Update () 
	{
		gameObject.transform.position = Vector3.Lerp (flatPosition, roundPoisition, WrapController.instance.wrapFactor);
	}
}
