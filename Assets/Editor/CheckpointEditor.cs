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

			GameObject go = new GameObject(checkpointPath.tag);
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
				break;
			case CheckpointType.Survey:
				checkpointScript = go.AddComponent<SurveyCheckpoint>();
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
		style.normal.textColor = Color.white;
		for (int i = 0; i < checkpointPath.transform.childCount; i++) {
			int nextCheckpointindex = i < checkpointPath.transform.childCount ? i : 0;
			Vector3 checkpointPosition = checkpointPath.transform.GetChild (i).position;
			Vector3 adjacentcheckpointPosition = checkpointPath.transform.GetChild (nextCheckpointindex).position;
			Handles.Label (checkpointPosition, "CP " + (i + 1), style);
		}
	}
}

public enum CheckpointType
{
	Normal = 0,
	Teleport = 1,
	Survey = 2
}
