using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject levelsMenu;
	public GameObject optionsMenu;
    public GameObject creditsMenu;

    private int openMenu = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("AudioOn"))
            PlayerPrefs.SetInt("AudioOn", 1);
        if (!PlayerPrefs.HasKey("MusicOn"))
            PlayerPrefs.SetInt("MusicOn", 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void ChangeActiveMenu(int index)
	{
		levelsMenu.SetActive (false);
		optionsMenu.SetActive (false);
        creditsMenu.SetActive(false);

        if (openMenu > index)
            BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Decline);
        else
            BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Accept);

        openMenu = index;

        switch (index)
        {
		    case 1:
			    levelsMenu.SetActive(true);
			    break;
		    case 2:
			    optionsMenu.SetActive(true);
			    break;
            case 3:
                creditsMenu.SetActive(true);
                break;
		}
	}

    public void ToggleAudio()
    {
        string audioKey = "AudioOn";

        int val = 0;
        if (PlayerPrefs.HasKey(audioKey))
            val = PlayerPrefs.GetInt(audioKey);

        PlayerPrefs.SetInt(audioKey, Mathf.Abs(val - 1));
    }

    public void ToggleMusic()
    {
        string musicKey = "MusicOn";

        int val = 0;
        if (PlayerPrefs.HasKey(musicKey))
            val = PlayerPrefs.GetInt(musicKey);

        PlayerPrefs.SetInt(musicKey, Mathf.Abs(val - 1));

    }
}

public enum ActiveMenu
{
	Main = 0,
	Level = 1,
	Option = 2,
    Credits = 3
}
