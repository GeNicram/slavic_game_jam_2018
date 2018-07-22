using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitersInput : MonoBehaviour {
    public float deadzone = 1.0f;
	public Waiter[] waiters;
	
	private void Update()
    {
		for (int i = 0; i < waiters.Length; i++)
		{
			ProcessInputForWaiter(i);
		}
	}

    private void FixedUpdate()
    {
        for (int i = 0; i < waiters.Length; i++)
        {
            ProcessMovementInputForWaiter(i);
        }
    }

    void ProcessMovementInputForWaiter(int waiter_index)
    {
        var current_waiter = waiters[waiter_index];
        string vertical_input_name = "Vertical" + waiter_index.ToString();
        string horizontal_input_name = "Horizontal" + waiter_index.ToString();
        float vertical_input = Input.GetAxisRaw(vertical_input_name);
        float horizontal_input = Input.GetAxisRaw(horizontal_input_name);
        Vector2 input_vector = new Vector2(horizontal_input, vertical_input);

        if (input_vector.magnitude <= deadzone)
        {
            input_vector = Vector2.zero;
        }

        current_waiter.ProcessInput(input_vector.normalized);
    }

    void ProcessInputForWaiter(int waiter_index)
	{
		var current_waiter = waiters[waiter_index];
        string dash_input_name = "Dash" + waiter_index.ToString();
        string dish_action_name = "DishAction" + waiter_index.ToString();
        string keep_dish = "KeepDish" + waiter_index.ToString();
        string throw_dish = "ThrowDish" + waiter_index.ToString();

        string vertical_input_name = "Vertical" + waiter_index.ToString();
        string horizontal_input_name = "Horizontal" + waiter_index.ToString();
        float vertical_input = Input.GetAxisRaw(vertical_input_name);
        float horizontal_input = Input.GetAxisRaw(horizontal_input_name);
        Vector2 input_vector = new Vector2(horizontal_input, vertical_input);

        if (Input.GetButtonDown(dish_action_name))
        {
            current_waiter.ProcessDishInput();
        }

        if (Input.GetButtonDown(keep_dish))
        {
            current_waiter.ProcessKeepDish();
        }

        if (Input.GetButtonDown(throw_dish))
        {
            current_waiter.ProcessThrowDish();
        }
        
        if (Input.GetButtonUp(dash_input_name))
        {
            current_waiter.Dash(input_vector.normalized);
        } 
    }
}
