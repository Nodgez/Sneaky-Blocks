using UnityEngine;
using System.Collections;

public class GameObjectDestroyer : MonoBehaviour {

	public GameObject[] GameObjectsToDisable;
	public Trigger trigger;

	void Start()
	{
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent triggerEvent;
		eventPool.GetEventFromPool (trigger.eventKey, out triggerEvent);
		triggerEvent.onHandleEvent += Remove;
	}

	public void Remove()
	{
		foreach (GameObject go in GameObjectsToDisable)
			Destroy (go);
	}
}
