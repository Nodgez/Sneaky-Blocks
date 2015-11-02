using UnityEngine;
using System.Collections;

public class SpringCamera : MonoBehaviour {

    public FindPathBehavior findPathBehavior;
    public SpringReference springReference;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = springReference.Direction.normalized * -1;
        float dis = springReference.Distance;
        Vector3 anchorPos = new Vector3(findPathBehavior.transform.position.x,
                                        10,
                                        findPathBehavior.transform.position.z);
        transform.position = anchorPos + direction * dis;

    }
}
