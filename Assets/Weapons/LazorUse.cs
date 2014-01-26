using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazorUse : SignScript 
{
	bool lightActive = false;
	GameObject firePoint;
	GameObject lightObject;

	bool active = false;

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		lightObject = gameObject.transform.parent.FindChild ("Light").gameObject;
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.E))
			active = true;

		if(Input.GetKeyUp (KeyCode.E))
			active = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (guiDraw && active)
		{
			active = false;
			lightActive = !lightActive;
			lightObject.SetActive (lightActive);
		}
	}
}
