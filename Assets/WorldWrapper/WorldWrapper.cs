using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WorldWrapper 
{
	const float worldLength = 360f;

	public static Vector3 WrapPoint(Vector3 point)
	{
		float radians = -point.x * Mathf.Deg2Rad;
		float height = point.y;
		
		float x = Mathf.Cos(radians) * height;
		float y = Mathf.Sin(radians) * height;

		return new Vector3(x,y,point.z);
	}

	public static Vector3 UnwrapPoint(Vector3 point)
	{
		float radians = -Mathf.Atan2(point.y, point.x);

		if(radians < 0)
			radians += 2*Mathf.PI;

		float x = radians * Mathf.Rad2Deg; 
		float y = point.magnitude;

		return new Vector3(x,y,point.z);
	}
}
