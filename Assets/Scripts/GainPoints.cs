using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPoints : MonoBehaviour {

    public int number_of_points;
    public Vector2 destination;
    public Color color;

    private Vector2 start_position;
    private float passed_time;
	// Use this for initialization
	void Start () {
        start_position = transform.position;
    
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = color;

        main.maxParticles = number_of_points;
    
        passed_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float progress = (passed_time < 0.7f ? 0 : (passed_time - 0.7f) / 0.3f) ;
    
        transform.position = Vector2.Lerp(start_position, destination, progress);
    
        passed_time += Time.deltaTime;
	}
}