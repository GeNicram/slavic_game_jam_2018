using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public float speed;

    public int incorrectDishPenalty = 10;
    public int incorrectDishPenaltyDeviation = 5;

    protected Rigidbody2D body;

    private int m_collectedTip = 0;
    private int carriedDishType = -1;

    private List<Table> tablesInRange = new List<Table>();
    private List<DishPickup> dishPickupsInRange = new List<DishPickup>();

    private bool canDash = true;
    public float dashCooldown = 0.5f;

    public GameObject waiter_face;
    public GainPoints waiter_gain_points_particle;

    public int collectedTip
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
        if (canDash)
        {
            canDash = false;
            body.AddForce(normalized_input * 155550);
            StartCoroutine(DashDelayCoroutine());
        }
    }

    IEnumerator DashDelayCoroutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;  
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
                if (closestTable.Serve(carriedDishType))
                {
                    m_collectedTip += closestTable.CollectTip();

                    GainPoints new_particle = Instantiate(waiter_gain_points_particle,
                        transform);
                    Vector2 particles_destination = new Vector2(5, 1);
                    if (waiter_face != null) {
                        particles_destination = waiter_face.transform.position;
                    }
                    else
                        Debug.Log("Please set waiters its faces from GUI");

                    new_particle.destination = particles_destination;
                }
                else
                {
                    int penalty = Random.Range(incorrectDishPenalty, incorrectDishPenalty + incorrectDishPenaltyDeviation);
                    m_collectedTip -= penalty;
                    if (m_collectedTip < 0)
                    {
                        m_collectedTip = 0;
                    }
                }

                RemoveDish();
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

    /*void OnGUI()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), "Carrying dish type: " + carriedDishType + "\nCollected tip: " + collectedTip, style);
    }*/
}
