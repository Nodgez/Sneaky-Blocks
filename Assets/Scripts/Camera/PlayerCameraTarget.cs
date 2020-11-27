using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour, ICameraTarget
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
