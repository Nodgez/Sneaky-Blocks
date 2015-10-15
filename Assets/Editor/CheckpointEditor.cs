using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CheckpointPath))]
public class CheckpointEditor : PlacementEditor {

	private CheckpointType checkpointTypeToPlace;

	protected override void OnSceneGUI ()
	{
		CheckpointPath checkpointPath = target as CheckpointPath;
		base.OnSceneGUI ();

		DrawPath (checkpointPath);

		int controlID = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;
		if (!e.isKey || Camera.current == null)
			return;
		if (e.keyCode != KeyCode.Period)
			return;

		switch (Event.current.GetTypeForControl (controlID)) 
		{
		case EventType.KeyDown:
			GUIUtility.hotControl = controlID;
			Vector3 position = SetPlacementPosition(e.mousePosition);

			GameObject go = new GameObject(checkpointPath.tag + " " + checkpointTypeToPlace);
			go.transform.position = position;
			go.tag = checkpointPath.tag;
			go.transform.SetParent(checkpointPath.transform,true);
			BaseCheckpoint checkpointScript = null;
			switch(checkpointTypeToPlace)
			{
			case CheckpointType.Normal:
				checkpointScript = go.AddComponent<NormalCheckpoint>();
				break;
			case CheckpointType.Teleport:
				checkpointScript = go.AddComponent<TeleporterCheckpoint>();
				GameObject particles = AssetDatabase.LoadAssetAtPath<GameObject>(@"Assets\Prefabs\Particles\Teleporter Particles Enter");
				particles.transform.SetParent(go.transform);
				break;
			case CheckpointType.Survey:
				checkpointScript = go.AddComponent<SurveyCheckpoint>();
				break;
			case CheckpointType.Timed:
				checkpointScript = go.AddComponent<TimedCheckpoint>();
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
		checkpointTypeToPlace = (CheckpointType)EditorGUILayout.EnumPopup ("Type of Checkpoint", checkpointTypeToPlace);
		EditorUtility.SetDirty (target);
	}

	void DrawPath (CheckpointPath checkpointPath)
	{
		GUIStyle style = new GUIStyle ();
		style.fontSize = 12;
		style.normal.textColor = Color.gray;
		//1st child is always the guard on the path
		for (int i = 1; i < checkpointPath.transform.childCount; i++) {
			int nextCheckpointindex = i < checkpointPath.transform.childCount - 1 ? i + 1 : 1;
			Vector3 checkpointPosition = checkpointPath.transform.GetChild (i).position;
			Vector3 adjacentcheckpointPosition = checkpointPath.transform.GetChild (nextCheckpointindex).position;
			float travelTime = Vector3.Distance (checkpointPosition, adjacentcheckpointPosition) / 2.5f;

			Handles.Label (checkpointPosition, "CP " + (i) + " :\n"+ travelTime.ToString("f2") + " Secs", style);
		}
	}
}

public enum CheckpointType
{
	Normal = 0,
	Teleport = 1,
	Survey = 2,
	Timed = 3
}
