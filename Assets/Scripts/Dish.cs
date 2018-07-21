using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public float transferSpeed = 10.0f;

    Vector3 m_oldPos;
    float m_oldScale;
    Transform m_transferTarget;
    float m_transferRate;

    Transform m_transform;

    int m_type;
    
    public int type
    {
        get { return m_type; }
    }

    public void Transfer(Transform targetTransform)
    {
        m_oldPos = m_transform.position;
        m_oldScale = m_transform.localScale.x;
        m_transferTarget = targetTransform;
        m_transferRate = 0.0f;
    }

	private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);//hack
        m_oldPos = Vector3.zero;
        m_transferTarget = null;
        m_transferRate = 0.0f;

        m_type = Random.Range(0, Common.dishTypeCount - 1);
        GetComponent<SpriteRenderer>().sprite = Common.dishSprites[m_type];
    }

    private void Update()
    {
        if (m_transferTarget != null) {
            m_transferRate += transferSpeed * Time.deltaTime;
            m_transferRate = Mathf.Min(m_transferRate, 1.0f);

            Vector3 pos = Vector3.Lerp(m_oldPos, m_transferTarget.position, m_transferRate);
            pos.y += Mathf.Sin(m_transferRate * Mathf.PI) * 0.5f;
            m_transform.position = pos;

            float scale = Mathf.Lerp(m_oldScale, m_transferTarget.localScale.x, m_transferRate);
            m_transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void Abandon()
    {
        m_transferTarget = null;
        Destroy(gameObject, 1);
    }
}
