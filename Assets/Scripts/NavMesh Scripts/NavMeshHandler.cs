using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshHandler : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshArray;

    //Should be changed when AR planes are implemented
    private void Update()
    {
        BakeNavMeshs();
    }

    public void BakeNavMeshs()
    {
        for (int i = 0; i < navMeshArray.Length; i++)
        {
            navMeshArray[i].BuildNavMesh();
        }
    }

    public void AddNavMesh(NavMeshSurface navMesh)
    {
        navMeshArray[navMeshArray.Length] = navMesh;
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 position = Vector3.zero;
        NavMeshHit hit;
        for (int i = 0; i < 25; i++)
        {
            position = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            if (!NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
            {
                break;
            }
        }
        return position;
    }
}