using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackingTrackManager : MonoBehaviour {

	public AudioClip[] backingTracks;

	private AudioSource audioSource;

	private static BackingTrackManager _instance;

	// Use this for initialization
	void Start () {

		if (_instance == null) {
			_instance = this;
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = backingTracks[0];
            audioSource.Play();
		}
		else if (this != _instance)
			Destroy (this);

		DontDestroyOnLoad (this);

        if (PlayerPrefs.GetInt("MusicOn") == 0)
            audioSource.volume = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("MusicOn") == 0)
            audioSource.volume = 0;
        else audioSource.volume = 1;
    }

	public void ChangeTrack(int index)
	{
		audioSource.clip = backingTracks [index];
	}
}
