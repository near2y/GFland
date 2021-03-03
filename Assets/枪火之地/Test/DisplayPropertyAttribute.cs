using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPropertyAttribute : PropertyAttribute
{
    public string m_PropertyName;
    public int m_Equal;

    public DisplayPropertyAttribute(string propertyName,int equal)
    {
        m_PropertyName = propertyName;
        m_Equal = equal;
    }

}
