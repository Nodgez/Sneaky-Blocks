using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ProximityDetector))]
[RequireComponent(typeof(Trigger))]
public class SwitchLever : MonoBehaviour, ICameraMoveTo {

	private ProximityDetector _proxDetector;
	private Trigger _switchTrigger;

	[SerializeField]
	private int _cameraPriority = 0;
	public int CameraPriority
	{
		get
		{
			return _cameraPriority;
		}
	}

	void Start () {
		_proxDetector = GetComponent<ProximityDetector> ();
		_switchTrigger = GetComponent<Trigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_proxDetector.DetectTargets ())
		{
			_switchTrigger.IsTriggered = true;
            BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Switch);
			this.gameObject.SetActive(false);
		}

        transform.Rotate(Vector3.up, Time.deltaTime * 100);
	}

	public T ConvertToComponent<T>() where T : MonoBehaviour
	{
		var component = this.GetComponent<T>();
		return component;
	}

}
