using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public float dishGenerationDelay = 2;
    public float maxRandomDelayDeviation = 0;
    public GameObject tipEffect;
    public SpeechBubble speechBubble;
	public Client client;
    public SpriteRenderer exclamation;

    private int m_tip = 0;
    private int m_desiredDishType = 0;
    private bool m_isWaitingForDish = false;
    private bool m_didLeaveCorrectDish = true;
    public AudioSource requestPop;
    public AudioClip pop;
    public AudioClip wrongDish;
    public AudioClip properDish;
    float requestDelay = 3f;

    private float m_patience;

    public int desiredDishType
    {
        get { return m_desiredDishType; }
        set { m_desiredDishType = value; }
    }

    public bool isWaitingForDish
    {
        get { return m_isWaitingForDish; }
    }
    
    private void SetRandom()
    {
        desiredDishType = Random.Range(0, Common.dishTypeCount);
        speechBubble.dishType = desiredDishType;
        m_isWaitingForDish = true;
        requestPop.PlayOneShot(pop);
        m_patience = 15.0f;
    }

    private void Start()
    {
        Debug.Assert(speechBubble != null, "Link the table to a speech bubble.");
        m_tip = 0;
        m_isWaitingForDish = false;
        StartCoroutine(RequestDelayCoro());
    }

    private void Update()
    {
        m_patience -= Time.deltaTime;

        if (isWaitingForDish)
        {
            if (m_patience < 5.0f)
            {
                exclamation.enabled = (Mathf.FloorToInt(m_patience * 3.0f) % 2 == 0);
            }
            else
            {
                exclamation.enabled = false;
            }
			if (m_patience < 0.0f)
			{
				client.ExpressDissapointment();
				m_tip = 0;
				m_isWaitingForDish = false;
				StartCoroutine(RequestDelayCoro());
			}
        }
        else
        {
            exclamation.enabled = false;
        }

        if (m_patience <= 0.0f)
        {
            m_desiredDishType = -1;
            speechBubble.dishType = -1;
        }
    }

    IEnumerator RequestDelayCoro()
    {
        yield return new WaitForSeconds(requestDelay + Random.Range(0f,10f));
        SetRandom();
    }

    public void LeaveTip(int tip)
    {
        // Tip effect disabled
        //GameObject effect = Instantiate(tipEffect, GetComponent<Transform>().position, Quaternion.identity);
        //effect.GetComponent<TipEffect>().tip = tip;
        m_tip += tip;
    }

    public int CollectTip()
    {
        int tip = m_tip;
        m_tip = 0;
        return tip;
    }

    public bool Serve(Dish dish)
    {
        dish.Transfer(GetComponent<Transform>().Find("DishPivot"));
        bool didLeaveCorrectDish = (dish.type == desiredDishType);
        if (didLeaveCorrectDish)
        {
            int calculate_tip = Random.Range(1, 3) + (int)Mathf.Max(0, m_patience);
            LeaveTip(calculate_tip);
            requestPop.PlayOneShot(properDish);
        }
        else {
            requestPop.PlayOneShot(wrongDish);
        }
        m_didLeaveCorrectDish = didLeaveCorrectDish;
        StartCoroutine(PostServeCoroutine());
        StartCoroutine(ConsumeDishCoroutine(dish));
        speechBubble.dishType = -1;
        return didLeaveCorrectDish;
    }

    IEnumerator PostServeCoroutine()
    {
        m_isWaitingForDish = false;
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0,maxRandomDelayDeviation));
        SetRandom();
    }

    IEnumerator ConsumeDishCoroutine(Dish dish)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(dish.gameObject);
    }
}
