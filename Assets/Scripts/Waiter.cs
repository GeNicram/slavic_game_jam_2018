using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public float speed;

    public int incorrectDishPenalty = 10;
    public int incorrectDishPenaltyDeviation = 5;

    protected Rigidbody2D body;

    private int m_collectedTip = 0;
    private Dish dish;

    private List<Table> tablesInRange = new List<Table>();
    private List<Waiter> waitersInRange = new List<Waiter>();
    private List<DishPickup> dishPickupsInRange = new List<DishPickup>();

    private bool canDash = true;
    public float dashCooldown = 0.5f;

    private float current_stun_time = 0;
    public float total_stun_time;
    private int stun_button_counter;
    public int button_pushes_to_keep_dish;
    private bool keep_dish_after_stun;
    private BubbleAnimate bubble;

    public GameObject waiter_face;
    public GainPoints waiter_gain_points_particle;
    public Color waiter_color;

    public int collectedTip
    {
        get { return m_collectedTip; }
    }

    // Use this for initialization
    void Start () {
		body = GetComponent<Rigidbody2D>();
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
        if (current_stun_time > 0)
        {
            current_stun_time -= Time.deltaTime;
            return;
        }
		body.AddForce(normalized_input * speed);
	}

    public bool IsCarryingDish()
    {
        return dish != null;
    }

	private void ThrowDish()
	{
<<<<<<< Updated upstream
        dish.Abandon();
        RemoveDish();
=======
		dish.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(body.velocity.x, body.velocity.y));
		dish = null;
>>>>>>> Stashed changes
	}

	private void RemoveDish()
    {
        dish = null;
    }

	private void ReclaimDish()
	{
		Dish dishToSteal = null;
		int waiterTested = 0;
		do
		{
			if (waiterTested >= waitersInRange.Count) return;
			dishToSteal = waitersInRange[waiterTested].dish;
			waiterTested++;
		} while (dishToSteal == null);
		
		waitersInRange[waiterTested].dish = null;
		dish = dishToSteal;
		dish.Transfer(transform);
	}

	private void DropDish()
    {
        current_stun_time = total_stun_time;
        keep_dish_after_stun = false;
        stun_button_counter = 0;

        StartCoroutine(TryToKeepDish(current_stun_time));
        if (IsCarryingDish()) {
            bubble = BubbleManager.SpawnBubble(BubbleManager.Bubble.pushB,
                new Vector2(transform.position.x + 0.5f, transform.position.y + 0.3f),
                current_stun_time);
        }
    }

    private IEnumerator TryToKeepDish(float time_to_react)
    {
        yield return new WaitForSeconds(time_to_react);

        if (!keep_dish_after_stun) {
            ThrowDish();
        }
    }

    public void ProcessDishInput()
    {
        if (IsCarryingDish())
	    {
            Table closestTable = null;
            foreach (var table in tablesInRange)
	        {
                if (table.isWaitingForDish) {
                    closestTable = table;
                }
            }

            if (closestTable != null)
	        {
                Debug.Assert(closestTable.isWaitingForDish);
                if (closestTable.Serve(dish))
                {
                    int got_tip = closestTable.CollectTip();
                    m_collectedTip += got_tip;

                    GainPoints new_particle = Instantiate(waiter_gain_points_particle,
                        transform.position, new Quaternion());
                    Vector2 particles_destination = new Vector2(5, 1);
                    if (waiter_face != null)
                    {
                        particles_destination = waiter_face.transform.position;
                    }
                    else
                        Debug.Log("Please set waiters its faces from GUI");

                    new_particle.destination = particles_destination;
                    new_particle.color = waiter_color;
                    new_particle.number_of_points = got_tip;
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
            else
            {
				ReclaimDish();
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
                
                dish = closestDishPickup.PickUp();
                dish.Transfer(GetComponent<Transform>().Find("DishPivot"));

                if (bubble)
                    bubble.Fadeout();
            }
        }
    }

    public void ProcessKeepDish()
    {
        if (current_stun_time > 0)
        {
            stun_button_counter += 1;
            if (stun_button_counter >= button_pushes_to_keep_dish) {
                keep_dish_after_stun = true;
                current_stun_time = 0;
                if (bubble)
                    bubble.Fadeout();
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
        else if (collision.CompareTag("Waiter"))
        {
            Waiter waiter = collision.GetComponentInParent<Waiter>();
			if (waiter == this) return;
            waitersInRange.Add(waiter);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5) {
            if (collision.collider.CompareTag("Obstacle"))
                DropDish();
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
        else if (collision.gameObject.CompareTag("Waiter"))
        {
            Waiter waiter = collision.GetComponentInParent<Waiter>();
            waitersInRange.Remove(waiter);
        }
	}
}
