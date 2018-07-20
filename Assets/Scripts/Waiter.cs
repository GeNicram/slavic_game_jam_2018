using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public float speed;

	protected Rigidbody2D body;

    private uint collectedTip = 0;
    private int carriedDishType = -1;
    private Table closestTable = null;
    private DishPickup closestDishPickup = null;

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

    private bool IsCarryingDish()
    {
        return carriedDishType != -1;
    }

    private void RemoveDish()
    {
        carriedDishType = -1;
    }

    public void ProcessDishInput()
    {
        if (IsCarryingDish())
        {
            if (closestTable != null && closestTable.isWaitingForDish)
            {
                closestTable.Serve(carriedDishType);
                RemoveDish();
                collectedTip += closestTable.CollectTip();
            }
        }
        else
        {
            if (closestDishPickup != null && closestDishPickup.hasDish)
            {
                carriedDishType = closestDishPickup.PickUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Table"))
        {
            closestTable = collision.GetComponent<Table>();
        }
        else if (collision.CompareTag("DishPickup"))
        {
            closestDishPickup = collision.GetComponent<DishPickup>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            closestTable = null;
        }
        else if (collision.gameObject.CompareTag("DishPickup"))
        {
            closestDishPickup = null;
        }
    }

    void OnGUI()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), "Carrying dish type: " + carriedDishType + "\nCollected tip: " + collectedTip, style);
    }
}
