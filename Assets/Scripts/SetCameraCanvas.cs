using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetCameraCanvas : MonoBehaviour {

    private Canvas canvas;

	void Start () {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
	}
}
