using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BaseCheckpoint))]
[ExecuteInEditMode()]
public class CheckpointInfo : Editor
{
//	void OnSceneGUI()
//	{
//		BaseCheckpoint selectedPoint = target as BaseCheckpoint;
//		float travelTime = Vector3.Distance (selectedPoint.Position, selectedPoint.AdjacentCheckpoint.Position) / 2.5f;
//		
//		Handles.BeginGUI ();
//		GUI.Label(new Rect(0,0,100,20), "Travel Time to Next Node = " + travelTime + " Seconds");
//		Handles.EndGUI ();
//	}
}
