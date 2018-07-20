using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public float speed;

	protected Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Dash(Vector2 normalized_input)
    {
        body.AddForce(normalized_input * 155550);
    }


    public void ProcessInput(Vector2 normalized_input)
	{
     
		if (normalized_input.magnitude == 0) return;
		body.AddForce(normalized_input * speed);
	}
}
