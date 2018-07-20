using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public float dishGenerationDelay = 2;
    public float maxRandomDelayDeviation = 0;
    private int m_tip = 0;
    private int m_desiredDishType = 0;
    private bool m_isWaitingForDish = false;
    private bool m_didLeaveCorrectDish = true;

    public int desiredDishType
    {
        get { return m_desiredDishType; }
        set { m_desiredDishType = value; }
    }

    public bool isWaitingForDish
    {
        get { return m_isWaitingForDish; }
    }

    private void Start()
    {
        m_tip = 0;
        desiredDishType = Random.Range(0, Common.dishTypeCount - 1);
        m_isWaitingForDish = true;

    }
    
    public void LeaveTip(int tip)
    {
        m_tip += tip;
    }

    public int CollectTip()
    {
        int tip = m_tip;
        m_tip = 0;
        return tip;
    }

    public bool Serve(int dishType)
    {
        bool didLeaveCorrectDish = (dishType == desiredDishType);
        if (didLeaveCorrectDish)
        {
            LeaveTip(Random.Range(10, 20));//todo
        }
        m_didLeaveCorrectDish = didLeaveCorrectDish;
        StartCoroutine(PostServeCoroutine());
        return didLeaveCorrectDish;
    }

    IEnumerator PostServeCoroutine()
    {
        m_isWaitingForDish = false;
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0,maxRandomDelayDeviation));
        desiredDishType = Random.Range(0, Common.dishTypeCount - 1);
        m_isWaitingForDish = true;
    }

    void OnGUI()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle style = new GUIStyle();
        if (isWaitingForDish)
        {
            style.normal.textColor = Color.black;
            GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), "I want dish type <" + desiredDishType + ">", style);
        }
        else
        {
            style.normal.textColor = m_didLeaveCorrectDish ? Color.green : Color.red;
            string text = m_didLeaveCorrectDish ? "Yay" : "That's the wrong dish";
            GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), text, style);
        }
    }
}
