using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    const uint dishTypeCount = 5;
    public float dishGenerationDelay = 2;
    public float maxRandomDelayDeviation = 0;
    private uint m_tip = 0;
    private uint m_desiredDishType = 0;
    private bool m_isWaitingForDish = false;

    public uint desiredDishType
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
        desiredDishType = (uint)Random.Range(0, dishTypeCount - 1);
        m_isWaitingForDish = true;

    }
    
    public void LeaveTip(uint tip)
    {
        m_tip += tip;
    }

    public uint TakeTip()
    {
        uint tip = m_tip;
        m_tip = 0;
        return tip;
    }

    public bool Serve(uint dishType)
    {
        if (!m_isWaitingForDish)
        {
            return false;
        }

        if (dishType == desiredDishType)
        {
            LeaveTip((uint)Random.Range(10, 20));//todo
        }

        StartCoroutine(PostServeCoroutine());
        return true;
    }

    IEnumerator PostServeCoroutine()
    {
        m_isWaitingForDish = false;
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0,maxRandomDelayDeviation));
        desiredDishType = (uint)Random.Range(0, dishTypeCount - 1);
        m_isWaitingForDish = true;
    }

    void OnGUI()
    {
        if (isWaitingForDish)  // or check the app debug flag
        {
            Transform transform = GetComponent<Transform>();
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), "I want dish type <" + desiredDishType + ">", style);
        }
    }
}
