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
	Vector3[] wrappedVertices;

	Vector3 unwrappedScale = new Vector3(-1, 1, -1);
	Vector3 wrappedScale = new Vector3(-1,1,1);

	void Start () 
	{
		meshCollider = GetComponent<MeshCollider>();
		mesh = GetComponent<MeshFilter>().mesh;

		originalVertices = new Vector3[mesh.vertexCount];
		wrappedVertices = CalculateWrappedVertices(originalVertices);
		mesh.vertices.CopyTo(originalVertices, 0);
	}

	Vector3[] CalculateWrappedVertices(Vector3[] vertices)
	{
		Vector3[] verts = vertices;
		
		for(int i = 0 ; i < verts.Length ; i++)
		{
			verts[i] = WorldWrapper.WrapPoint( verts[i] );
			verts[i] *= -1;
		}

		return verts;
	}

	public void Wrap()
	{
		StartCoroutine(WrapAnimate());
	}

	public void Unwrap()
	{
		mesh.vertices = originalVertices;
		meshCollider.sharedMesh = mesh;

		transform.localScale = Vector3.one;
	}

	IEnumerator WrapAnimate()
	{
		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			Vector3[] verts = originalVertices;

			for(int i = 0 ; i < verts.Length ; i++)
			{
				verts[i] = Vector3.Slerp(originalVertices[i], wrappedVertices[i], t);
			}

			mesh.vertices = verts;
			meshCollider.sharedMesh = mesh;

			transform.localScale = Vector3.one;
		}));



	}
}
