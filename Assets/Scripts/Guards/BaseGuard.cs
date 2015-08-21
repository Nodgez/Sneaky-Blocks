using UnityEngine;
using System.Collections;

public class BaseGuard : MonoBehaviour,IDetect {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public virtual void Seek()
	{

	}

	public virtual bool DetectUnit(Vector3 position)
	{
		return false;
	}

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

	public string TargetName {
		get;
		set;
	}

	public float DetectionRadius {
		get;
		set;
	}
}
