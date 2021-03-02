using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    [Header("随机范围值")]
    public Vector2 m_scaleRange;
    [Header("插值率")]
    [Range(0,1)]    
    public float m_lerp;
    Vector3 tarScale;
    Vector3 currentScale;

    void Start()
    {
        tarScale = transform.localScale;
        currentScale = transform.lossyScale;
    }


    private void Update()
    {
        if(Mathf.Abs(transform.localScale.y - tarScale.y)<0.1f)
        {
            tarScale.y = Random.Range(m_scaleRange.x, m_scaleRange.y);
        }
        else
        {
            currentScale = transform.localScale;
            currentScale.y = Mathf.Lerp(currentScale.y, tarScale.y, m_lerp);
            transform.localScale = currentScale;
        }
    }
}
