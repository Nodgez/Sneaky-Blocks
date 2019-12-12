using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine;

public class Wall : IEditorBlock
{
	public void ConstructBlock(Transform parent, Vector3 position)
	{
		Material wallMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Wall.mat");
		GameObject wallPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wallPart.transform.position = position;
		wallPart.layer = 8;
		wallPart.transform.SetParent(parent, true);
		wallPart.isStatic = true;
		wallPart.GetComponent<Renderer>().sharedMaterial = wallMaterial;
		NavMeshBuilder.BuildNavMesh();
	}
}
