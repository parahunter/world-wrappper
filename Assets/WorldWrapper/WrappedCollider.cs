using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrappedCollider : WrappedEntity 
{

	void Start()
	{
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
		Vector3 unwrappedDirection = transform.up;
		Vector3 wrappedDirection = WorldWrapper.WrapPoint(transform.up);

		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			transform.position = Vector3.Slerp(unwrappedPoint, wrappedPoint, t);
			transform.up = Vector3.Slerp(unwrappedDirection, wrappedDirection, t);
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

		Vector3 wrappedPoint = transform.position;
		Vector3 unwrappedPoint = WorldWrapper.UnwrapPoint(transform.position);
		
		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			transform.position = Vector3.Slerp(wrappedPoint, unwrappedPoint, t);
		}));
		
		collider.enabled = true;
	}
}
