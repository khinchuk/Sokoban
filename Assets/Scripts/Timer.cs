using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float interval = 1f;
    public Text timerText;
    private IDisposable timer;

    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        timer = Observable.Interval(TimeSpan.FromSeconds(interval))
            .Subscribe(_ =>
            {
                timerText.text = interval.ToString("F2");
            });
    }

    public void StopTimer()
    {
        timer.Dispose();
    }
}