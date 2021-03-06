﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TeleporterCheckpoint : BaseCheckpoint {

	private Transform playerTransform;

	public override bool ApplyCheckpointAction (Transform transform)
	{
		transform.position = AdjacentCheckpoint.Position;
		return true;
	}

	void Start()
	{
		playerTransform = GameObject.Find ("Player").transform;
	}

	void Update()
	{
		if (Vector3.Distance (playerTransform.position, this.transform.position) <= 1)
		{
			playerTransform.GetComponent<NavMeshAgent>().enabled = false;
			ApplyCheckpointAction (playerTransform);

			playerTransform.GetComponent<NavMeshAgent>().enabled = true;
			playerTransform.GetComponent<NavMeshAgent>().SetDestination(AdjacentCheckpoint.Position);

		}
	}
}
