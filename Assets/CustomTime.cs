using UnityEngine;
using System.Collections;

public class CustomTime : MonoBehaviour {

	public static float customDelta;

	private float _storedTime;
	void Start () {
		_storedTime = Time.realtimeSinceStartup;
	}
	
	void Update () {
		customDelta = _storedTime - Time.realtimeSinceStartup;
		_storedTime = Time.realtimeSinceStartup;
	}
}
