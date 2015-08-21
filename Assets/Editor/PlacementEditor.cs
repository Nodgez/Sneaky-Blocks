using UnityEngine;
using UnityEditor;
using System.Collections;

public abstract class PlacementEditor : Editor {

	protected virtual void OnSceneGUI()
	{
		if (EditorWindow.focusedWindow) {
			if (EditorWindow.focusedWindow.titleContent.text == "Hierarchy") {
				var window = EditorWindow.GetWindow <SceneView> ("Scene");
				window.Focus ();
			}
		}
	}

	protected Vector3 SetPlacementPosition(Vector3 mousePos)
	{
		Vector3 position = Camera.current.ScreenToWorldPoint(mousePos);
		position.z -= Camera.current.transform.position.z * 2;
		return position = new Vector3 (Mathf.Round (position.x),
		                               0.5f,
		                               Mathf.Round (-position.z));
	}
}
