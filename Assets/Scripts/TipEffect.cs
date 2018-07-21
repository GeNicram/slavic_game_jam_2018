using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipEffect : MonoBehaviour
{
    public Font font;

    public float counterSpeed = 2.0f;
    public float lingerTime = 0.5f;
    public float raise = 0.3f;

    int m_tip = 0;
    int m_displayTip = 0;
    float m_tipCounter = 0.0f;

    private Vector3 m_pos;

    public int tip
    {
        set { m_tip = value; }
    }

    private void Start()
    {
        m_pos = GetComponent<Transform>().position;
    }

    private void Update()
    {
        m_tipCounter += Time.deltaTime;
        float t = Mathf.Min(m_tipCounter, 1.0f);

        m_displayTip = (int)Mathf.Lerp(0, m_tip, t);
        Vector3 offset = Vector3.zero;
        offset.y = Mathf.Sin(t * (Mathf.PI / 2)) * raise;

        Transform transform = GetComponent<Transform>();
        transform.position = m_pos + offset;

        if (m_tipCounter >= 1.0f + lingerTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(0.0f, 0.5f, 0.0f);
        style.fontSize = 20;
        style.font = font;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y - 50, 100, 100), "+$" + m_displayTip, style);
    }
}
