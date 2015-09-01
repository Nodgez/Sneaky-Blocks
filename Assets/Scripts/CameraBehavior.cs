using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public Transform targetTransform;
	public float minZoom = 3;
	public float maxZoom = 7;

	void Start () {
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();

		GameEvent playerFoundEvent;
		eventPool.GetEventFromPool ("Player Found", out playerFoundEvent);
		playerFoundEvent.onHandleEvent += EventZoom;
	}
	
	void Update () {
		transform.position = targetTransform.position - new Vector3 (0, -10, 0);
	}

	void EventZoom()
	{
		Camera.main.orthographicSize = minZoom;
	}
}
