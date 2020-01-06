using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICameraMoveTo
{
	public int CameraPriority	{ get; set; }

	void Start()
	{
		CameraPriority = 3;
	}
}
