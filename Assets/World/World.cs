using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour 
{
	MeshCollider meshCollider;
	Mesh mesh;
	public int subdivison = 10;
	// Use this for initialization
	
	Vector3[] originalVertices;

	Vector3 unwrappedScale = new Vector3(-1, 1, -1);
	Vector3 wrappedScale = new Vector3(-1,1,1);

	void Start () 
	{
		meshCollider = GetComponent<MeshCollider>();
		mesh = GetComponent<MeshFilter>().mesh;
		originalVertices = mesh.vertices;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
			Wrap();
	}
	
	void Wrap()
	{

		Vector3[] verts = mesh.vertices;

		for(int i = 0 ; i < verts.Length ; i++)
		{
			verts[i] = WorldWrapper.WrapPoint( verts[i] );
		}
		
		mesh.vertices = verts;
		meshCollider.sharedMesh = mesh;

		transform.localScale = wrappedScale;
	}

}
