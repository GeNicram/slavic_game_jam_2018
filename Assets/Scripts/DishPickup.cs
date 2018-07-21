using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPickup : MonoBehaviour
{
    public GameObject dishPrefab = null;
    public float dishGenerationDelay = 3.0f;
    public float maxRandomDelayDeviation = 1.0f;
    public AudioSource audioSC;
    public AudioClip dishReady;

    private Dish m_dish;

    int dishType = -1;
    
    public bool hasDish
    {
        get { return dishType != -1; }
    }

    public Dish dish
    {
        get { return m_dish; }
    }

    private void SetRandom()
    {
        dishType = Random.Range(0, Common.dishTypeCount - 1);
        GameObject dishObject = Instantiate(dishPrefab, GetComponent<Transform>().position, Quaternion.identity);
        dishObject.GetComponent<SpriteRenderer>().sprite = Common.dishSprites[dishType];
        m_dish = dishObject.GetComponent<Dish>();
        audioSC.PlayOneShot(dishReady);
    }

    private void Start()
    {
        Debug.Assert(dishPrefab != null);
        StartCoroutine("InitialDelayCoro");
     //   
    }

    public int PickUp()
    {
        int temp = dishType;
        dishType = -1;
        m_dish = null;
        StartCoroutine(PostPickUpCoroutine());
        return temp;
    }

    IEnumerator InitialDelayCoro()
    {
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0f, 6f));
        SetRandom();
    }

    IEnumerator PostPickUpCoroutine()
    {
        yield return new WaitForSeconds(dishGenerationDelay + Random.Range(0.0f, maxRandomDelayDeviation));
        SetRandom();
    }
}
