using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;

    NavMeshHandler _runtimeBaker;
    private IEnumerator spawnCoroutine;
    void Start()
    {
        _runtimeBaker = GetComponent<NavMeshHandler>();
        spawnCoroutine = EnemySpawnCoroutine();
        StartCoroutine(spawnCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawnCoroutine()
    {
        while (true) 
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = _runtimeBaker.GetRandomPoint();
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    
}
