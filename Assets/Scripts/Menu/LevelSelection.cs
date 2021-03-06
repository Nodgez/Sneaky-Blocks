﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

	public int levelCount;
	public Button buttonPrefab;

	void Start () {
		RectTransform rectTransform = transform as RectTransform;
		GridLayoutGroup grid = GetComponent<GridLayoutGroup> ();
		float cellHeight = rectTransform.rect.height / 3f;
		grid.cellSize = new Vector2 (cellHeight * 2, cellHeight); 

		rectTransform.offsetMax = new Vector2 ((grid.cellSize.x * levelCount) / 3 + grid.cellSize.x, rectTransform.offsetMax.y);
		for (int i = 0; i < levelCount; i++) {
			Button buttonInstance = Instantiate(buttonPrefab) as Button;
			buttonInstance.transform.SetParent(this.transform);
			buttonInstance.transform.localScale = Vector3.one;
			Text[] texts = buttonInstance.GetComponentsInChildren<Text>();
			texts[0].text = "Level " + (i + 1).ToString();
			texts[1].text = "Time";
            texts[2].text = PlayerPrefs.GetFloat("Level" + (i + 1).ToString() + "Time", 0.00f).ToString("0.00");
			int levelIndex = i + 1;
			buttonInstance.onClick.AddListener(delegate {
				SceneManager.LoadScene(levelIndex);
			});
		}
	}
}
