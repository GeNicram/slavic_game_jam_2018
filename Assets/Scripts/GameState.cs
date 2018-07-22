using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public float time_limit_seconds;

    public Waiter[] waiters;
    public Text[] waiters_points_view;
    public Text[] timer_digits;

    public GameObject ending_handler;

    static private int[] m_results;

    private float current_time;
    private bool reached_end;

    public GameState() : base()
    {
        m_results = new int[4];
    }

    // Use this for initialization
    void Start()
    {
        Debug.Assert(waiters.Length == waiters_points_view.Length);
        Debug.Assert(timer_digits.Length == 4);

        for (int i = 0; i < m_results.Length; ++i) {
            m_results[i] = 0;
        }

        current_time = time_limit_seconds;
        ending_handler.SetActive(false);
        reached_end = false;


        previousTip = new int[4];
        currentTip = new int[4];
        destinyTip = new int[4];
        passed_tip_counter = new float[4];
    }
    // Update is called once per frame
    void Update() {
        if (reached_end)
            return;

        current_time -= Time.deltaTime;

        if (current_time <= 0) {
            current_time = 0;
            ending_handler.SetActive(true);
            Time.timeScale = 0; 
            EndingScreen.SetResults(new int[]{
        waiters[0].collectedTip,
        waiters[1].collectedTip,
        waiters[2].collectedTip,
        waiters[3].collectedTip});
            reached_end = true;
        }

        for (int i = 0; i < waiters_points_view.Length; ++i) {
            if (waiters[i].collectedTip != destinyTip[i]) {
                previousTip[i] = currentTip[i];
                destinyTip[i] = waiters[i].collectedTip;
                passed_tip_counter[i] = 0;
                Debug.Log("New");
            }

            currentTip[i] = (int)Mathf.Lerp((float)previousTip[i], (float)destinyTip[i], (float)passed_tip_counter[i]);
            passed_tip_counter[i] += Time.deltaTime;
        }

    }

    private int[] previousTip;
    private int[] currentTip;
    private int[] destinyTip;
    private float[] passed_tip_counter;

    private void OnGUI()
    {
        for (int i = 0; i < waiters_points_view.Length; ++i) {
            waiters_points_view[i].text = currentTip[i].ToString(); //waiters[i].collectedTip.ToString();
        }

        var digitText = FormatTime(current_time);
        for (int i = 0; i < 4; ++i)
        {
            timer_digits[i].text = digitText[i];
        }
    }

    private string[] FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time) % 60;
        int minutes = Mathf.FloorToInt(time) / 60;
        int minutes_tens = minutes / 60;
        int minutes_ones = minutes % 60;
        int seconds_tens = seconds / 10;
        int seconds_ones = seconds % 10;

        return new string[4] {
            minutes_tens.ToString(),
            minutes_ones.ToString(),
            seconds_tens.ToString(),
            seconds_ones.ToString(),
        };
    }
}