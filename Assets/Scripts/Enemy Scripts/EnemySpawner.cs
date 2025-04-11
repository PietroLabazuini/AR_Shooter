using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    LineRenderer _lineRenderer;
    [SerializeField]
    Text _buttonText;
    [SerializeField]
    NavMeshHandler _runtimeBaker;
    private IEnumerator spawnCoroutine;

    public bool spawning;

    public float min_X, max_X, min_Z, max_Z;
    void Start()
    {
        //_runtimeBaker = GetComponent<NavMeshHandler>();
        _lineRenderer = GetComponent<LineRenderer>();

        min_X = -5f;
        min_Z = -5f;
        max_X = 5f;
        max_Z = 5f;
        _lineRenderer.positionCount = 0;
        spawnCoroutine = EnemySpawnCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        //DrawSpawnZone();
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
        Vector3 spawnPosition = _runtimeBaker.GetRandomPoint(min_X, max_X, min_Z, max_Z);
        if (spawnPosition != Vector3.zero)
        {
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void DrawSpawnZone()
    {
        _lineRenderer.positionCount = 5;
        _lineRenderer.SetPosition(0,new Vector3(min_X, 0, min_Z));
        _lineRenderer.SetPosition(4, new Vector3(min_X, 0, min_Z));
        _lineRenderer.SetPosition(1, new Vector3(min_X, 0, max_Z));
        _lineRenderer.SetPosition(2, new Vector3(max_X, 0, max_Z));
        _lineRenderer.SetPosition(3, new Vector3(max_X, 0, min_Z));
    }

    public void EnableSpawning()
    {
        if (!spawning)
        {
            StartCoroutine(spawnCoroutine);
            _buttonText.text = "Stop";
        }
        else
        {
            StopCoroutine(spawnCoroutine);
            _buttonText.text = "Start";
        }
        spawning = !spawning;
    }
    
}
