using UnityEngine;
using System.Collections;

public class ProximityManager : MonoBehaviour {

	private ProximityDetector[] detectors;
	public Transform targetTransform;

	void Start () {
		detectors = GameObject.FindObjectsOfType<ProximityDetector> ();
	}
	
	void Update () {
		if (detectors == null)
			return;

		foreach (ProximityDetector pd in detectors)
		{
			pd.Detect (targetTransform.position);
		}
	}
}
