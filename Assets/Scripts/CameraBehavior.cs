using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	private EventPool eventPool;

	public float minZoom = 3;
	public float maxZoom = 7;

	void Start () {
		eventPool = GameObject.FindObjectOfType<EventPool> ();

		GameEvent gameEvent;
		eventPool.GetEventFromPool ("Player Found", out gameEvent);
		gameEvent.onHandleEvent += EventZoom;
	}
	
	void Update () {
	
	}

	void EventZoom()
	{
		Camera.main.orthographicSize = minZoom; 
	}
}
