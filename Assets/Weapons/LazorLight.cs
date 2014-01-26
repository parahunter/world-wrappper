using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazorLight : MonoBehaviour 
{
	public float length = 20000.0f;
	GameObject firePoint;
	LineRenderer lineRendered;

	// Use this for initialization
	void Start () 
	{
		firePoint = gameObject.transform.parent.FindChild ("FirePoint").gameObject;
		lineRendered = gameObject.GetComponent<LineRenderer> ();
		lineRendered.SetPosition (0, firePoint.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		float lightDistance = length;
		RaycastHit hit;
		if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, length, 1<<8))
		{
			lightDistance = hit.distance;
			TriggerBox box = hit.transform.gameObject.GetComponent<TriggerBox>();
			if (box)
			{
				box.OnTrigger();
			}
		}

		lineRendered.SetPosition (0, firePoint.transform.position);
		lineRendered.SetPosition (1, firePoint.transform.position + firePoint.transform.forward * lightDistance);
	}
}
