using UnityEngine;
using System.Collections;

public class CumulativeToggling : MonoBehaviour {

	public GameObject[] objectsToToggle;
	public string eventKey;
	public float increment;
	private float currentValue;

	void Start () {
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent gameEvent = new GameEvent ();
		eventPool.GetEventFromPool (eventKey, out gameEvent);
		gameEvent.onHandleEvent += AddValue;	
	}
	
	void AddValue()
	{
		currentValue += increment;
		if (currentValue >= 1)
			for (int i = 0; i < objectsToToggle.Length; i++)
				objectsToToggle [i].SetActive (!objectsToToggle[i].activeSelf);
	}
	

}
