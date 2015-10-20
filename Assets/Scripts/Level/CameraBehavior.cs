
using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public FindPathBehavior findPathBehavior;
	public SpringReference springReference;
	public float offsetFactor = 3;
	public float minZoom = 3;
	public float maxZoom = 7;

	const float STIFFNESS = 1000;

	private bool _zooming = false;
	private float _zoomLerp = 0;
	private float _storedSize = 0;
	private float _lerpAmount = 0;
	private Vector3 _storedDestination;
	private Vector3 _destinationVector;
	private Vector3 _startVector;
	private Vector3 _direction;
	private Vector3 _velocity;

	void Start () {
		//Find the event pool and add the event to it
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent playerFoundEvent;
		eventPool.GetEventFromPool ("Player Found", out playerFoundEvent);
		playerFoundEvent.onHandleEvent += EventZoom;
	}
	
	void Update () {

//		Vector3 targetPos = new Vector3 (findPathBehavior.transform.position.x,
//		                                10,
//		                                findPathBehavior.transform.position.z);
//		float distance = Vector3.Distance (transform.position, targetPos);
//		float force = STIFFNESS * distance;
//		Vector3 direction = targetPos - transform.position;
//		Vector3 velocity = force * direction.normalized;
//		transform.position += velocity * Time.deltaTime * 0.5f;
//		Debug.DrawLine (transform.position, targetPos, Color.cyan);

		Vector3 direction = springReference.Direction.normalized * -1;
		float dis = springReference.Distance;
		Vector3 anchorPos = new Vector3 (findPathBehavior.transform.position.x,
		                                10,
		                                findPathBehavior.transform.position.z);
		transform.position = anchorPos + direction * dis;

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
