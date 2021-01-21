using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

	private Mesh _mesh;
	private MeshFilter _meshFilter;
	private GameObject _viewMesh;

	public Material viewMaterial;
	public int viewAngle;
	public float radius;
    public LayerMask detectionLayer;
	
	void Awake()
	{
		_mesh = new Mesh ();
		gameObject.AddComponent<MeshRenderer> ().sharedMaterial = viewMaterial;
		_meshFilter = gameObject.AddComponent<MeshFilter> ();
		_meshFilter.mesh = _mesh;
	}

	void Update()
	{
		DrawFOV ();
	}
	
	protected void DrawFOV()
	{
		if (viewAngle % 2 != 0)
			viewAngle += 1;
		Vector3[] points = new Vector3[(viewAngle) + 1];
		int[] indices = new int[(points.Length * 3) - 6];
		points [points.Length -1] = this.transform.localPosition;
		int threshHold = viewAngle / 2;

		for(int i = -threshHold;i < threshHold;i++)
		{
			float x = this.transform.position.x;
			float z = this.transform.position.z;
			float yRot = transform.rotation.eulerAngles.y;

			Vector3 point1 = new Vector3(x + radius * Mathf.Cos(((i * 2) - yRot) * Mathf.Deg2Rad),
			                             this.transform.position.y,
			                             z + radius * Mathf.Sin(((i * 2) - yRot) * Mathf.Deg2Rad));

			RaycastHit hit;
			Ray ray = new Ray(transform.position
			                  ,point1 - transform.position);

			if(Physics.Raycast(ray,out hit, radius, detectionLayer))
				point1 = hit.point;

			point1 = transform.InverseTransformPoint(point1);
			points[i + threshHold] = point1;
		}
		
		for(int j = 0;j < indices.Length;j += 3)
		{
			int secondIndex = j - 2 < 0 ? 1 : indices[j - 2] + 1;
			int thirdIndex = j - 3 < 0 ? 2 : indices[j - 3] + 1;
			
			indices[j] = thirdIndex;
			indices[j + 1] = secondIndex;
			indices[j + 2] = points.Length -1;
		}
		
		_mesh.vertices = points;
		_mesh.triangles = indices;
	}
}
