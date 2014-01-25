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

		originalVertices = new Vector3[mesh.vertexCount];
		mesh.vertices.CopyTo(originalVertices, 0);
	}
	
	public void Wrap()
	{

		Vector3[] verts = mesh.vertices;

		for(int i = 0 ; i < verts.Length ; i++)
		{
			verts[i] = WorldWrapper.WrapPoint( verts[i] );
			verts[i] *= -1;
		}
		
		mesh.vertices = verts;
		meshCollider.sharedMesh = mesh;

		//transform.localScale = wrappedScale;
	}

	public void Unwrap()
	{
		mesh.vertices = originalVertices;
		meshCollider.sharedMesh = mesh;

		//transform.localScale = unwrappedScale;
	}
}
