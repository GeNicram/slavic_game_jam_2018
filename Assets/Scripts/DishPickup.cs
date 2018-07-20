using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPickup : MonoBehaviour
{
    public float dishGenerationDelay = 2.0f;
    public float maxRandomDelayDeviation = 1.0f;

    int dishType = -1;
    
    public bool hasDish
    {
        get { return dishType != -1; }
    }

    private void SetRandom()
    {
        dishType = Random.Range(0, Common.dishTypeCount - 1);
    }

    void Start()
    {
        SetRandom();
    }

    public int PickUp()
    {
        int temp = dishType;
        dishType = -1;
        StartCoroutine(PostPickUpCoroutine());
        return temp;
    }

    IEnumerator PostPickUpCoroutine()
    {
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0.0f, maxRandomDelayDeviation));
        SetRandom();
    }

    void OnGUI()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), dishType.ToString(), style);
    }
}
