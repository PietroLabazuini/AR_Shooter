using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshHandler : MonoBehaviour
{
    public List<NavMeshSurface> navMeshArray;

    //Should be changed when AR planes are implemented
    private void Update()
    {
        BakeNavMeshs();
    }

    public void BakeNavMeshs()
    {
        for (int i = 0; i < navMeshArray.Count; i++)
        {
            navMeshArray[i].BuildNavMesh();
        }
    }

    public void AddNavMesh(NavMeshSurface navMesh)
    {
        navMeshArray.Add(navMesh);
    }

    public void RemoveNavMesh(NavMeshSurface navMesh)
    {
        navMeshArray.Remove(navMesh);
    }

    public Vector3 GetRandomPoint(float min_X, float max_X, float min_Z, float max_Z)
    {
        Vector3 position = Vector3.zero;
        NavMeshHit hit;
        for (int i = 0; i < 25; i++)
        {
            position = new Vector3(Random.Range(min_X, max_X), 1f, Random.Range(min_Z, max_Z));
            if (!NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
            {
                break;
            }
        }
        return position;
    }
}