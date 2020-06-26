using UnityEngine;

public interface ICameraMoveTo
{
	int CameraPriority { get; }

	T ConvertToComponent<T>() where T : MonoBehaviour;
}
