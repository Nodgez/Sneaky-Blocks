using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void FadeEvent();

public class FadePanel : MonoBehaviour {

    public event FadeEvent onFadeIn;
    public event FadeEvent onFadeOut;

    public float fadeTime;
    public Image img;

    private FadeState _fadeState = FadeState.Idle;
    private float _timer;
    private Color color;
    private float _halfTime;
    private float numerator;

    void Start () {
        color = img.color;
        _halfTime = fadeTime / 2;
	}
	
	void Update () {
        if (_fadeState == FadeState.Idle)
            return;

        _timer += Time.deltaTime;
        numerator += Time.deltaTime * (int)_fadeState;
        if (_timer >= _halfTime && _fadeState == FadeState.Into)
        {
                if (onFadeIn != null)
                    onFadeIn();

                _fadeState = FadeState.Outto;
        }
        else if(_timer >= fadeTime && _fadeState == FadeState.Outto)
        {
            Debug.Log("Delta is : " + Time.deltaTime);
            if (onFadeOut != null)
                onFadeOut();

            _fadeState = FadeState.Idle;
            _timer = 0;
            numerator = 0;
        }

        float perc = Mathf.Clamp01(numerator / _halfTime);
        color.a = perc;
        img.color = color;
        Debug.Log("Color A : " + img.color.a);
	}

    public void StartFade()
    {
        _timer = 0;
        _fadeState = FadeState.Into;
    }

    public FadeState GetFadeState
    {
        get { return _fadeState; }
    }

}

public enum FadeState
{
    Into = 1,
    Idle = 0,
    Outto = -1,
}
