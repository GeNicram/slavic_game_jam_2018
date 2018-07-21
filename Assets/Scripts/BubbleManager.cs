using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {
    public enum Bubble : int
    {
        pushY = 0,
        other
    };

    // This list should be same to Bubble enum
    public GameObject[] bubbles;
    static private GameObject[] m_bubbles;

	// Use this for initialization
	void Start () {
        m_bubbles = bubbles;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnBubble(Bubble.pushY, new Vector2(3, 3), 5);
        }
	}

    public static GameObject SpawnBubble(Bubble type, Vector2 position, float duration)
    {
        GameObject new_bubble = Instantiate(
            m_bubbles[(int)type], position, new Quaternion());

        Destroy(new_bubble, duration);

        return new_bubble;
    }
}
