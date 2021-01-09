using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5;

    Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        offSet = target.position - transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position - offSet;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }

}
