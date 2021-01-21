using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="TestConfig",menuName ="CreateTestConfig")]
public class TestConfig : ScriptableObject
{
    [Header("< 测试配置表使用 >")]
    public bool inTest = false;
    [Space(5f)]
    public SceneName testSceneName = SceneName.scene07;
    [Space(5f)]
    public string smallLevelID = string.Empty;
}

public class TestData
{
    private static TestData m_Instance;
    public static TestData Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new TestData();
            }

            return m_Instance;
        }
    }

    TestConfig m_TestConfig = null;

    public TestConfig TestConfig {
        get { 
            if(m_TestConfig == null)
            {
                string path = "Assets/Scripts/ForTest/TestConfig.asset";
                m_TestConfig = AssetDatabase.LoadAssetAtPath<TestConfig>(path);
            }
            return m_TestConfig; }
    } 


}

public enum SceneName
{
    scene07,
    Test
}
