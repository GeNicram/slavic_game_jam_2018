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
		
	}

    public void TransitionToGame()
    {
    }

    public void TransitionToMenu()
    {

    }

}
