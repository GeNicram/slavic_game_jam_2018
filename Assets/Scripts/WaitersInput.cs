using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitersInput : MonoBehaviour {

	public Waiter[] waiters;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < waiters.Length; i++)
		{
			ProcessInputForWaiter(i);
		}
	}

	void ProcessInputForWaiter(int waiter_index)
	{
		//TODO make it work for multiple pads
		var current_waiter = waiters[waiter_index];
		float vertical_input = Input.GetAxis("Vertical");
		float horizontal_input = Input.GetAxis("Horizontal");
		Vector2 input_vector = new Vector2(horizontal_input, vertical_input);
		current_waiter.ProcessInput(input_vector.normalized);
	}
}
