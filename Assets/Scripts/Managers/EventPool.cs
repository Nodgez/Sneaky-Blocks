using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventPool : MonoBehaviour {

	private Dictionary <string, GameEvent> eventDictionary = new Dictionary<string, GameEvent>();

	void Start () {
		
	}
	
	void Update () {
		foreach (string s in eventDictionary.Keys)
			eventDictionary [s].CheckEventTriggered ();
	}

	public void AddEventToPool(string key, ref GameEvent gameEvent)
	{
		if (!eventDictionary.ContainsKey (key))
			eventDictionary.Add (key, gameEvent);
		else
			eventDictionary [key] = gameEvent;
	}

	public void GetEventFromPool(string key, out GameEvent gameEvent)
	{
		if (eventDictionary.ContainsKey (key))
			gameEvent = eventDictionary [key];
		else
			gameEvent = null;
	}
}
