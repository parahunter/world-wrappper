using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour 
{
	Transform target;

	public float followTime = 1f;
	public float rotateFollowTime = 1f;
		
	private Vector3 velocity;
	private float rotateVelocity;
	public float heightOffsetUnwrapped = 5;
	public float heightOffsetWrapped = 5;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		Vector3 pos = GameObject.FindGameObjectWithTag("Portal").transform.position;

		pos.z = transform.position.z;

		transform.position = pos;
	}

	// Update is called once per frame
	void Update () 
	{
		if(WrapController.instance.isWrapping)
			return;

		Vector3 pos = transform.position;

		float heightOffset = WrapController.instance.wrapped ? heightOffsetWrapped : heightOffsetUnwrapped;
		Vector3 targetPos = target.position + target.up * heightOffset;
		pos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followTime);
		pos.z = transform.position.z;

		transform.position = pos;
		
		float currentAngle = transform.rotation.eulerAngles.z;
		float angle = Mathf.SmoothDampAngle(currentAngle, target.rotation.eulerAngles.z, ref rotateVelocity, rotateFollowTime);

		transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
	}

	public void Wrap()
	{
		StartCoroutine(WrapAnimate());
	}
	
	IEnumerator WrapAnimate()
	{
		Vector3 unwrappedPoint = transform.position;
		Vector3 wrappedPoint = WorldWrapper.WrapPoint(transform.position);

		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
        {
			transform.position = Vector3.Slerp(unwrappedPoint, wrappedPoint, t);
		}));
	}
	
	public void Unwrap()
	{
		StartCoroutine(UnwrapAnimate());
	}
	
	IEnumerator UnwrapAnimate()
	{
		Vector3 unwrappedPoint = WorldWrapper.UnwrapPoint(transform.position);
		Vector3 wrappedPoint = transform.position;

		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		                                       {
			transform.position = Vector3.Slerp(wrappedPoint, unwrappedPoint, t);
		}));
	}
}



