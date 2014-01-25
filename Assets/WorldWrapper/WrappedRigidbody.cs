using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class WrappedRigidbody : MonoBehaviour 
{
	public float gravity = 9.98f;
	public ForceMode forcemode;
	public bool wrapped = false;

	public bool alignWithGravity = true;
	public float onWrapOffset = 0f;

	void Reset()
	{
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;;

	}

	void Start()
	{
		WrapController.instance.AddBody(this);
	}
		
	void FixedUpdate()
	{
		Vector3 direction;
		if(wrapped)
		{
			direction = -transform.position.normalized;

			rigidbody.AddForce(direction * gravity, forcemode);
		}
		else
		{
			direction = Vector3.down;
			
			rigidbody.AddForce(direction * gravity, forcemode);
		}

		if(alignWithGravity)
			transform.up = -direction;
	}

	public void Wrap()
	{
		transform.position = WorldWrapper.WrapPoint(transform.position);
		transform.Translate(transform.position.normalized * onWrapOffset);
		wrapped = true;
	}

	public void Unwrap()
	{
		transform.position = WorldWrapper.UnwrapPoint(transform.position);
		transform.Translate(Vector3.up * onWrapOffset);
		
		wrapped = false;
	}
}
