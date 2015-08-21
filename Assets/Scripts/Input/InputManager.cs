using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public InputAxis[] axies;
	public InputState inputState;

	void Update () {
		foreach (InputAxis axis in axies)
			inputState.SetButton (axis.button,axis.State);
	}
}

[System.Serializable]
public struct InputAxis
{
	public string axisName;
	public float offValue;
	public Condition condition;
	public Buttons button;

	public bool State
	{
		get
		{
			float val = Input.GetAxis(axisName);

			switch(condition)
			{
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}

			return false;
		}
	}
}

public enum Condition
{
	GreaterThan = 0,
	LessThan = 1
}

public enum Buttons
{
	SpaceBar = 0,
	Back = 1,
	Forward = 2,
	Tap = 3
}
