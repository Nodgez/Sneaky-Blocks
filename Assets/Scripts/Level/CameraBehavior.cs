using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehavior : MonoBehaviour
{
    public Transform targetTrasform;
    public float minZoom = 3;
    public float midZoom = 5;
    public float maxZoom = 7;

    public float width;
    public float height;

    private float _storedSize = 0;
    private Vector3 velocity;
    void Start()
    {
        //Find the event pool and add the event to it
        EventPool eventPool = GameObject.FindObjectOfType<EventPool>();
        GameEvent playerFoundEvent;
        eventPool.GetEventFromPool("Player Found", out playerFoundEvent);
        //playerFoundEvent.onHandleEvent += EventZoom;

        Vector3 mapScale = Vector3.zero;
        float area = width * height;
        float size = minZoom;
        if (area > 100)
            size = midZoom;
        else if (area > 200)
            size = maxZoom;

        Camera.main.orthographicSize = size;
    }

    void Update()
    {
        var newTargetPosition = new Vector3(targetTrasform.position.x, 10, targetTrasform.position.z);
        var cameraDirection = (Input.mousePosition - newTargetPosition).normalized;
        transform.position = Vector3.SmoothDamp(transform.position, newTargetPosition + cameraDirection, ref velocity, 0.3f);
    }

    IEnumerator Zoom()
	{
		float t = 0;
		float cameraSize = 0;

		while (t != 1)
		{
			t = Mathf.Clamp01(t + Time.unscaledDeltaTime / 3f);
			cameraSize = Mathf.Lerp(_storedSize, minZoom, t);
			Camera.main.orthographicSize = cameraSize;
			yield return null;
		}

		if (cameraSize == minZoom)
		{
			Time.timeScale = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

	}

    //zoom properties set on event trigger
    void EventZoom()
    {
        Time.timeScale = 0;
        _storedSize = Camera.main.orthographicSize;

		StartCoroutine(Zoom());
    }

    Vector3 Interpolate(Vector3 start, Vector3 end, float interpAmount)
    {
        interpAmount = Mathf.Clamp01(interpAmount);
        if (interpAmount == 1)
            return end;
        else if (interpAmount == 0)
            return start;
        else
            return Vector3.Lerp(start, end, interpAmount);
    }
}