using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazorUse : MonoBehaviour 
{
	bool lightActive = false;
	GameObject firePoint;
	GameObject light;

	// Use this for initialization
	void Start () 
	{
		light = gameObject.transform.parent.FindChild ("Light").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter()
	{
		if (!lightActive)
		{
			light.SetActive (true);
			lightActive = true;
		}
	}

	void OnTriggerExit()
	{
		if (lightActive)
		{
			light.SetActive (false);
			lightActive = false;
		}
	}
}
