using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIDurationBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField]private Gradient gradient;
    [SerializeField]private Image fill;

    private void Awake()
    {
        slider.gameObject.SetActive(false);
    }
    public void EnableBar(float maxTime)
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
        slider.gameObject.SetActive(true);

        UpdateColor();

    }
    public void UpdateValue(float  timeLeft)
    {
        slider.value = Mathf.Max(timeLeft, 0);
        UpdateColor();
        if (timeLeft <= 0)
        {
            slider.gameObject.SetActive(false);
        }
    }

    private void UpdateColor()
    {
        float val = slider.value / slider.maxValue;
        fill.color = gradient.Evaluate(val);
    }
}
