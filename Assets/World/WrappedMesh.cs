using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrappedMesh : WrappedEntity 
{
	MeshCollider meshCollider;
	Mesh mesh;
	// Use this for initialization
	
	Vector3[] originalVertices;
	Vector3[] wrappedVertices;

	Vector3 startScale;
	void Start () 
	{
		startScale = transform.localScale;
		WrapController.instance.AddBody(this);

		meshCollider = GetComponent<MeshCollider>();
		mesh = GetComponent<MeshFilter>().mesh;

		originalVertices = mesh.vertices;
		wrappedVertices = CalculateWrappedVertices(originalVertices);
		mesh.vertices.CopyTo(originalVertices, 0);
	}

	Vector3[] CalculateWrappedVertices(Vector3[] vertices)
	{
		Vector3[] verts = new Vector3[vertices.Length];

		for(int i = 0 ; i < verts.Length ; i++)
		{
			verts[i] = WorldWrapper.WrapPoint( vertices[i] + transform.position) + transform.position;
			verts[i] *= -1;
		}

		return verts;
	}

	public override void Wrap()
	{
		StartCoroutine(WrapAnimate());
	}

	public override void Unwrap()
	{
		StartCoroutine(UnwrapAnimate());
	}

	IEnumerator WrapAnimate()
	{
		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		{
			Vector3[] verts = mesh.vertices;
			print(t);
			for(int i = 0 ; i < verts.Length ; i++)
			{
				verts[i] = Vector3.Slerp(originalVertices[i], wrappedVertices[i], t);
			}
				
			mesh.vertices = verts;
			mesh.RecalculateBounds();

			if(meshCollider != null)
				meshCollider.sharedMesh = mesh;

			transform.localScale = startScale;
		}));
	}

	IEnumerator UnwrapAnimate()
	{
		yield return StartCoroutine( pTween.To(WrapController.instance.wrapTime, t =>
		                                       {
			Vector3[] verts = mesh.vertices;
			for(int i = 0 ; i < verts.Length ; i++)
			{
				verts[i] = Vector3.Slerp(wrappedVertices[i], originalVertices[i], t);
			}
			
			mesh.vertices = verts;
			mesh.RecalculateBounds();

			if(meshCollider != null)
				meshCollider.sharedMesh = mesh;
			
			transform.localScale = startScale;
		}));
	}

}
