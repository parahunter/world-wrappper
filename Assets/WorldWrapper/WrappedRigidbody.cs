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
		StartCoroutine(WrapAnimate());
	}

	IEnumerator WrapAnimate()
	{
		wrapped = true;
		rigidbody.isKinematic = true;
		collider.enabled = false;
		Vector3 unwrappedPoint = transform.position;
		Vector3 wrappedPoint = WorldWrapper.WrapPoint(transform.position);

		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			transform.position = Vector3.Slerp(unwrappedPoint, wrappedPoint, t);
		}));

		rigidbody.isKinematic = false;
		collider.enabled = true;

	}

	public void Unwrap()
	{
		StartCoroutine(UnwrapAnimate());
	}

	IEnumerator UnwrapAnimate()
	{
		wrapped = false;
		rigidbody.isKinematic = true;
		collider.enabled = false;
		Vector3 wrappedPoint = transform.position;
		Vector3 unwrappedPoint = WorldWrapper.UnwrapPoint(transform.position);
		
		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		                                       {
			transform.position = Vector3.Slerp(wrappedPoint, unwrappedPoint, t);
		}));
		
		rigidbody.isKinematic = false;
		collider.enabled = true;
		
	}

}
