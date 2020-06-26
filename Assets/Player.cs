using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICameraMoveTo
{
	[SerializeField]
	private int _cameraPriority = 0;
	public int CameraPriority
	{
		get
		{
			return _cameraPriority;
		}
	}

	public T ConvertToComponent<T>() where T : MonoBehaviour
	{
		var component = this.GetComponent<T>();
		return component;
	}
}
