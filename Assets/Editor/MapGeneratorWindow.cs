using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapGeneratorWindow : EditorWindow {
	[MenuItem("Map Generator/Generator")]
	public static void OpenMapGenerator()
	{
		EditorWindow.GetWindow(typeof(MapGeneratorWindow));
	}

	public int width;
	public int height;
	public Map map;

	void OnGUI()
	{
		GUILayout.Label ("Width");
		width = EditorGUILayout.IntField (width);
		
		GUILayout.Label ("Height");
		height = EditorGUILayout.IntField (height);

		GUILayout.Label ("Map");
		map = EditorGUILayout.ObjectField (map, typeof(Map), true) as Map;

		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("generate")) {
			Generate ();
			//Combine ();
		}

		if (GUILayout.Button ("Combine")) {
            Combine();
		}

		GUILayout.EndHorizontal ();
		EditorUtility.SetDirty (this);
	}

	void Generate () {
		if (map == null) {
			Debug.Log("No Map Selected");
			return;
		}

		GameObject pad = GameObject.CreatePrimitive (PrimitiveType.Quad);
        pad.transform.position = new Vector3(((float)width -1) * 0.5f, 0, ((float)height -1) * 0.5f);
		pad.transform.rotation = Quaternion.Euler (90, 0, 0);
		pad.transform.localScale = new Vector3 (width, height,1);
		pad.isStatic = true;
		pad.transform.SetParent (map.transform);
        pad.transform.SetAsFirstSibling();
		pad.GetComponent<Renderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material> ("Assets/Materials/Floor.mat");
	}

	void Combine() {
		MeshFilter[] meshFilters = map.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		for(int i = meshFilters.Length - 1; i > -1; i--) {
            if (meshFilters[i].tag != "Quad")
                continue;
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			if(i != 0)
				DestroyImmediate(meshFilters[i].gameObject);
		}
		map.transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
		map.transform.GetComponent<MeshFilter> ().sharedMesh.CombineMeshes (combine);
		map.gameObject.AddComponent<MeshCollider> ();
	}
}