using UnityEngine;
using UnityEngine.Events;

public class DayTimer : MonoBehaviour
{
    [SerializeField] private float dayDuration = 120f;

    private float timer;
    private bool running;

    public UnityEvent OnDayEnded;

    void Update()
    {
        if (!running) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            running = false;
            OnDayEnded?.Invoke();
        }
    }

    public void StartTimer()
    {
        timer = dayDuration;
        running = true;
    }

    public float GetRemainingTime()
    {
        return timer;
    }
}