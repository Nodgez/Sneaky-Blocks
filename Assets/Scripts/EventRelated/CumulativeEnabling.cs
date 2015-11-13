using UnityEngine;
using System.Collections;

public class CumulativeEnabling : MonoBehaviour {

	public GameObject[] objectsToEnable;
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
			for (int i = 0; i < objectsToEnable.Length; i++)
				objectsToEnable [i].SetActive (true);
	}
	

}
