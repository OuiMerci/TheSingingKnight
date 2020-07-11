using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Canvas canvas;
    public Image StaminaGauge;
    public Image HealthGauge;

    Vector3 baseEuler;

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
    }

    public void SetStaminaRatio(float fill)
    {
        StaminaGauge.fillAmount = fill;
    }

    public void SetHealthGauge(float fill)
    {
        HealthGauge.fillAmount = fill;
    }
}
