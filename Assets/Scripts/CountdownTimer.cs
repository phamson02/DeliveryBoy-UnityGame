using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 0;
    public bool takingAway = false;
    public int minutesLeft = 4;

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = textDisplay.GetComponent<TextMeshProUGUI>();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (takingAway == false && secondsLeft > 0 && minutesLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        else if (takingAway == false && secondsLeft == 0 && minutesLeft > 0) {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;

        if (minutesLeft > 0 || secondsLeft > 0)
        {
            yield return new WaitForSeconds(1);

            if (secondsLeft == 0)
            {
                minutesLeft--;
                secondsLeft = 59;
            }
            else
            {
                secondsLeft--;
            }

            UpdateTimerDisplay();
        }

        takingAway = false;
    }

    void UpdateTimerDisplay()
    {
        textMeshPro.text = minutesLeft.ToString("00") + ":" + secondsLeft.ToString("00");
    }
}
