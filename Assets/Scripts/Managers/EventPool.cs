using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///A manager for all events 
/// </summary>
public class EventPool : MonoBehaviour {
	
	public string[] eventNames;
	private Dictionary <string, GameEvent> _eventDictionary = new Dictionary<string, GameEvent>();

	void Awake () {
		for (int i = 0; i < eventNames.Length; i++) {
			GameEvent gameEvent = new GameEvent();
			AddEventToPool (eventNames[i], ref gameEvent);
		}
	}

	void Start()
	{
		//ensures that the time scale has been reset N.B.* should be moved somewhere more meaningful
		Time.timeScale = 1;
	}
	
	void Update () {
		foreach (string s in _eventDictionary.Keys)
			_eventDictionary [s].CheckEventTriggered ();
	}

	//Adds a game event to the collection of events if it does not exist
	//or replaces the event with the Key if it does exist
	public void AddEventToPool(string key, ref GameEvent gameEvent)
	{
		if (!_eventDictionary.ContainsKey (key))
			_eventDictionary.Add (key, gameEvent);
		else
			_eventDictionary [key] = gameEvent;
	}

	//Out's a game event if it exists otherwise it will out null
	public void GetEventFromPool(string key, out GameEvent gameEvent)
	{
		if (_eventDictionary.ContainsKey (key))
			gameEvent = _eventDictionary [key];
		else
			gameEvent = null;
	}

	//Adds a trigger to the event of your choice
	public void AddTriggerToEvent(string key, Trigger trigger)
	{
		if (_eventDictionary.ContainsKey (key)) {
			_eventDictionary [key].AddEventTrigger (trigger);
			Debug.Log(trigger.ToString() + " added to " + key);

		}
	}
}
