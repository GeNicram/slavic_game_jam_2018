using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer bubbleSpriteRenderer;
    
    public int dishType
    {
        set
        {
            if (value != -1)
            {
                GetComponentInParent<SpriteRenderer>().enabled = true;
                bubbleSpriteRenderer.enabled = true;
                bubbleSpriteRenderer.sprite = Common.dishSprites[value];
            }
            else
            {
                GetComponentInParent<SpriteRenderer>().enabled = false;
                bubbleSpriteRenderer.enabled = false;
            }
        }
    }

	private void Start()
    {
        Debug.Assert(bubbleSpriteRenderer != null);
        dishType = -1;
    }
}
