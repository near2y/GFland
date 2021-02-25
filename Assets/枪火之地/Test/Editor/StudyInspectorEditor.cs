using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class StudyInspectorEditor : Editor
{
    StudyEditor m_StudyEditor = null;
    ReorderableList reorderableList;

    private void OnEnable()
    {
        m_StudyEditor = (StudyEditor)target;
        reorderableList = new ReorderableList(serializedObject,
                                 serializedObject.FindProperty("texts"));
    }

    public override void OnInspectorGUI()
    {
        reorderableList.DoLayoutList();
    }
}
