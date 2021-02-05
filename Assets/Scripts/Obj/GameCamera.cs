using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GameCamera : MonoBehaviour
{
    public float smoothing = 5;
    public CinemachineVirtualCamera walkCamera;
    public CinemachineStateDrivenCamera drivenCamera;
    public CinemachineTargetGroup targetGroup;


    CinemachineTargetGroup.Target[] targetList = null;
    float shakeTimer = 0;
    CinemachineBasicMultiChannelPerlin shakePerlin;
    float startIntensity = 0;
    float shakeTimerTotal;

    private void Start()
    {
        shakePerlin = walkCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            float res = Mathf.Lerp(0f,startIntensity, shakeTimer / shakeTimerTotal);
            shakePerlin.m_AmplitudeGain = res;
        }

    }

    public void ShakeCamera(float intensity,float time)
    {
        shakePerlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    public void AddGroup(Transform trans,float weight = 1,float radius = 1)
    {
        targetList = new CinemachineTargetGroup.Target[targetGroup.m_Targets.Length + 1];
        for(int i = 0; i < targetGroup.m_Targets.Length ; i++)
        {
            targetList[i] = targetGroup.m_Targets[i];
        }
        targetList[targetGroup.m_Targets.Length] = new CinemachineTargetGroup.Target() { target = trans, weight = weight, radius = radius};
        targetGroup.m_Targets = targetList;
    }

}
