using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class FindPathBehavior : AbstractBehavior {

	NavMeshAgent agent;
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
			if(Physics.Raycast(ray, out hit))
				agent.SetDestination(hit.point);
		}
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
			Vector3 direction = agent.destination - transform.position;
			return direction.normalized;
		}

	}
}