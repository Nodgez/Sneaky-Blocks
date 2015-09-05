using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

	Mesh mesh;
	MeshFilter meshFilter;
	GameObject viewMesh;
	public Material viewMaterial;
	public int viewAngle;
	public float radius;
	
	void Awake()
	{
		mesh = new Mesh ();
		gameObject.AddComponent<MeshRenderer> ().sharedMaterial = viewMaterial;
		meshFilter = gameObject.AddComponent<MeshFilter> ();
		meshFilter.mesh = mesh;
		//DrawFOV ();
	}

	void Update()
	{
		DrawFOV ();
	}
	
	protected void DrawFOV()
	{
		Vector3[] points = new Vector3[(viewAngle * 2) + 1];
		int[] indices = new int[(points.Length * 3) - 6];
		points [points.Length -1] = this.transform.localPosition;

		for(int i = -viewAngle;i < viewAngle;i++)
		{
			float x = this.transform.position.x;
			float z = this.transform.position.z;
			float yRot = transform.rotation.eulerAngles.y;

			Vector3 point1 = new Vector3(x + radius * Mathf.Cos(((i) - yRot) * Mathf.Deg2Rad),
			                             this.transform.position.y,
			                             z + radius * Mathf.Sin(((i) - yRot) * Mathf.Deg2Rad));

			RaycastHit hit;
			Ray ray = new Ray(transform.position
			                  ,point1 - transform.position);

			if(Physics.Raycast(ray,out hit, radius, 1 << 8))
				point1 = hit.point;

			point1 = transform.InverseTransformPoint(point1);
			points[i + viewAngle] = point1;
		}
		
		for(int j = 0;j < indices.Length;j += 3)
		{
			int secondIndex = j - 2 < 0 ? 1 : indices[j - 2] + 1;
			int thirdIndex = j - 3 < 0 ? 2 : indices[j - 3] + 1;
			
			indices[j] = thirdIndex;
			indices[j + 1] = secondIndex;
			indices[j + 2] = points.Length -1;
		}
		
		mesh.vertices = points;
		mesh.triangles = indices;
	}
}
