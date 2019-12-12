using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FindPathBehavior : AbstractBehavior {

	NavMeshAgent agent;
    bool taplifted = true;
    public GameObject pointer;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool tapped = false;
		inputState.GetButtonValue (Buttons.Tap, out tapped);
		if (tapped) {
			Ray ray = Camera.main.ScreenPointToRay(inputState.gesturePosition);
			RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer != LayerMask.NameToLayer("ShadowLayer"))
            {
                agent.SetDestination(hit.point);

                if (taplifted)
                    Instantiate(pointer, hit.point, Quaternion.Euler(90, 0, 0));
            }
            else if (hit.collider != null)
            {
                Vector3 dir = hit.point - agent.transform.position;
                Vector3 newDestination = hit.point - dir.normalized;
                agent.SetDestination(newDestination);

                if (taplifted)
                {
                    Instantiate(pointer, newDestination, Quaternion.identity);
                }
            }
		}

        taplifted = !tapped;
    }

    public Vector3 Destination {
		get
		{
			return agent.destination;
		}
	}

	public Vector3 MoveDirection
	{
		get
		{
			Vector3 direction = agent.destination - new Vector3(transform.position.x,
			                                                    agent.destination.y,
			                                                    transform.position.z);
			return direction.normalized;
		}

	}
}