using UnityEngine;

public interface ICameraTarget
{
	int CameraPriority { get; }

	T ConvertToComponent<T>() where T : MonoBehaviour;
}
