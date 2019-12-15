using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FindPathBehavior : AbstractBehavior {

	NavMeshAgent agent;
    public GameObject pointer;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		inputState.GetButtonValue (Buttons.Tap, out var tapped);
		if (tapped)
		{
			Ray ray = Camera.main.ScreenPointToRay(inputState.gesturePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer != LayerMask.NameToLayer("ShadowLayer"))
			{
				agent.isStopped = false;
				agent.destination = hit.point;
			}
			else if (hit.collider != null)
			{
				Vector3 dir = hit.point - agent.transform.position;
				Vector3 newDestination = hit.point - dir.normalized;
				agent.isStopped = false;
				agent.destination = hit.point;
			}
		}
		else if (!tapped)
			agent.isStopped = true;
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