using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : UsableObject 
{
	public float openingHeight;
	public float openingTime;

	GameObject opening;
	Vector3 openingInitialPosition = new Vector3();
	bool isOpening = false;
	float openingRatio = 0.0f;

	// Use this for initialization
	void Start () 
	{
		opening = gameObject.transform.FindChild ("Opening").gameObject;
		openingInitialPosition = opening.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isOpening)
		{
			openingRatio += Time.deltaTime / openingTime;
			if (openingRatio >= 1.0f)
			{
				openingRatio = 1.0f;
				isOpening = false;
			}
			opening.transform.position = openingInitialPosition + Vector3.up * (openingHeight * openingRatio);
		}
	}

	public override void Use()
	{
		if (openingRatio == 0.0f)
			isOpening = true;
	}
}
