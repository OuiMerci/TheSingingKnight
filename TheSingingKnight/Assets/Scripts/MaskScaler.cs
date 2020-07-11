using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskScaler : MonoBehaviour
{
    public float localScaleGrowth;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localScale += transform.parent.lossyScale * localScaleGrowth;
    }
}
