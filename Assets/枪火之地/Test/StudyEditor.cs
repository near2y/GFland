using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StudyEditor : MonoBehaviour
{
    [ContextMenuItem("Random","RandomNumber")]
    [Tooltip("这是一个随机攻击力")]
    public float randomATK;

    [SerializeField]
    string[] texts;

    [ContextMenu("RandomNumber")]
    void RandomNumber()
    {
        Debug.Log("near2y");
    }
}
