using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour {

	public Sprite[] cutSceneFrames;
	public float frameTime = 3;
	public Image blackoutImage;

	private float _frameTimer = 0;
	private int _currentFrame = 0;
	private Image _imageComponent;
	private float _blackoutAlpha = 1;

	void Start () {
		_imageComponent = GetComponent<Image> ();
		_imageComponent.sprite = cutSceneFrames [_currentFrame];
	}
	
	void Update () {

		if (_currentFrame >= cutSceneFrames.Length - 1)
			return;

		if (_blackoutAlpha < 0 && _frameTimer < frameTime) {
				_frameTimer += Time.deltaTime;
				return;
		}
		else
			_blackoutAlpha -= Time.deltaTime;

		float clampedPositive = Mathf.Clamp01 (Mathf.Abs (_blackoutAlpha));
		if (clampedPositive == 1) {
			_currentFrame ++;
			_imageComponent.sprite = cutSceneFrames[_currentFrame];
			_blackoutAlpha = 1;
			_frameTimer = 0;
		}
		blackoutImage.color = new Color (0, 0, 0, clampedPositive);
	}
}
