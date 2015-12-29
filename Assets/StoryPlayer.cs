using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryPlayer : MonoBehaviour {

    public Sprite[] storyClips;
    public float clipTime;
    public FadePanel fadePanel;
    public Image img;

    private float _timer;
    private int _clipIndex;

	void Start () {
        fadePanel.onFadeIn += SwapImage;
        img.sprite = storyClips[_clipIndex];
    }

    // Update is called once per frame
    void Update () {

        if (fadePanel.GetFadeState == FadeState.Idle)
            _timer += Time.deltaTime;

        if (_timer >= clipTime)
        {
            fadePanel.StartFade();
            _timer = 0;
        }
	}

    public void SwapImage()
    {
        _clipIndex++;
        if (_clipIndex > storyClips.Length - 1)
        {
            Application.LoadLevel(Application.levelCount - 1);
            return;
        }
        img.sprite = storyClips[_clipIndex];
    }
}
