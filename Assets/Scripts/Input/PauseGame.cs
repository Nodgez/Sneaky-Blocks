using UnityEngine;
using System.Collections;

public class PauseGame : AbstractBehavior {

    public GameObject pauseMenu;

    private bool previousButtonValue = false;

	void Start () {
	
	}
	
	void Update () {

        bool buttonValue = false;
        inputState.GetButtonValue(Buttons.Back, out buttonValue);

        Debug.Log("Current : " + buttonValue);

        if (previousButtonValue == buttonValue)
            return;

        previousButtonValue = buttonValue;
        Debug.Log("Previous : " + previousButtonValue);

        if (buttonValue && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        else if (buttonValue && pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }	
	}

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
