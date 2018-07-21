using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public float dishGenerationDelay = 2;
    public float maxRandomDelayDeviation = 0;
    public GameObject tipEffect;
    public SpeechBubble speechBubble;

    private int m_tip = 0;
    private int m_desiredDishType = 0;
    private bool m_isWaitingForDish = false;
    private bool m_didLeaveCorrectDish = true;
    public AudioSource requestPop;
    public AudioClip pop; 
    float requestDelay = 3f;

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
        desiredDishType = Random.Range(0, Common.dishTypeCount - 1);
        speechBubble.dishType = desiredDishType;
    }

    private void Start()
    {
        Debug.Assert(speechBubble != null, "Link the table to a speech bubble.");
        m_tip = 0;
        SetRandom();
        m_isWaitingForDish = true;
        StartCoroutine(RequestDelayCoro());
    }

    IEnumerator RequestDelayCoro()
    {
        yield return new WaitForSeconds(requestDelay + Random.Range(0f,10f));
        desiredDishType = Random.Range(0, Common.dishTypeCount - 1);
        m_isWaitingForDish = true;
        requestPop.PlayOneShot(pop);
    }

    public void LeaveTip(int tip)
    {
        GameObject effect = Instantiate(tipEffect, GetComponent<Transform>().position, Quaternion.identity);
        effect.GetComponent<TipEffect>().tip = tip;
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
            LeaveTip(Random.Range(10, 20));//todo
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
        m_isWaitingForDish = true;
    }

    IEnumerator ConsumeDishCoroutine(Dish dish)
    {
        yield return new WaitForSeconds(dishGenerationDelay);
        Destroy(dish.gameObject);
    }
}
