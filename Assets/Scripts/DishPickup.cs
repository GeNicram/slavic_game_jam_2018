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
    
    public bool hasDish
    {
        get { return m_dish != null; }
    }

    public Dish dish
    {
        get { return m_dish; }
    }

    private void SetRandom()
    {
        GameObject dishObject = Instantiate(dishPrefab, GetComponent<Transform>().position, Quaternion.identity);
        m_dish = dishObject.GetComponent<Dish>();
        audioSC.PlayOneShot(dishReady);
    }

    private void Start()
    {
        Debug.Assert(dishPrefab != null);
        StartCoroutine("InitialDelayCoro");
     //   
    }

    public Dish PickUp()
    {
        Dish dish = m_dish;
        m_dish = null;
        StartCoroutine(PostPickUpCoroutine());
        return dish;
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
