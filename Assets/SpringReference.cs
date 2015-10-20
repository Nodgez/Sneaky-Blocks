using UnityEngine;
using System.Collections;

public class SpringReference : MonoBehaviour {

	public Transform targetTransform;

	public float stiffness = 2.5f;
	Vector3 targetPos;
	float previousDistance = 0;
	private float originalStiffness;

	// Use this for initialization
	void Start () {
		originalStiffness = stiffness;
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = new Vector3 (targetTransform.position.x,
		                                 10,
		                                 targetTransform.position.z);
		float distance = Vector3.Distance (transform.position, targetPos);
		float force = stiffness * distance;
		Vector3 direction = targetPos - transform.position;
		Vector3 velocity = force * direction.normalized;
		transform.position += velocity * Time.deltaTime * 0.5f;

//		Debug.Log ("Distance Difference : " + (previousDistance - distance).ToString());

//		//moving away
//		if (previousDistance <= distance) {
//			stiffness = originalStiffness;
//		}
//		//moving too
//		else if(previousDistance > distance) {
//			stiffness = 10f;
//		}

		previousDistance = distance;
		Debug.DrawLine (transform.position, targetPos, Color.cyan);
	}

	public Vector3 Direction
	{
		get{ 
			Vector3 direction = transform.position - targetTransform.position;
			return new Vector3(direction.x, 0,direction.z);
		}
	}

	public float Distance
	{
		get{return Vector3.Distance (transform.position, targetPos);}
	}

	public Vector3 Position
	{
		get{ return transform.position;}
	}
}
