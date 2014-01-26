using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour 
{
	public AnimationCurve fadeCurve;

	public static MusicController instance
	{
		get;
		private set;
	}

	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			StartCoroutine(FadeMusic());

	}
	
	IEnumerator FadeMusic()
	{

		yield return StartCoroutine(pTween.To (fadeCurve.lastTime(), t =>
		{
			instance.audio.volume = fadeCurve.Evaluate(1-t);
			audio.volume = fadeCurve.Evaluate(t);
		}));

		Destroy(instance.gameObject);

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

}
