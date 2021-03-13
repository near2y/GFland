using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstGameSet
{
    public static readonly string skillJsonUrl = "Assets/RealFram/Data/Json/skillTest.json";
    public static readonly string equipmentJsonUrl = "Assets/RealFram/Data/Json/equipmentTest.json";
    
    public static readonly string effectJsonUrl = "Assets/RealFram/Data/Json/effectTest.json";
}

public enum GamePhysicsLayer:Int32
{
    Player = 8,
    Monster = 9,
    Ground = 10,
    PlayerBullet = 11,
    MonsterBullet = 12
}
