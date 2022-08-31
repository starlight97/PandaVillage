using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : EnemyA
{
    public float attackDelay;
    public GameObject bulletPrefab;
    public UnityAction onAppearComplete;

    public float radius = 1;
    public float degree = 0;

    public GameObject[] firePivots;

    private Coroutine appearRoutine;
    private Coroutine moveRoutine;
    private Coroutine attackRoutine;

}
