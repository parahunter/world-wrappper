using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portal : MonoBehaviour 
{
	public Transform door;
	public float doorCloseTime = 0.5f;
	public float ballScaleTime = 1f;

	public AudioSource winSound;
	public AudioSource exitSound;
	bool triggered = false;
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !triggered)
		{
			triggered = true;
			StartCoroutine(Transition(other.gameObject));
		}
	}

	IEnumerator Transition(GameObject player)
	{
		player.GetComponent<PlayerMoveScript>().enabled = false;

		Vector3 doorClosedScale = door.localScale;
		Vector3 doorOpenScale = doorClosedScale;

		winSound.Play();
		doorOpenScale.x = 0;
		yield return StartCoroutine(pTween.To (doorCloseTime, t =>
		                                       {
			door.localScale = Vector3.Lerp(doorClosedScale, doorOpenScale , t);
			
		}));

		Transform playerTransform = player.transform;
		Vector3 startScale = playerTransform.localScale;

		Vector3 playerStartPos = playerTransform.position;
		exitSound.Play();
		yield return StartCoroutine(pTween.To (ballScaleTime, t =>
			                                       {
			playerTransform.position = Vector3.Lerp(playerStartPos, transform.position, t);
			playerTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

		}));

		yield return StartCoroutine(pTween.To (doorCloseTime, t =>
		                                       {
			door.localScale = Vector3.Lerp(doorOpenScale, doorClosedScale, t);
			
		}));

		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
