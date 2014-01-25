using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WrapController : MonoBehaviour 
{
	public World world;

	public static WrapController instance
	{
		get;
		private set;
	}

	List<WrappedRigidbody> bodies = new List<WrappedRigidbody>();

	public void AddBody(WrappedRigidbody body)
	{
		bodies.Add(body);
	}

	public void RemoveBody(WrappedRigidbody body)
	{
		bodies.Remove(body);
	}

	void Awake()
	{
		instance = this;
	}

	bool wrap = true;
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(wrap)
			{
				world.Wrap();
				bodies.ForEach(body => body.Wrap());
			}
			else
			{
				world.Unwrap();
				bodies.ForEach(body => body.Unwrap());
			}
			wrap = !wrap;
		}
	}


}
