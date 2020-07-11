using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongRing : MonoBehaviour
{
    public Transform DistanceChecker;
    public SongsNames SongName;
    public float GrowSpeed;
    public float maxScale;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(transform.localScale.x < maxScale)
        {
            GrowSize();
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

    }
}
