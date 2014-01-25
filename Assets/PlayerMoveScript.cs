using UnityEngine;
using System.Collections;

struct KeyState
{
	public bool left;
	public bool right;
	public bool up;
	public bool space;
}

public class PlayerMoveScript : MonoBehaviour {
	
	KeyState mOldKeyState, mKeyState;

	private void CaptureKeyState()
	{
		mKeyState.left = Input.GetKeyDown (KeyCode.LeftArrow);
		mKeyState.right = Input.GetKeyDown (KeyCode.RightArrow);
		mKeyState.up = Input.GetKeyDown (KeyCode.UpArrow);
		mKeyState.space = Input.GetKeyDown (KeyCode.Space);
	}


	private void MoveControl()
	{ 
		float xMovement = Input.GetAxis ("Horizontal");

		if (WrapController.instance.wrapped)
				xMovement = -xMovement;

		if (xMovement != 0.0f) {
				gameObject.rigidbody.AddForce (gameObject.transform.right * xMovement * 20.0f);
				Debug.Log (gameObject.rigidbody.velocity);
		}
	}

	private bool Grounded()
	{
		Vector3 footPoint = gameObject.transform.position - (14.0f * gameObject.transform.up);

		Ray jumpRay = new Ray (footPoint, Vector3.back);

		if (Physics.Raycast (jumpRay)) {
			return true;
		}

		return false;
	}

	// Use this for initialization
	void Start () {
		mOldKeyState.up = false;
		mOldKeyState.space = false;
		mOldKeyState.left = false;
		mOldKeyState.right = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		CaptureKeyState();

		MoveControl();

		if(Grounded())
		{
			if(mKeyState.up && !mOldKeyState.up)
			{
				gameObject.rigidbody.AddForce(50.0f * transform.up);
			}
		}	

		mOldKeyState = mKeyState;
	}
}
