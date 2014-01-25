using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WorldWrapper 
{
	const float worldLength = 360f;

	public static Vector3 WrapPoint(Vector3 point)
	{
		float radians = 2 * Mathf.PI * point.x / worldLength;
		float height = point.y;

		float x = Mathf.Cos(radians) * height;
		float y = Mathf.Sin(radians) * height;

		return new Vector3(x,y,0);
	}
}
