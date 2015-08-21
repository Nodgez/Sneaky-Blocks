using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DetectionManager : MonoBehaviour {

	private List<IDetect> detectors = new List<IDetect>();
	public GameObject detectionTarget;
	public string objectsTag;

	void Start () {
		GameObject[] detectorObjects = GameObject.FindGameObjectsWithTag (objectsTag);
		for (int i = 0; i < detectorObjects.Length; i++) {
			IDetect localDetectors = detectorObjects[i].GetComponent(typeof(IDetect)) as IDetect;
//			foreach(IDetect detector in localDetectors)
				detectors.Add(localDetectors);
		}
	}
	
	void Update () {
		foreach (IDetect detector in detectors) {
			detector.DetectUnit(detectionTarget.transform.position);
		}
	}
}
