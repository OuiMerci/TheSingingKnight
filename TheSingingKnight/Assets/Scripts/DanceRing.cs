using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceRing : MonoBehaviour
{
    public Gradient gradient;
    public float strobeDuration;
    public SpriteRenderer Renderer;

    public void Update()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        Renderer.color = gradient.Evaluate(t);
    }
}
