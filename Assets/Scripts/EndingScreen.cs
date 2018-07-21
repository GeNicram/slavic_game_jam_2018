using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour {

  public GameObject[] m_waitres_faces;
  static private int[] m_results;

  private List<waitres_and_points> m_sortet_results;

  private class waitres_and_points {
    public waitres_and_points(ref GameObject waiter_face, int points) {
      m_waitrer_face = waiter_face;
      m_points = points;

      animate_starting_position = waiter_face.transform.position;
    }

    public GameObject m_waitrer_face;
    public int m_points;
    public Vector2 animate_starting_position;
    public Vector2 animate_destination_position;
  }

	// Use this for initialization
	void Start () {
    m_sortet_results = new List<waitres_and_points>();
    for (int i = 0; i < m_waitres_faces.Length; ++i) {
      m_sortet_results.Add(new waitres_and_points(ref m_waitres_faces[i], m_results[i]));
    }

    m_sortet_results.Sort(
      delegate(waitres_and_points a, waitres_and_points b) {
      return (a.m_points == b.m_points ? 0 :
        (a.m_points < b.m_points) ? -1 : 1);
    });

    for (int i = 0; i < m_sortet_results.Count; ++i) {
      m_sortet_results[i].animate_destination_position = new Vector2(3.8f, i);
    }

    m_passed_time = 0;
	}

  float m_passed_time;
	
	// Update is called once per frame
	void Update () {
    for (int i = 0; i < m_sortet_results.Count; ++i) {
      m_sortet_results[i].m_waitrer_face.transform.position =
        Vector2.Lerp(m_sortet_results[i].animate_starting_position,
        m_sortet_results[i].animate_destination_position,
        Mathf.Min(1, m_passed_time / 1));
    }

    m_passed_time += Time.deltaTime;
	}

  public void OnClickAgain() {
    Debug.Log("Reloading...");
        Time.timeScale = 1;
    SceneManager.LoadScene("MainLevel");
        
    
  }
  public void OnClickQuit() {
    //Debug.Log("Quitting...");

        Destroy(FindObjectOfType<AudioScript>().gameObject);
        SceneManager.LoadScene("MainMenu");
  }

  static public void SetResults(int[] results) {
    m_results = results;
  }
}
