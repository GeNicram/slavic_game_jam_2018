using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public float speed;

	protected Rigidbody2D body;

    private uint m_collectedTip = 0;
    private int carriedDishType = -1;

    private List<Table> tablesInRange = new List<Table>();
    private List<DishPickup> dishPickupsInRange = new List<DishPickup>();

    public uint collectedTip
    {
        get { return m_collectedTip; }
    }

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
            Table closestTable = null;
            foreach (var table in tablesInRange)
            {
                if (table.isWaitingForDish)
                {
                    closestTable = table;
                }
            }

            if (closestTable != null)
            {
                Debug.Assert(closestTable.isWaitingForDish);
                closestTable.Serve(carriedDishType);
                RemoveDish();
                m_collectedTip += closestTable.CollectTip();
            }
        }
        else
        {
            Vector3 pos = GetComponent<Transform>().position;
            DishPickup closestDishPickup = null;
            float closestDishPickupDistance = float.MaxValue;
            foreach (var dishPickup in dishPickupsInRange)
            {
                float distance = (dishPickup.GetComponent<Transform>().position - pos).sqrMagnitude;
                if (dishPickup.hasDish && distance < closestDishPickupDistance)
                {
                    closestDishPickup = dishPickup;
                    closestDishPickupDistance = distance;
                }
            }

            if (closestDishPickup != null)
            {
                Debug.Assert(closestDishPickup.hasDish);
                carriedDishType = closestDishPickup.PickUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Table"))
        {
            Table table = collision.GetComponent<Table>();
            tablesInRange.Add(table);
        }
        else if (collision.CompareTag("DishPickup"))
        {
            DishPickup pickup = collision.GetComponent<DishPickup>();
            dishPickupsInRange.Add(pickup);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            Table table = collision.GetComponent<Table>();
            tablesInRange.Remove(table);
        }
        else if (collision.gameObject.CompareTag("DishPickup"))
        {
            DishPickup pickup = collision.GetComponent<DishPickup>();
            dishPickupsInRange.Remove(pickup);
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
