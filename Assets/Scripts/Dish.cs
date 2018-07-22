using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    static List<int> s_dishTypesInPlay = new List<int>();

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

    public static bool CanInstantiate()
    {
        return FindObjectsOfType<Dish>().Length < FindObjectsOfType<DishPickup>().Length;
    }

    private static int[] Shuffle(int n)
    {
        var random = new System.Random();
        var result = new int[n];
        for (var i = 0; i < n; i++)
        {
            var j = random.Next(0, i + 1);
            if (i != j)
            {
                result[i] = result[j];
            }
            result[j] = i;
        }
        return result;
    }

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);//hack
        m_oldPos = Vector3.zero;
        m_transferTarget = null;
        m_transferRate = 0.0f;
        
        m_type = -1;
        int[] typemap = Shuffle(Common.dishTypeCount);
        for (int i = 0; i < Common.dishTypeCount; ++i)
        {
            int dishType = typemap[i];
            if (!s_dishTypesInPlay.Contains(dishType))
            {
                m_type = dishType;
                break;
            }
        }

        // there's at least one of each dish, just pick a random one
        if (m_type == -1)
        {
            m_type = Random.Range(0, Common.dishTypeCount - 1);
        }

        s_dishTypesInPlay.Add(m_type);
        GetComponent<SpriteRenderer>().sprite = Common.dishSprites[m_type];
    }

    private void OnDestroy()
    {
        s_dishTypesInPlay.Remove(m_type);
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
