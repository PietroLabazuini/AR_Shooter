using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.AI.Navigation;

public class EnemyNav : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    public int agentId;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        enemy.destination = player.transform.position;
    }

    public void ChangeAgentType()
    {
        enemy.agentTypeID = agentId;
    }
}
