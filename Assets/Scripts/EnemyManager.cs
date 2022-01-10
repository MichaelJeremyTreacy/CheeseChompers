using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static System.Timers.Timer s_timer;

    private float _countDownToNewEnemy;
    public float CountDownFrom = 5f;

    public GameObject EnemyPrefab;

    private void Start()
    {
        _countDownToNewEnemy = CountDownFrom;

        s_timer = new System.Timers.Timer(1000);
        s_timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => _countDownToNewEnemy--;
        s_timer.Start();
    }

    private void Update()
    {
        if (_countDownToNewEnemy <= 0)
        {
            SpawnNewEnemy();
            _countDownToNewEnemy = CountDownFrom;
        }
    }

    private void SpawnNewEnemy()
    {
        Instantiate(EnemyPrefab, new Vector3(0f, 1.1f, 0f), Quaternion.identity);
    }
}
