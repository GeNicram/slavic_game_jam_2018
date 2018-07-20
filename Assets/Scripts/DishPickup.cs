using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPickup : MonoBehaviour
{
    int dishType = -1;
    
    public bool hasDish
    {
        get { return dishType != -1; }
    }

    void Start()
    {
        dishType = Random.Range(0, Common.dishTypeCount - 1);
    }

    public int PickUp()
    {
        Destroy(gameObject);
        int temp = dishType;
        dishType = -1;
        return temp;
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
