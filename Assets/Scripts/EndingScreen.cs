using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour {

    public Color[] m_waitres_colors;
    static private int[] m_results;

    private List<waitres_and_points> m_sortet_results;
    public GameObject winning_hat;

    private class waitres_and_points {
        public waitres_and_points(ref Color waiter_color, int points) {
            m_waitrer_color = waiter_color;
            m_points = points;

        }

        public Color m_waitrer_color;
        public int m_points;
        public Vector2 animate_starting_position;
        public Vector2 animate_destination_position;
    }

	// Use this for initialization
	void Start () {
        m_sortet_results = new List<waitres_and_points>();
        for (int i = 0; i < m_waitres_colors.Length; ++i) {
            m_sortet_results.Add(new waitres_and_points(ref m_waitres_colors[i], m_results[i]));
        }

    m_sortet_results.Sort(
        delegate(waitres_and_points a, waitres_and_points b) {
        return (a.m_points == b.m_points ? 0 :
            (a.m_points < b.m_points) ? -1 : 1);
    });

        for (int i = 0; i < m_sortet_results.Count; ++i) {
            m_sortet_results[i].animate_destination_position = new Vector2(3.8f, i);
        }

        SpriteRenderer renderer = winning_hat.GetComponent<SpriteRenderer>();
        renderer.color = m_sortet_results[3].m_waitrer_color;
	}

  float m_passed_time;
	
	// Update is called once per frame
	void Update () {

	}

    public void OnClickAgain() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainLevel");
      }
    public void OnClickQuit() {
        Destroy(FindObjectOfType<AudioScript>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    static public void SetResults(int[] results) {
        m_results = results;
    }
}
