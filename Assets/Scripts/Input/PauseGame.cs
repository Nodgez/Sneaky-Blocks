﻿using UnityEngine;
using System.Collections;

public class PauseGame : AbstractBehavior {

    public GameObject pauseMenu;

    private bool previousButtonValue = false;

	void Start () {
	
	}
	
	void Update () {

        bool buttonValue = false;
        inputState.GetButtonValue(Buttons.Back, out buttonValue);

        if (previousButtonValue == buttonValue)
            return;

        previousButtonValue = buttonValue;

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



    public void TogglePauseGame()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
