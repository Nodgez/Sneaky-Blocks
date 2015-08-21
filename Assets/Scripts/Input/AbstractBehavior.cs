using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {
	
	public Buttons[] inputButtons;
	public MonoBehaviour[] disabledScripts;
	
	protected InputState inputState;
	protected Rigidbody2D body2D;
	
	protected virtual void Awake()
	{
		inputState = GetComponent<InputState> ();
		body2D = GetComponent<Rigidbody2D> ();
	}
	
	protected virtual void ToggleScripts(bool value)
	{
		foreach (MonoBehaviour script in disabledScripts)
			script.enabled = value;
	}
}
