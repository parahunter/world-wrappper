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
	bool grounded = false;

	public float baseGrip;
	public float fullGrip;
	public float circleRadius;
	public float jumpPower;
	public ForceMode forcemode;

	KeyState mOldKeyState, mKeyState;

	bool shouldJump = false;

	void Update()
	{
		if(Input.GetButtonDown("Vertical"))
			shouldJump = true;
		else if(Input.GetButtonUp("Vertical"))
			shouldJump = false;
	}

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

		float grip = baseGrip;

		if (grounded)
			grip = fullGrip;

		rigidbody.AddForce (transform.right * xMovement * grip, forcemode);
	}

	private bool Grounded()
	{
		Vector3 footPoint = Vector3.back * 1000 + transform.position - (circleRadius * transform.up);

		Ray jumpRay = new Ray (footPoint, Vector3.forward);
		Debug.DrawRay(jumpRay.origin, jumpRay.direction);
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
		if(!enabled)
			return;

		grounded = Grounded(); 

		CaptureKeyState();

		MoveControl();

		if(grounded)
		{
			if(shouldJump)
			{
				shouldJump = false;
				rigidbody.AddForce(jumpPower * transform.up, forcemode);
			}
		}	

		mOldKeyState = mKeyState;
	}
}
