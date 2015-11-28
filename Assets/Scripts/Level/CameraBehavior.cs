using UnityEngine;
using ProBuilder2.Common;
using ProBuilder2.MeshOperations;
using ProBuilder2.Math;
using ProBuilder2.Triangulator;
using System.Collections;
using System.Collections.Generic;

public class CameraBehavior : MonoBehaviour {

    public Transform targetTrasform;
	public float minZoom = 3;
    public float midZoom = 5;
	public float maxZoom = 7;

    public float width;
    public float height;

	private bool _zooming = false;
	private float _zoomLerp = 0;
	private float _storedSize = 0;

	void Start () {
		//Find the event pool and add the event to it
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent playerFoundEvent;
		eventPool.GetEventFromPool ("Player Found", out playerFoundEvent);
		playerFoundEvent.onHandleEvent += EventZoom;

        Vector3 mapScale = Vector3.zero;
        float area = width * height;
        float size = minZoom;
        if (area > 100)
            size = midZoom;
        else if (area > 200)
            size = maxZoom;

        Camera.main.orthographicSize = size;
    }

    void Update () {

        transform.position = new Vector3(targetTrasform.position.x, 10, targetTrasform.position.z);

		if (!_zooming)
			return;
		float cameraSize = 0;
		_zoomLerp += Time.unscaledDeltaTime;
		cameraSize = _storedSize + Mathf.Clamp01(_zoomLerp) * (minZoom - _storedSize);
		Camera.main.orthographicSize = cameraSize;

		if (cameraSize == minZoom) {
			Time.timeScale = 1;
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	//zoom properties set on event trigger
	void EventZoom()
	{
		Time.timeScale = 0;
		_zooming = true;
		_storedSize = Camera.main.orthographicSize;
	}

	Vector3 Interpolate(Vector3 start, Vector3 end, float interpAmount)
	{
		interpAmount = Mathf.Clamp01 (interpAmount);
		if (interpAmount == 1)
			return end;
		else if (interpAmount == 0)
			return start;
		else
			return Vector3.Lerp (start, end, interpAmount);
	}
}
