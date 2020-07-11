using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongRing : MonoBehaviour
{
    public Transform DistanceChecker;
    public SongsNames SongName;
    public float GrowSpeed;
    public float maxScale;

    private double lastTick;
    private void Update()
    {
        if(transform.localScale.x < maxScale)
        {
            GrowSize();
        }

        if (lastTick + GameManager.Instance.Player.SongGameplay.TickLength <= Time.time)
        {
            lastTick = Time.time;
            LookForTargets();
        }
    }

    private void GrowSize()
    {
        transform.localScale += new Vector3(1, 1, 0) * GrowSpeed * Time.deltaTime;

        if (transform.localScale.x >= maxScale)
        {
            transform.localScale = new Vector3(maxScale, maxScale, transform.localScale.z);
        }
    }

    private void LookForTargets()
    {
        float radius = Vector3.Distance(DistanceChecker.position, transform.position);

        foreach(Listener l in GameManager.Instance.Listeners)
        {
            if(Vector3.Distance(transform.position, l.transform.position) <= radius)
            {
                l.TryApplyDamage(this);
            }
        }
    }
}
