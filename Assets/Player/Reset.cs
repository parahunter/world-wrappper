using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reset : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}
}
