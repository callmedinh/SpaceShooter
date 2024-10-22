using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvent
{
    public static Action<EnemyData> EnemyHited;
    public static Action<int> PlayerHited;
    public static Action GameOver;
    public static Action<bool> GameStarted;
    public static Action OnBossDeath;
    public static Action EnemyKilled;
}
