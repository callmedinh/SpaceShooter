using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvent
{
    public static Action<EnemyData> EnemyHited;
    public static Action<PlayerData> PlayerHited;
    public static Action GameOver;
}
