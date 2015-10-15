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

		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		GameEvent playerFoundEvent;
		eventPool.GetEventFromPool ("Player Found", out playerFoundEvent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeTrack(int index)
	{
		audioSource.clip = backingTracks [index];
	}
}
