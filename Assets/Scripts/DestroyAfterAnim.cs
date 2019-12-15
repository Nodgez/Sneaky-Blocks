using UnityEngine;
using System.Collections;

public class DestroyAfterAnim : MonoBehaviour {

    private Animator anim;
    public float length;

	void Start () {
        anim = GetComponent<Animator>();
        Destroy(gameObject, length);
    }
}
