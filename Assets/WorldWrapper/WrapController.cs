﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WrapController : MonoBehaviour 
{
	public CameraFollow followScript;
	public float wrapTime = 1f;
	public bool startWrapped = false;

	public bool isWrapping
	{
		get;
		private set;
	}

	public float wrapFactor
	{
		get;
		private set;
	}

	public static WrapController instance
	{
		get;
		private set;
	}

	List<WrappedEntity> entities = new List<WrappedEntity>();

	public void AddBody(WrappedEntity body)
	{
		entities.Add(body);
	}

	public void RemoveBody(WrappedEntity body)
	{
		entities.Remove(body);
	}

	void Awake()
	{
		instance = this;
		isWrapping = false;
		wrapFactor = 0;
	}

	public bool wrapped
	{
		get;
		private set;
	}

	void Start()
	{
		wrapped = false;
		if(startWrapped)
			StartCoroutine(WrapCoroutine());
	}

	public void DoWrap()
	{
		if(!isWrapping)
			StartCoroutine(WrapCoroutine());
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q) && !isWrapping)
		{
			StartCoroutine(WrapCoroutine());
		}
	}

	IEnumerator WrapCoroutine()
	{
		wrapped = !wrapped;
		isWrapping = true;
		if(wrapped)
		{
			followScript.Wrap();
			entities.ForEach(body => body.Wrap());
		}
		else
		{
			followScript.Unwrap();
			entities.ForEach(body => body.Unwrap());
		}


		yield return StartCoroutine(pTween.To(wrapTime, t =>
		{
			if(wrapped)
				wrapFactor = t;
			else
				wrapFactor = 1 -t;

		}));

		yield return new WaitForSeconds(0.1f);

		isWrapping = false;
	}


}
