using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
  public float timer;


  public Text[] waiters_points_view;
  public Text timer_view;

	// Use this for initialization
	void Start() {
    timer = 0;
		
	}
	
	// Update is called once per frame
	void Update() {
    timer += Time.deltaTime;
		
	}

  private void OnGUI()
  {
    foreach (Text text in waiters_points_view) {
      text.text = "0";
    }

    timer_view.text = FormatTime(timer);
  }

  private string FormatTime(float time) {
    int minutes = Mathf.FloorToInt(time) / 60;
    int seconds = Mathf.FloorToInt(time);
    int miliseconds = Mathf.FloorToInt((time - (float)seconds) * 1000);

    seconds %= 60;


    return string.Format("{0:00}:{1:00}.{2:000}",
      minutes, seconds, miliseconds);
  }
}
