using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.AI.Navigation;

public class EnemyNav : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;
    [SerializeField]
    GameObject player;
    [SerializeField]
    NavMeshAgent enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        surface.BuildNavMesh();
        enemy.destination = player.transform.position;
    }
}
