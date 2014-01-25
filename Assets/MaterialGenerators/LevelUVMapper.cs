using UnityEngine;
using System.Collections;

public class LevelUVMapper : MonoBehaviour {

	public Material material;
	public Mesh mesh;

	// Use this for initialization
	void Start () {
		if (mesh == null)
			mesh = GetComponent<MeshFilter>().mesh;

		// find edges
		Bounds bounds = mesh.bounds;
		Vector3[] verts = mesh.vertices;
		Vector2[] uvs = mesh.uv;
		
		for(int i = 0 ; i < verts.Length ; i++)
		{
			uvs[i].x = Mathf.InverseLerp(bounds.min.x, bounds.max.x, verts[i].x);
			uvs[i].y = Mathf.InverseLerp(bounds.min.y, bounds.max.y, verts[i].y);
		}

		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
