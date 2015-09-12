using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnHandleEvent();

public class GameEvent {

	public event OnHandleEvent onHandleEvent;
	private List<Trigger> eventTriggers = new List<Trigger>();

	public void CheckEventTriggered () {
		foreach (Trigger eventTrigger in eventTriggers) {
			if (eventTrigger.IsTriggered) {

				if(onHandleEvent != null)
					onHandleEvent();

				break;
			}
		}
	}

	public void AddEventTrigger(Trigger trigger)
	{
		eventTriggers.Add (trigger);
	}
}
