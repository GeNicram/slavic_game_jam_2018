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
        GetComponent<SpriteRenderer>().sprite = Common.dishSprites[dishType];
    }

    private void Start()
    {
        StartCoroutine("InitialDelayCoro");
     //   
    }

    public int PickUp()
    {
        int temp = dishType;
        dishType = -1;
        StartCoroutine(PostPickUpCoroutine());
        return temp;
    }

    IEnumerator InitialDelayCoro()
    {
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(-4f, 3f));
        SetRandom();
    }

    IEnumerator PostPickUpCoroutine()
    {
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0.0f, maxRandomDelayDeviation));
        SetRandom();
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().enabled = hasDish;
    }
}
