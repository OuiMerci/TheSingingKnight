using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Canvas canvas;
    public Image StaminaGauge;
    public Image HealthGauge;

    public double gaugeDisplayTime;

    Vector3 baseEuler;
    private double lastStaminaUpdate;
    private double lastHeathUpdate;

    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.forward = Camera.main.transform.forward;
        baseEuler = canvas.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.eulerAngles = baseEuler;

        if (lastHeathUpdate + gaugeDisplayTime <= Time.time)
        {
            HealthGauge.transform.parent.gameObject.SetActive(false);
        }

        if (lastStaminaUpdate + gaugeDisplayTime <= Time.time)
        {
            StaminaGauge.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetStaminaRatio(float fill)
    {
        StaminaGauge.transform.parent.gameObject.SetActive(true);
        StaminaGauge.fillAmount = fill;
    }

    public void SetHealthGauge(float fill)
    {
        HealthGauge.transform.parent.gameObject.SetActive(true);
        HealthGauge.fillAmount = fill;
    }
}
