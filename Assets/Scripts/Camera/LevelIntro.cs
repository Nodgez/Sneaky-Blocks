using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
	[SerializeField]
	[Range(0.25f, 5f)]
	private float _cameraCycleSpeed;
	private CameraBehavior _cameraBehavior;
	private FindPathBehavior _findPathBehavior;
	private List<ICameraTarget> _cameraMoveToPath = new List<ICameraTarget>();

	public Action OnIntroCycleComplete;

	void Start()
	{
		_cameraBehavior = FindObjectOfType<CameraBehavior>();
		_cameraMoveToPath = FindObjectsOfType<MonoBehaviour>().OfType<ICameraTarget>().OrderBy(x => x.CameraPriority).ToList();// this line hurts my soul
		_findPathBehavior = _cameraMoveToPath.Last().ConvertToComponent<FindPathBehavior>();

		_findPathBehavior.enabled = false;
		_cameraBehavior.enabled = false;
		_cameraBehavior.transform.position = (_cameraMoveToPath[0] as MonoBehaviour).transform.position + new Vector3(0, 10, 0);

        foreach (var pt in _cameraMoveToPath)
			Debug.Log(pt.ToString());
		StartCoroutine(CyclePoints());
	}

	IEnumerator CyclePoints()
	{
		var waitHalfASecond = new WaitForSeconds(0.5f);
		var t = 0f;
		var cameraTransform = _cameraBehavior.transform;
		var cameraLerpStart = cameraTransform.position;

		foreach (var pt in _cameraMoveToPath)
		{
			var cameraLerpEnd = (pt as MonoBehaviour).transform.position + new Vector3(0, 10, 0);
			while (t != 1)
			{
				t = Mathf.Clamp01(t += Time.deltaTime / _cameraCycleSpeed);
				cameraTransform.position = Vector3.Lerp(cameraLerpStart, cameraLerpEnd, EasingFunction.EaseInOutCubic(0, 1, t));
				yield return null;
			}
			yield return waitHalfASecond;
			t = 0;
			cameraLerpStart = cameraLerpEnd;
		}

		_cameraBehavior.enabled = true;
		_findPathBehavior.enabled = true;

		OnIntroCycleComplete();
	}
}
