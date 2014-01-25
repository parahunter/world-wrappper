using UnityEngine;
using System.Collections;

public class SkyPositioner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () { 
		GameObject level = gameObject.transform.parent.gameObject;
		Mesh mesh = level.GetComponent<MeshFilter> ().mesh;
		//Vector3 newPosition = new Vector3 (mesh.bounds.center.x, mesh.bounds.center.y, gameObject.transform.position.z);
		//gameObject.transform.position = newPosition;
		Vector3 newPosition = new Vector3 (mesh.bounds.center.x, mesh.bounds.center.y, gameObject.transform.localPosition.z);
		gameObject.transform.localPosition = newPosition;
	}
}
