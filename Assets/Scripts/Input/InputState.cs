using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InputState : MonoBehaviour {

	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();
	public Vector3 gesturePosition = Vector3.zero;

	void Update()
	{
		gesturePosition = Input.mousePosition;
	}

	public void SetButton(Buttons key,bool value)
	{
		if (!buttonStates.ContainsKey (key))
			buttonStates.Add (key, new ButtonState ());

		ButtonState buttonState = buttonStates [key];

		if (buttonState.value && !value)
			buttonState.holdTime = 0;
		else if (buttonState.value && value)
			buttonState.holdTime += Time.deltaTime;
		buttonState.value = value;

	}

	public void GetButtonValue(Buttons key, out bool value)
	{
		if (buttonStates.ContainsKey (key))
			value = buttonStates [key].value;
		else {
			Debug.Log(key + " Not Mapped");
			value = false;
		}
	}

	public void GetButtonHoldTime(Buttons key, out float holdTime)
	{
		if (buttonStates.ContainsKey (key))
			holdTime = buttonStates [key].holdTime;
		else {
			Debug.Log("Not Mapped");
			holdTime = 0;
		}
	}
}

public class ButtonState
{
	public bool value;
	public float holdTime = 0;
}
