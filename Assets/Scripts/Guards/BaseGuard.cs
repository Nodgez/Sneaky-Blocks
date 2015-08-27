using UnityEngine;
using System.Collections;

public abstract class BaseGuard : MonoBehaviour, ITrigger {

	public float detectionRadius = 5f;
	public string targetName;

	public abstract void Seek ();
	public abstract bool DetectUnit(Vector3 position);

	public Vector3 Position
	{
		get{ return this.transform.position;}
		set{this.transform.position = value;}
	}
	
	public Quaternion Rotation
	{
		get{ return this.transform.rotation;}
		set{this.transform.rotation = value;}
	}

	public bool IsTriggered {
		get;
		set;
	}
}
