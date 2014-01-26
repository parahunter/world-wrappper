using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazorUse : SignScript 
{
	bool lightActive = false;
	GameObject firePoint;
	GameObject lightObject;

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		lightObject = gameObject.transform.parent.FindChild ("Light").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (guiDraw && Input.GetKeyDown (KeyCode.E))
		{
			lightActive = !lightActive;
			lightObject.SetActive (lightActive);
		}
	}
}
