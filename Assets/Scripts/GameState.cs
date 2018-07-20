using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float timer;

    public Waiter[] waiters;
    public Text[] waiters_points_view;
    public Text timer_view;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(waiters.Length == waiters_points_view.Length);
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

    }

    private void OnGUI()
    {
        for (int i = 0; i < waiters.Length; ++i)
        {
            waiters_points_view[i].text = waiters[i].collectedTip.ToString();
        }

        timer_view.text = FormatTime(timer);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time) / 60;
        int seconds = Mathf.FloorToInt(time);
        int miliseconds = Mathf.FloorToInt((time - (float)seconds) * 1000);

        seconds %= 60;


        return string.Format("{0:00}:{1:00}.{2:000}",
          minutes, seconds, miliseconds);
    }
}