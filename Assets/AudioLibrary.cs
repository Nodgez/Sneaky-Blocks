using UnityEngine;
using System.Collections;

public class AudioLibrary : MonoBehaviour {

    public AudioClip[] audioClips;

    public void PlayClip(AudioEffects effect)
    {
        if (GameObject.Find(effect.ToString()) != null || PlayerPrefs.GetInt("AudioOn") == 0)
            return;
        GameObject clip = new GameObject(effect.ToString());
        AudioSource audioS = clip.AddComponent<AudioSource>();
        audioS.PlayOneShot(audioClips[(int)effect]);
        Destroy(clip, audioClips[(int)effect].length);
    }
	
}

public enum AudioEffects
{
    Accept = 0,
    Decline = 1,
    Detected = 2,
    Switch = 3,
    Teleport = 4,
    Win = 5
}
