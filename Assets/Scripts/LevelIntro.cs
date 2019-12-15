using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
	private InputManager inputManager;
	private CameraBehavior cameraBehavior;
	private List<ICameraMoveTo> cameraMoveToPath = new List<ICameraMoveTo>();

	void Start()
	{
		cameraBehavior = FindObjectOfType<CameraBehavior>();
		cameraMoveToPath = FindObjectsOfType<MonoBehaviour>().OfType<ICameraMoveTo>().ToList();// this line hurts my soul
		inputManager = FindObjectOfType<InputManager>();

		StartCoroutine(CyclePoints());
	}

	IEnumerator CyclePoints()
	{
		var waitHalfASecond = new WaitForSeconds(0.5f);
		var t = 0f;
		var cameraTransform = cameraBehavior.transform;
		var cameraLerpStart = cameraTransform.position;
		cameraBehavior.enabled = false;
		inputManager.enabled = false;
		foreach (var pt in cameraMoveToPath)
		{
			while (t != 1)
			{
				t = Mathf.Clamp01(t += Time.deltaTime);
				cameraTransform.position = Vector3.Lerp(cameraLerpStart, (pt as MonoBehaviour).transform.position, EasingFunction.EaseInElastic(0, 1, t));
				yield return null;
			}
			yield return waitHalfASecond;
			t = 0;
		}

		cameraBehavior.enabled = true;
		inputManager.enabled = true;
	}
}
