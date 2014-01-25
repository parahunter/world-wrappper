using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazorUse : MonoBehaviour 
{
	bool lightActive = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter()
	{
		if (!lightActive)
		{
			GameObject light = gameObject.transform.parent.FindChild ("Light").gameObject;
			light.SetActive (true);
			lightActive = true;
		}
	}

	void OnTriggerExit()
	{
		if (lightActive)
		{
			GameObject light = gameObject.transform.parent.FindChild ("Light").gameObject;
			light.SetActive (false);
			lightActive = false;
		}
	}
}
