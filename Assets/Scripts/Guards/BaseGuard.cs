using UnityEngine;
using System.Collections;
/// <summary>
/// Abstract class for guard type objects
/// </summary>
public abstract class BaseGuard : MonoBehaviour {

	public float detectionRadius = 5f;
	public string targetName;

	public abstract void Seek ();
	public abstract bool DetectUnit(Vector3 position);
}
