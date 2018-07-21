using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

  public GainPoints point_prefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    if (Input.GetKeyDown(KeyCode.Space)) {
      GainPoints new_particle = Instantiate(point_prefab, new Vector2(5, 0), new Quaternion());
      new_particle.destination = new Vector2(Random.Range(-8, -5), Random.Range(-4, 4));
    }
		
	}
}
