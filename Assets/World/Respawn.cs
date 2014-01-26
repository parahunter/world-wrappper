using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour 
{
	public bool useOriginalPosition = false;
	public float yRespawnThreshold = 70;
	public float yWrapThreshold = -5;
	Vector3 originalPos;
	public float offset = 150;

	public bool useOffset = true;

	void Start()
	{
		originalPos = transform.position;
	}


	// Update is called once per frame
	void Update () 
	{
		float y = transform.position.y;
		if(!WrapController.instance.isWrapping && !WrapController.instance.wrapped && y < yRespawnThreshold)
		{
			if(gameObject.tag == "Player")
				GetComponent<PlayerMoveScript>().enabled = false;
		}

		if(!WrapController.instance.isWrapping && !WrapController.instance.wrapped && y < yWrapThreshold)
			DoRespawn();
	}

	void DoRespawn()
	{
		if(useOriginalPosition)
			transform.position = originalPos;

		if(useOffset)
			transform.position -= transform.up * offset;

		if(gameObject.tag == "Player")
			GetComponent<PlayerMoveScript>().enabled = true;

		WrapController.instance.DoWrap();
	}

//	IEnumerator RespawnAnim(Anim)
//	{
//
//	}
}
