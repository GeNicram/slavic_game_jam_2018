using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitersInput : MonoBehaviour {

	public Waiter[] waiters;
    bool dash = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < waiters.Length; i++)
		{
            if (Input.GetKeyUp("joystick button " + i.ToString())) 
           {
                dash = true;
                Debug.Log("dupa");
            }
			ProcessInputForWaiter(i);
		}
	}

   

	void ProcessInputForWaiter(int waiter_index)
	{
		//TODO make it work for multiple pads
		var current_waiter = waiters[waiter_index];
		string vertical_input_name = "Vertical" + waiter_index.ToString();
		string horizontal_input_name = "Horizontal" + waiter_index.ToString();
        string dash_input_name = "Fire" + waiter_index.ToString();
        float vertical_input = Input.GetAxis(vertical_input_name);
		float horizontal_input = Input.GetAxis(horizontal_input_name);
		Vector2 input_vector = new Vector2(horizontal_input, vertical_input);
		current_waiter.ProcessInput(input_vector.normalized);

        if (Input.GetButtonDown("DishAction" + waiter_index.ToString()))
        {
            current_waiter.ProcessDishInput();
        }

        if (dash)
        {
            current_waiter.Dash(input_vector.normalized);
            dash = false;
        }     
    }
}
