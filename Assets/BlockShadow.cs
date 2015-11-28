using UnityEngine;
using System.Collections;

public class BlockShadow : MonoBehaviour {

    private Transform target;
    private Light directionalLight;
    
    void Start () {
        directionalLight = GameObject.FindObjectOfType<Light>();
        transform.localPosition = Vector3.zero;
        target = transform.parent;
        transform.parent = null;
	}
	
	void Update () {
        Vector3 dir = directionalLight.transform.forward;
        transform.position = target.position + new Vector3(dir.x, 0, dir.z) * 0.75f;
        Vector3 eulers = target.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulers.x + 90, eulers.y, eulers.z);

        gameObject.SetActive(target.gameObject.activeSelf);

	}
}
