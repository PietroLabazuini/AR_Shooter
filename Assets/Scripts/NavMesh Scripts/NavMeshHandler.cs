using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine.XR.ARFoundation;

public class NavMeshHandler : MonoBehaviour
{
    [SerializeField]
    Text text;


    List<ARPlane> obstacles;
    NavMeshSurface surface;

    private void Start()
    {
        obstacles = new List<ARPlane>();
        if(TryGetComponent<NavMeshSurface>(out NavMeshSurface s))
        {
            surface = s;
            Debug.Log("NavMesh found.");
        }
        else
        {
            Debug.Log("No Surface found.");
        }
    }

    public void BuildNavMesh()
    {
        text.text = "NavMesh built.";
        Debug.Log("NavMesh built");
        surface.BuildNavMesh();
    }

    public void AddObstacle(GameObject plane)
    {
       StartCoroutine(UpdateObstacleHitbox(plane));
    }

    IEnumerator UpdateObstacleHitbox(GameObject plane)
    {
        //Reset the navMeshObstacle hitbox to current mesh
        if (plane.TryGetComponent<NavMeshObstacle>(out var oldObstacle))
        {
            Destroy(oldObstacle);
        }
        yield return null;
        NavMeshObstacle navObstacle = plane.AddComponent<NavMeshObstacle>();
        navObstacle.carving = true;
        navObstacle.carveOnlyStationary = false;
    }

    public Vector3 GetRandomPoint(float min_X, float max_X, float min_Z, float max_Z)
    {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < 25; i++)
        {
            position = new Vector3(Random.Range(min_X - surface.center.x, max_X - surface.center.x), 1f, Random.Range(min_Z - surface.center.z, max_Z - surface.center.z));
            Debug.Log(position);
            if (NavMesh.SamplePosition(position, out _, 2.0f, 0))
            {
                break;
            }
            position = Vector3.zero;
        }
        return position;
    }


}