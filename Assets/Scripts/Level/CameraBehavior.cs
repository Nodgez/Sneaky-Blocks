using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public Transform targetTransform;
	public float minZoom = 3;
	public float maxZoom = 7;

	private bool _zooming = false;
	private float _zoomLerp = 0;
	private float _storedSize = 0;

	void Start () {
		//Find the event pool and add the event to it
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent playerFoundEvent;
		eventPool.GetEventFromPool ("Player Found", out playerFoundEvent);
		playerFoundEvent.onHandleEvent += EventZoom;
	}
	
	void Update () {
		//follow the target
		transform.position = targetTransform.position - new Vector3 (0, -10, 0);

		if (!_zooming)
			return;
		float cameraSize = 0;
		_zoomLerp += Time.unscaledDeltaTime;
		cameraSize = _storedSize + Mathf.Clamp01(_zoomLerp) * (minZoom - _storedSize);
		Camera.main.orthographicSize = cameraSize;

		if (cameraSize == minZoom) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	//zoom properties set on event
	void EventZoom()
	{
		Time.timeScale = 0;
		_zooming = true;
		_storedSize = Camera.main.orthographicSize;
	}
}
