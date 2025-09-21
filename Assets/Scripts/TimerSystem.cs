using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem
{
    private Text timerText;
    private float startTime;
    private bool isRunning = false;

    public float ElapsedTime => isRunning ? Time.time - startTime : 0f;
    public bool IsRunning => isRunning;

    public TimerSystem(Text text)
    {
        this.timerText = text;
    }

    public void StartTimer()
    {
        isRunning = true;
        startTime = Time.time;
        Debug.Log("Race timer started!");
    }

    public void StopTimer()
    {
        isRunning = false;
        Debug.Log($"Race timer stopped at {ElapsedTime:F3} seconds");
    }

    public void Reset()
    {
        isRunning = false;
        startTime = 0f;
        if (timerText != null)
            timerText.text = "00:00.000";
    }

    public void UpdateDisplay()
    {
        if (!isRunning || timerText == null) return;

        TimeSpan timeSpan = TimeSpan.FromSeconds(ElapsedTime);
        timerText.text = string.Format("{0:D2}:{1:D2}.{2:D3}",
            timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }
}