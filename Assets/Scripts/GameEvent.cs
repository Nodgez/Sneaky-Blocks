using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnHandleEvent();

public class GameEvent {

	public event OnHandleEvent onHandleEvent;
	private List<ITrigger> eventTriggers = new List<ITrigger>();

	public void CheckEventTriggered () {
		foreach (ITrigger eventTrigger in eventTriggers) {
			if (eventTrigger.IsTriggered) {

				if(onHandleEvent != null)
					onHandleEvent();

				break;
			}
		}
	}

	public void AddEventTrigger(ITrigger trigger)
	{
		eventTriggers.Add (trigger);
	}
}
