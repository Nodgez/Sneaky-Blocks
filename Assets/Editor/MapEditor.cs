using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Map))]
[ExecuteInEditMode()]
public class MapEditor : PlacementEditor {

	public BlockType blockType = BlockType.Wall;

	protected override void OnSceneGUI()
	{
		base.OnSceneGUI ();

		int controlID = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;

		GUIStyle style = new GUIStyle ();
		style.fontSize = 20;
		style.normal.textColor = Color.white;

		Vector3 screenToView = Camera.current.ScreenToViewportPoint (e.mousePosition);
		Vector3 viewToWorld = Camera.current.ViewportToWorldPoint (screenToView);

		Handles.BeginGUI ();
		GUI.Label (new Rect (0, 0, 200, 100),
		           "Screen Position : " + e.mousePosition + "\nView : " + screenToView + "\nWorld : " + viewToWorld
		           + "\nCamera Z : " + Camera.current.transform.position.z,
		           style);
		Handles.EndGUI ();

		if (!e.isKey || Camera.current == null)
			return;
		if (e.keyCode != KeyCode.Period)
			return;

		Map map = target as Map;
		switch (Event.current.GetTypeForControl (controlID)) 
		{
		case EventType.KeyDown:
			GUIUtility.hotControl = controlID;
			Vector3 position = SetPlacementPosition(e.mousePosition);

			for(int i = 0; i < map.transform.childCount;i++)
			{
				Transform childTrans = map.transform.GetChild(i);
				if(childTrans.position == position)
				{
					DestroyImmediate(childTrans.gameObject);
					return;
				}
			}
			switch(blockType)
			{
			case BlockType.Wall:
				Material wallMaterial = AssetDatabase.LoadAssetAtPath<Material> ("Assets/Materials/Wall.mat");
				GameObject wallPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
				wallPart.transform.position = position;
				wallPart.layer = LayerMask.NameToLayer("Wall");
				wallPart.transform.SetParent(map.transform,true);
				wallPart.isStatic = true;
				wallPart.GetComponent<Renderer>().sharedMaterial = wallMaterial;
				NavMeshBuilder.BuildNavMesh();
				break;
			case BlockType.Defensive_Wall:
				Material defensiveWallMaterial = AssetDatabase.LoadAssetAtPath<Material> ("Assets/Materials/DefensiveWall.mat");
				Object defensiveWallPrefab = AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Level/DefensiveWall.prefab");
				GameObject defensiveWall = PrefabUtility.InstantiatePrefab(defensiveWallPrefab) as GameObject;
				defensiveWall.transform.position = position;
				defensiveWall.transform.SetParent(map.transform,true);
				defensiveWall.GetComponent<Renderer>().sharedMaterial = defensiveWallMaterial;
				NavMeshBuilder.BuildNavMesh();
				break;
			}

			break;
		case EventType.KeyUp:
			GUIUtility.hotControl = 0;
			SceneView.RepaintAll();
			break;
		}
	}
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		blockType = (BlockType)EditorGUILayout.EnumPopup ("Type of Checkpoint", blockType);
		EditorUtility.SetDirty (target);
	}
}

public enum BlockType
{
	Wall,
	Defensive_Wall,
}
