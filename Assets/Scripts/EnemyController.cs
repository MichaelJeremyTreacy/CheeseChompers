using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _navMeshEnemy;

    private void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
        _navMeshEnemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navMeshEnemy.SetDestination(_playerTransform.position);
    }
}
