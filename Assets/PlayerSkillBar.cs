using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBar : MonoBehaviour
{
    int m_skillProgress = 0;
    int m_lastSkillProgress = -1;
    public int SkillProgress
    {
        get
        {
            return m_skillProgress;
        }

        set
        {
            m_skillProgress = value;

        }
    }

    const string processShaderName = "_skillprogress";
    const string colorShaderName = "_Color";
    public Renderer bar1 = null;
    public Renderer bar2 = null;

    Color green = new Color(0.5315f, 1.1486f, 0);
    Color yellow = new Color(1.15f, 0.9451f, 0);

    private void Update()
    {
        var euler = transform.rotation.eulerAngles;
        euler.y = 180;
        transform.rotation = Quaternion.Euler(euler);
        if (SceneManager.Instance.gameUI == null) return;
        if(m_lastSkillProgress != m_skillProgress)
        {
            m_lastSkillProgress = (int)Mathf.Lerp(m_lastSkillProgress, m_skillProgress, 10 * Time.deltaTime);

            bar1.material.SetFloat(processShaderName, m_lastSkillProgress);
            bar2.material.SetFloat(processShaderName, m_lastSkillProgress);
            if (m_lastSkillProgress >= 100)
            {
                bar1.material.SetColor(colorShaderName, yellow);
                bar2.material.SetColor(colorShaderName, yellow);
                //SceneManager.Instance.gameUI.m_Panel.skillBtn.interactable = true;
                SceneManager.Instance.gameUI.m_Panel.skillBtn.gameObject.SetActive(true);
            }
            else
            {
                bar1.material.SetColor(colorShaderName, green);
                bar2.material.SetColor(colorShaderName, green);
                //SceneManager.Instance.gameUI.m_Panel.skillBtn.interactable = false;
                SceneManager.Instance.gameUI.m_Panel.skillBtn.gameObject.SetActive(false);
            }
        }
    }

}
