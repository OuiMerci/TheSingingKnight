using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceRing : MonoBehaviour
{
    public Gradient gradient;
    public float strobeDuration;
    public SpriteRenderer Renderer;
    public Transform DistanceChecker;

    public void Update()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        Renderer.color = gradient.Evaluate(t);
    }

    private void LookForTargets()
    {
        float radius = Vector3.Distance(DistanceChecker.position, transform.position);

        foreach (Listener l in GameManager.Instance.Listeners)
        {
            if (Vector3.Distance(transform.position, l.transform.position) <= radius)
            {
                l.TryApplyDance();
            }
        }
    }
}
