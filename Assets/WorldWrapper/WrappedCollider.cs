using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrappedCollider : WrappedEntity 
{
	Vector3 originalPos;
	Quaternion originalRotation;

	void Start()
	{
		originalPos = transform.position;
		originalRotation = transform.rotation;
		WrapController.instance.AddBody(this);
	}

	public override void Wrap()
	{
		StartCoroutine(WrapAnimate());
	}
	
	IEnumerator WrapAnimate()
	{
		wrapped = true;
		collider.enabled = false;
		Vector3 unwrappedPoint = transform.position;
		Vector3 wrappedPoint = WorldWrapper.WrapPoint(transform.position);

		Quaternion unwrappedRotation = transform.rotation;
		Vector3 newDirection = wrappedPoint.normalized;
		newDirection.z = 0;
		Quaternion wrappedRotation = transform.rotation * Quaternion.FromToRotation(Vector3.up, newDirection.normalized);

		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			transform.position = Vector3.Slerp(unwrappedPoint, wrappedPoint, t);
			transform.rotation = Quaternion.Slerp(unwrappedRotation, wrappedRotation,t);
		}));
		
		collider.enabled = true;
		
	}
	
	public override void Unwrap()
	{
		StartCoroutine(UnwrapAnimate());
	}
	
	IEnumerator UnwrapAnimate()
	{
			wrapped = false;
			collider.enabled = false;
			Vector3 unwrappedPoint = originalPos;
			Vector3 wrappedPoint = transform.position;
			
			Quaternion wrappedRotation = transform.rotation;
			Quaternion unwrappedRotation = originalRotation;
			
			yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
			{
			transform.position = Vector3.Slerp(wrappedPoint, unwrappedPoint, t);
				transform.rotation = Quaternion.Slerp(wrappedRotation, unwrappedRotation,t);
			}));
			
			collider.enabled = true;
		}
}
