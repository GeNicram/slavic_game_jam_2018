using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimate : MonoBehaviour {
    private float fade_in = 0.1f;
    private float fade_out = 0.3f;


    public float duration;
    private float passed_time;
	// Use this for initialization
	void Start () {
        passed_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Fadeout()
    {
        Destroy(this.gameObject);
    }
}
