using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject levelsMenu;
	public GameObject optionsMenu;

	public void ChangeActiveMenu(int index)
	{
		levelsMenu.SetActive (false);
		optionsMenu.SetActive (false);

		switch (index) {
		case 1:
			levelsMenu.SetActive(true);
			break;
		case 2:
			optionsMenu.SetActive(true);
			break;
		}
	}
}

public enum ActiveMenu
{
	Main = 0,
	Level = 1,
	Option = 2
}
