using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	private EventPool eventPool;

	public Transform targetTransform;
	public float minZoom = 3;
	public float maxZoom = 7;

	void Start () {
		eventPool = GameObject.FindObjectOfType<EventPool> ();

		GameEvent gameEvent;
		eventPool.GetEventFromPool ("Player Found", out gameEvent);
		gameEvent.onHandleEvent += EventZoom;
	}
	
	void Update () {
		transform.position = targetTransform.position - new Vector3 (0, -10, 0);
	}

	void EventZoom()
	{
		Camera.main.orthographicSize = minZoom;
	}
}
