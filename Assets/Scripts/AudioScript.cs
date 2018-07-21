using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour {

    public AudioMixerSnapshot MenuTheme;
    public AudioMixerSnapshot GameTheme;
    public AudioMixer mixer;
    public AudioClip menu;
    public AudioClip game;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(this.gameObject);

    }

    public void TransitionToGame()
    {
        GameTheme.TransitionTo(1f);
    }

    public void TransitionToMenu()
    {
        MenuTheme.TransitionTo(1f);
        
    }

}
