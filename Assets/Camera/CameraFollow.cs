using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;

	public float followTime = 1f;
	public float rotateFollowTime = 1f;

	private Vector3 velocity;
	private float rotateVelocity;

	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = transform.position;

		pos = Vector3.SmoothDamp(transform.position, target.position, ref velocity, followTime);
		pos.z = transform.position.z;

		transform.position = pos;
		
		float currentAngle = transform.rotation.eulerAngles.z;
		float angle = Mathf.SmoothDampAngle(currentAngle, target.rotation.eulerAngles.z, ref rotateVelocity, rotateFollowTime);

		transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
	}
}



