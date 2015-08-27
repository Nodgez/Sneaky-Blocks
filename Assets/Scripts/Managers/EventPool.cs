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

	public void AddEventToPool(string key, GameEvent gameEvent)
	{
		if (!eventDictionary.ContainsKey (key))
			eventDictionary.Add (key, gameEvent);
		else
			eventDictionary [key] = gameEvent;
	}

	public GameEvent GetEventFromPool(string key)
	{
		if (eventDictionary.ContainsKey (key))
			return eventDictionary [key];
		else
			return null;
	}
}
