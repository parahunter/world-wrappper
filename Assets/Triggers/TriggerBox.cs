using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerBox : MonoBehaviour 
{
	public UsableObject triggeredObject;

	delegate void OnHitDelegate();
	OnHitDelegate onHit;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnTrigger()
	{
		if (onHit != null)
		{
			onHit();
		}
		if (triggeredObject != null)
		{
			triggeredObject.Use();
		}
	}
}
