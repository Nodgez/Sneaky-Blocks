using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventPool : MonoBehaviour {

	private Dictionary <string, GameEvent> _eventDictionary = new Dictionary<string, GameEvent>();

	void Awake () {
		GameEvent playerFound = new GameEvent ();
		GameEvent endOfLevel = new GameEvent ();

		AddEventToPool ("Player Found", ref playerFound);
		AddEventToPool ("End Of Level", ref endOfLevel);
	}
	
	void Update () {
		foreach (string s in _eventDictionary.Keys)
			_eventDictionary [s].CheckEventTriggered ();
	}

	public void AddEventToPool(string key, ref GameEvent gameEvent)
	{
		if (!_eventDictionary.ContainsKey (key))
			_eventDictionary.Add (key, gameEvent);
		else
			_eventDictionary [key] = gameEvent;
	}

	public void GetEventFromPool(string key, out GameEvent gameEvent)
	{
		if (_eventDictionary.ContainsKey (key))
			gameEvent = _eventDictionary [key];
		else
			gameEvent = null;
	}

	public void AddTriggerToEvent(string key, ITrigger trigger)
	{
		if (_eventDictionary.ContainsKey (key))
			_eventDictionary [key].AddEventTrigger (trigger);
	}
}
