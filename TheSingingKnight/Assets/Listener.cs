using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Listener : MonoBehaviour
{
    public SongsNames Weakness;
    public float MaxHealth;
    public float HealthRegen;
    public float HealthRegenDelay;

    [Header("UI")]
    public Canvas canvas;
    public Image HealthGauge;

    [Header("Read Only")]
    public float health;

    private double lastRegenBlock;
    private Vector3 baseEuler;

    private void Start()
    {
        health = MaxHealth;
        canvas.transform.forward = Camera.main.transform.forward;
        baseEuler = canvas.transform.eulerAngles;
    }

    private void Update()
    {
        canvas.transform.eulerAngles = baseEuler;
        TryApplyRegen();
    }

    public void TryApplyDamage(SongRing ring)
    {
        if(Weakness == SongsNames.None || ring.SongName == Weakness)
        {
            Debug.Log("Apply dmg : " + GameManager.Instance.Player.SongGameplay.DamagePerTick);
            health -= GameManager.Instance.Player.SongGameplay.DamagePerTick;
            HealthGauge.fillAmount = health / MaxHealth;
            lastRegenBlock = Time.time;
        }
    }

    public void TryApplyRegen()
    {
        if (health >= MaxHealth)
            return;
        else if (lastRegenBlock + HealthRegenDelay <= Time.time)
            health += HealthRegen * Time.deltaTime;

        //if (health <= 0)
        //    Debug.Log("NPC Defeated");
    }

    public void TryApplyDance()
    {
        lastRegenBlock = Time.time;
    }
}
