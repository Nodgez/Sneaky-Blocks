using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour {

	public int levelCount;
	public Button buttonPrefab;

	void Start () {
		RectTransform rectTransform = transform as RectTransform;
		GridLayoutGroup grid = GetComponent<GridLayoutGroup> ();
		float cellHeight = rectTransform.rect.height / 3;
		grid.cellSize = new Vector2 (cellHeight * 2, cellHeight); 

		rectTransform.offsetMax = new Vector2 ((grid.cellSize.x * levelCount) / 3, rectTransform.offsetMax.y);
		for (int i = 0; i < levelCount; i++) {
			Button buttonInstance = Instantiate(buttonPrefab) as Button;
			buttonInstance.transform.SetParent(this.transform);
			buttonInstance.transform.localScale = Vector3.one;
			Text[] texts = buttonInstance.GetComponentsInChildren<Text>();
			texts[0].text = "Level " + (i + 1).ToString();
			texts[1].text = "Time : 0:00";
			int levelIndex = i + 1;
			buttonInstance.onClick.AddListener(delegate {
				Application.LoadLevel(levelIndex);
			});
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
