using System.Collections.Generic;
using UnityEngine;

public class SkinPlayer : MonoBehaviour
{
    public List<Renderer> meshRenders;
    public Vector2 heightRange = new Vector2(0.5f, 2.5f);
    public float displayLerp = 5;
    public int skinID = 0;
    public float glitterTime = 1;

    Animator anim;
    float displayTimer = 0;
    float currentDisplayHeight = 0;
    float targetHeight = 0;
    float finishHeight = 0;
    Near2yCallBack HideCallBack = null;

    //glitter
    float glitterTimer = 0;
    bool inGlitter = false;
    const string emissionColorStr = "_EmissionColor";

    Dictionary<int, Color> renderEmissionStandColorDic = new Dictionary<int, Color>();


    private void Awake()
    {
        anim = GetComponent<Animator>();
        meshRenders = new List<Renderer>();
        foreach (var item in transform.GetComponentsInChildren<Renderer>())
        {
            renderEmissionStandColorDic.Add(meshRenders.Count, item.material.GetColor(emissionColorStr));
            meshRenders.Add(item);
        }
        currentDisplayHeight = targetHeight = heightRange.y;
    }


    private void Update()
    {
        if (currentDisplayHeight != targetHeight)
        {
            displayTimer += Time.deltaTime;
            currentDisplayHeight = Mathf.Lerp(currentDisplayHeight, targetHeight, Time.deltaTime * displayLerp);
            if (Mathf.Abs(targetHeight - currentDisplayHeight) < 0.15f)
            {
                currentDisplayHeight = targetHeight;
                SetDisplayHeight(finishHeight);
                Glitter();
                anim.speed = 1;
                if(HideCallBack!= null)HideCallBack();
                HideCallBack = null;
            }
            else
            {
                SetDisplayHeight(currentDisplayHeight);
            }
        }

        if (inGlitter)
        {
            glitterTimer += Time.deltaTime;
            for(int i = 0; i < meshRenders.Count; i++)
            {
                Color color = Color.Lerp(meshRenders[i].material.GetColor(emissionColorStr), renderEmissionStandColorDic[i], glitterTimer / glitterTime);
                meshRenders[i].material.SetColor(emissionColorStr, color);
            }
            if (glitterTimer >= glitterTime) inGlitter = false;
        }

    }

    public void DisplaySkin()
    {
        gameObject.SetActive(true);
        anim.Play("show");
        anim.speed = 0;
        currentDisplayHeight = heightRange.x;
        targetHeight = heightRange.y;
        finishHeight = 10;
        SetDisplayHeight(currentDisplayHeight);
        displayTimer = 0;
    }


    public void HideSkin(Near2yCallBack complete = null)
    {
        //currentDisplayHeight = heightRange.x;
        targetHeight = heightRange.x;
        finishHeight = heightRange.x;
        SetDisplayHeight(currentDisplayHeight);
        displayTimer = 0;
        HideCallBack = () =>
        {
            if (complete != null) complete();
            gameObject.SetActive(false);
        };
    }


    void SetDisplayHeight(float displayHeight)
    {
        foreach (var item in meshRenders)
        {
            item.material.SetFloat("_DisplayHeight", displayHeight);
        }
    }

    //闪光
    void Glitter()
    {
        for(int i = 0; i < meshRenders.Count; i++)
        {
            meshRenders[i].material.SetColor(emissionColorStr,Color.white);
        }

        glitterTimer = 0;
        inGlitter = true;
    }

    void OutMenu()
    {
        //MenuSceneManager.Instance.mainCamera.gameObject.SetActive(false);
        GameManager.Instance.GameStart();
    }

}
