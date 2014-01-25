using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WrapController : MonoBehaviour 
{
	public World world;
	public float wrapTime = 1f;

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

	public bool wrapped
	{
		get;
		private set;
	}

	void Start()
	{
		wrapped = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(wrapped)
			{
				world.Wrap();
				bodies.ForEach(body => body.Wrap());
			}
			else
			{
				world.Unwrap();
				bodies.ForEach(body => body.Unwrap());
			}
			wrapped = !wrapped;
		}
	}




}
