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
        float alpha = 0;

        if (passed_time < fade_in) {
            alpha = Mathf.Sin(passed_time / fade_in * Mathf.PI / 2);
        }

        SetAlpha(Mathf.Sin(passed_time));
        passed_time += Time.deltaTime;
	}

    public void Fadeout()
    {
        Destroy(this.gameObject);
    }

    private void SetAlpha(float value)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, value);
    }
}
