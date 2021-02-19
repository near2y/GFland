using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Method
{
    /// <summary>
    /// 判断是否在视野内
    /// </summary>
    /// <returns></returns>
    public static bool InSight(Transform src,Transform tar, float angle)
    {

        Vector3 dir = tar.position - src.position;
        return Vector3.Angle(src.forward, dir)<angle; 
    }


    public static bool SetRenderColorRange(Renderer renderer,float value)
    {
        if (renderer == null) return false;
        renderer.material.SetFloat("_colorrange", value);
        return true;
    }

    public static bool LerpRenderColorRange(Renderer renderer, float value)
    {
        if (renderer == null) return false;
        float res = Mathf.Lerp(renderer.material.GetFloat("_colorrange"), value, 15 * Time.deltaTime);
        if (res - value < 0.3f)
        {
            res = value;
            renderer.material.SetFloat("_colorrange", res);
            return false;
        }
        else
        {
            
            renderer.material.SetFloat("_colorrange", res);
            return true;
        }
    }

    public static float GetColorrangeInRender(Renderer renderer)
    {
        if (renderer == null) return -1;
        return renderer.material.GetFloat("_colorrange");
    }



}
