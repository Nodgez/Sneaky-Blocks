﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
	private InputManager _inputManager;
	private CameraBehavior _cameraBehavior;
	private List<ICameraMoveTo> _cameraMoveToPath = new List<ICameraMoveTo>();

	void Start()
	{
		_cameraBehavior = FindObjectOfType<CameraBehavior>();
		_cameraMoveToPath = FindObjectsOfType<MonoBehaviour>().OfType<ICameraMoveTo>().OrderBy(x => x.CameraPriority).ToList();// this line hurts my soul
		_inputManager = FindObjectOfType<InputManager>();


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

		_cameraBehavior.enabled = false;
		_inputManager.enabled = false;
		foreach (var pt in _cameraMoveToPath)
		{
			var cameraLerpEnd = (pt as MonoBehaviour).transform.position + new Vector3(0,10,0 );
			while (t != 1)
			{
				t = Mathf.Clamp01(t += Time.deltaTime);
				cameraTransform.position = Vector3.Lerp(cameraLerpStart, cameraLerpEnd, EasingFunction.EaseInQuart(0, 1, t));
				yield return null;
			}
			yield return waitHalfASecond;
			t = 0;
			cameraLerpStart = cameraLerpEnd;
		}

		_cameraBehavior.enabled = true;
		_inputManager.enabled = true;
	}
}
